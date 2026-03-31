using Microsoft.AspNetCore.Mvc;
using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Interfaces.Service;

namespace TaboAni.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class OrdersController(IOrderService orderService) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseDto<OrderResponseDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateOrder(
        [FromBody] OrderRequestDto orderRequestDto,
        CancellationToken cancellationToken)
    {
        if (orderRequestDto.BuyerUserId == Guid.Empty)
        {
            return BadRequest(new ErrorResponseDto
            {
                Success = false,
                Message = "Order creation failed.",
                Errors = ["Buyer user ID is required."]
            });
        }

        try
        {
            var createdOrder = await _orderService.CreateOrderAsync(orderRequestDto, cancellationToken);

            return CreatedAtAction(
                nameof(GetOrdersByUserId),
                new { userId = createdOrder.BuyerUserId },
                new ApiResponseDto<OrderResponseDto>
                {
                    Success = true,
                    Message = "Order created successfully.",
                    Data = createdOrder
                });
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new ErrorResponseDto
            {
                Success = false,
                Message = "Order creation failed.",
                Errors = [exception.Message]
            });
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto
            {
                Success = false,
                Message = "An unexpected error occurred while creating the order.",
                Errors = ["Please try again later."]
            });
        }
    }

    [HttpGet("user/{userId:guid}")]
    [ProducesResponseType(typeof(ApiResponseDto<IEnumerable<OrderResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOrdersByUserId(Guid userId, CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty)
        {
            return BadRequest(new ErrorResponseDto
            {
                Success = false,
                Message = "Order lookup failed.",
                Errors = ["User ID is required."]
            });
        }

        try
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userId, cancellationToken);

            return Ok(new ApiResponseDto<IEnumerable<OrderResponseDto>>
            {
                Success = true,
                Message = "Orders retrieved successfully.",
                Data = orders
            });
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new ErrorResponseDto
            {
                Success = false,
                Message = "Order lookup failed.",
                Errors = [exception.Message]
            });
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto
            {
                Success = false,
                Message = "An unexpected error occurred while retrieving orders.",
                Errors = ["Please try again later."]
            });
        }
    }
}
