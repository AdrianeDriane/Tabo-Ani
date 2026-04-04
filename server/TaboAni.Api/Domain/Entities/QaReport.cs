namespace TaboAni.Api.Domain.Entities;

public class QaReport
{
    public Guid QaReportId { get; set; }
    public Guid DeliveryId { get; set; }
    public Guid OrderId { get; set; }
    public Guid SubmittedByUserId { get; set; }
    public string QaStage { get; set; } = string.Empty;
    public decimal FreshPercent { get; set; }
    public decimal DamagedPercent { get; set; }
    public decimal ExpectedQuantityKg { get; set; }
    public decimal ActualQuantityKg { get; set; }
    public string OverallCondition { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public DateTimeOffset SubmittedAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

