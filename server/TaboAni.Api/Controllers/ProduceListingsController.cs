using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaboAni.Api.Data;
using TaboAni.Api.Models;

namespace TaboAni.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProduceListingsController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public ProduceListingsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProduceListing>>> GetAll()
    {
        var items = await _dbContext.ProduceListings
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProduceListing>> GetById(int id)
    {
        var item = await _dbContext.ProduceListings.FindAsync(id);

        if (item is null)
            return NotFound();

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ProduceListing>> Create([FromBody] CreateProduceListingRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Name is required.");

        if (string.IsNullOrWhiteSpace(request.Category))
            return BadRequest("Category is required.");

        if (request.PricePerKg < 0)
            return BadRequest("PricePerKg cannot be negative.");

        var item = new ProduceListing
        {
            Name = request.Name.Trim(),
            Category = request.Category.Trim(),
            PricePerKg = request.PricePerKg
        };

        _dbContext.ProduceListings.Add(item);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }
}

public class CreateProduceListingRequest
{
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal PricePerKg { get; set; }
}