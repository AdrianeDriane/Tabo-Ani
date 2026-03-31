using System.Net;
using System.Text.Json;

namespace TaboAni.Api.Models;

public class User
{
    public Guid UserId { get; set; }
    public string? Email { get; set; }
    public string? MobileNumber { get; set; }
    public string? PasswordHash { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public string? ProfilePhotoUrl { get; set; }
    public bool IsEmailVerified { get; set; }
    public bool IsMobileVerified { get; set; }
    public string AccountStatus { get; set; } = string.Empty;
    public DateTimeOffset? LastLoginAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public class Role
{
    public Guid RoleId { get; set; }
    public string RoleCode { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class UserRole
{
    public Guid UserRoleId { get; set; }
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public DateTimeOffset AssignedAt { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class FarmerProfile
{
    public Guid FarmerProfileId { get; set; }
    public Guid UserId { get; set; }
    public string FarmName { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string FarmLocationText { get; set; } = string.Empty;
    public decimal? FarmLatitude { get; set; }
    public decimal? FarmLongitude { get; set; }
    public int? YearsOfExperience { get; set; }
    public bool IsPublic { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public class BuyerProfile
{
    public Guid BuyerProfileId { get; set; }
    public Guid UserId { get; set; }
    public string BusinessName { get; set; } = string.Empty;
    public string ContactPersonName { get; set; } = string.Empty;
    public string BusinessType { get; set; } = string.Empty;
    public string BusinessLocationText { get; set; } = string.Empty;
    public decimal? BusinessLatitude { get; set; }
    public decimal? BusinessLongitude { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public class DistributorProfile
{
    public Guid DistributorProfileId { get; set; }
    public Guid UserId { get; set; }
    public string FleetDisplayName { get; set; } = string.Empty;
    public string? LicenseNumber { get; set; }
    public string BaseLocationText { get; set; } = string.Empty;
    public decimal? BaseLatitude { get; set; }
    public decimal? BaseLongitude { get; set; }
    public bool IsAvailable { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public class KycApplication
{
    public Guid KycApplicationId { get; set; }
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public string ApplicationStatus { get; set; } = string.Empty;
    public DateTimeOffset SubmittedAt { get; set; }
    public DateTimeOffset? ReviewedAt { get; set; }
    public string? FinalRemarks { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public class KycDocument
{
    public Guid KycDocumentId { get; set; }
    public Guid KycApplicationId { get; set; }
    public string DocumentType { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string MimeType { get; set; } = string.Empty;
    public long? FileSizeBytes { get; set; }
    public string DocumentStatus { get; set; } = string.Empty;
    public string? RejectionReason { get; set; }
    public DateTimeOffset UploadedAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class KycReview
{
    public Guid KycReviewId { get; set; }
    public Guid KycApplicationId { get; set; }
    public Guid ReviewedByUserId { get; set; }
    public string ReviewAction { get; set; } = string.Empty;
    public string? Remarks { get; set; }
    public DateTimeOffset ReviewedAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class ProduceCategory
{
    public Guid ProduceCategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class ProduceListing
{
    public Guid ProduceListingId { get; set; }
    public Guid FarmerProfileId { get; set; }
    public Guid ProduceCategoryId { get; set; }
    public string ListingTitle { get; set; } = string.Empty;
    public string ProduceName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal PricePerKg { get; set; }
    public decimal MinimumOrderKg { get; set; }
    public decimal? MaximumOrderKg { get; set; }
    public string ListingStatus { get; set; } = string.Empty;
    public string PrimaryLocationText { get; set; } = string.Empty;
    public decimal? PrimaryLatitude { get; set; }
    public decimal? PrimaryLongitude { get; set; }
    public bool IsPremiumBoosted { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public class ProduceListingImage
{
    public Guid ProduceListingImageId { get; set; }
    public Guid ProduceListingId { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public bool IsPrimary { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class ProduceInventoryBatch
{
    public Guid ProduceInventoryBatchId { get; set; }
    public Guid ProduceListingId { get; set; }
    public string? BatchCode { get; set; }
    public DateOnly? EstimatedHarvestDate { get; set; }
    public DateOnly? ActualHarvestDate { get; set; }
    public decimal AvailableQuantityKg { get; set; }
    public decimal ReservedQuantityKg { get; set; }
    public string InventoryStatus { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public class ListingPriceHistory
{
    public Guid ListingPriceHistoryId { get; set; }
    public Guid ProduceListingId { get; set; }
    public decimal OldPricePerKg { get; set; }
    public decimal NewPricePerKg { get; set; }
    public Guid? ChangedByUserId { get; set; }
    public DateTimeOffset EffectiveAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class ListingAvailabilityWindow
{
    public Guid ListingAvailabilityWindowId { get; set; }
    public Guid ProduceListingId { get; set; }
    public DateOnly AvailableFromDate { get; set; }
    public DateOnly AvailableToDate { get; set; }
    public string? Notes { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public class VehicleType
{
    public Guid VehicleTypeId { get; set; }
    public string VehicleTypeName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal MaxCapacityKg { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class FarmerListingVehicleType
{
    public Guid FarmerListingVehicleTypeId { get; set; }
    public Guid ProduceListingId { get; set; }
    public Guid VehicleTypeId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class Order
{
    public Guid OrderId { get; set; }
    public Guid BuyerUserId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string OrderStatus { get; set; } = string.Empty;
    public decimal DownpaymentDueAmount { get; set; }
    public decimal DownpaymentPaidAmount { get; set; }
    public decimal FinalPaymentDueAmount { get; set; }
    public decimal FinalPaymentPaidAmount { get; set; }
    public decimal SubtotalAmount { get; set; }
    public decimal DeliveryFeeAmount { get; set; }
    public decimal PlatformFeeAmount { get; set; }
    public decimal RefundAmount { get; set; }
    public string DeliveryLocationText { get; set; } = string.Empty;
    public decimal? DeliveryLatitude { get; set; }
    public decimal? DeliveryLongitude { get; set; }
    public DateOnly? RequestedDeliveryDate { get; set; }
    public DateTimeOffset? DownpaymentPaidAt { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
    public DateTimeOffset? CancelledAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public class OrderItem
{
    public Guid OrderItemId { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProduceListingId { get; set; }
    public decimal QuantityKg { get; set; }
    public decimal UnitPricePerKg { get; set; }
    public decimal LineSubtotalAmount { get; set; }
    public string ListingTitleSnapshot { get; set; } = string.Empty;
    public string ProduceNameSnapshot { get; set; } = string.Empty;
    public Guid FarmerProfileId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class OrderStatusHistory
{
    public Guid OrderStatusHistoryId { get; set; }
    public Guid OrderId { get; set; }
    public Guid? TriggeredByUserId { get; set; }
    public string? FromStatus { get; set; }
    public string ToStatus { get; set; } = string.Empty;
    public string? Remarks { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class OrderCancellation
{
    public Guid OrderCancellationId { get; set; }
    public Guid OrderId { get; set; }
    public Guid CancelledByUserId { get; set; }
    public string CancelledByRoleCode { get; set; } = string.Empty;
    public string CancellationReason { get; set; } = string.Empty;
    public DateTimeOffset CancelledAt { get; set; }
    public string RefundPolicyApplied { get; set; } = string.Empty;
    public decimal RefundPercentage { get; set; }
    public decimal RefundAmount { get; set; }
    public decimal FarmerKeptAmount { get; set; }
    public decimal PlatformKeptAmount { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class Cart
{
    public Guid CartId { get; set; }
    public Guid UserId { get; set; }
    public string CartStatus { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public class CartItem
{
    public Guid CartItemId { get; set; }
    public Guid CartId { get; set; }
    public Guid ProduceListingId { get; set; }
    public decimal QuantityKg { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public class Wallet
{
    public Guid WalletId { get; set; }
    public Guid UserId { get; set; }
    public decimal AvailableBalance { get; set; }
    public decimal HeldBalance { get; set; }
    public string WalletStatus { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public class WalletTransaction
{
    public Guid WalletTransactionId { get; set; }
    public Guid WalletId { get; set; }
    public Guid? PaymentId { get; set; }
    public Guid? EscrowTransactionId { get; set; }
    public Guid? FarmerPayoutId { get; set; }
    public string TransactionType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal BalanceBefore { get; set; }
    public decimal BalanceAfter { get; set; }
    public string? ReferenceCode { get; set; }
    public string? Remarks { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class Payment
{
    public Guid PaymentId { get; set; }
    public Guid OrderId { get; set; }
    public string PaymentType { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? ExternalReference { get; set; }
    public DateTimeOffset? PaidAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public class EscrowTransaction
{
    public Guid EscrowTransactionId { get; set; }
    public Guid OrderId { get; set; }
    public Guid? PaymentId { get; set; }
    public Guid? FarmerPayoutId { get; set; }
    public string EscrowAction { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string ActionStatus { get; set; } = string.Empty;
    public DateTimeOffset ActedAt { get; set; }
    public string? Remarks { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class FarmerPayout
{
    public Guid FarmerPayoutId { get; set; }
    public Guid FarmerProfileId { get; set; }
    public Guid OrderId { get; set; }
    public decimal GrossAmount { get; set; }
    public decimal PlatformFeeAmount { get; set; }
    public decimal NetAmount { get; set; }
    public string PayoutStatus { get; set; } = string.Empty;
    public DateTimeOffset? ReleasedAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public class Delivery
{
    public Guid DeliveryId { get; set; }
    public Guid VehicleTypeId { get; set; }
    public string DeliveryCode { get; set; } = string.Empty;
    public string DeliveryStatus { get; set; } = string.Empty;
    public DateTimeOffset? PlannedPickupDate { get; set; }
    public DateTimeOffset? ActualPickupAt { get; set; }
    public DateTimeOffset? ActualArrivalAt { get; set; }
    public decimal TotalReservedCapacityKg { get; set; }
    public string? Notes { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public class DeliveryOrder
{
    public Guid DeliveryOrderId { get; set; }
    public Guid DeliveryId { get; set; }
    public Guid OrderId { get; set; }
    public decimal ReservedCapacityKg { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class DeliveryAssignment
{
    public Guid DeliveryAssignmentId { get; set; }
    public Guid DeliveryId { get; set; }
    public Guid DistributorUserId { get; set; }
    public string AssignmentStatus { get; set; } = string.Empty;
    public DateTimeOffset AssignedAt { get; set; }
    public DateTimeOffset? EndedAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class DeliveryStatusHistory
{
    public Guid DeliveryStatusHistoryId { get; set; }
    public Guid DeliveryId { get; set; }
    public Guid? TriggeredByUserId { get; set; }
    public string? FromStatus { get; set; }
    public string ToStatus { get; set; } = string.Empty;
    public string? Remarks { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

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

public class QaReportImage
{
    public Guid QaReportImageId { get; set; }
    public Guid QaReportId { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class QaIssueFlag
{
    public Guid QaIssueFlagId { get; set; }
    public Guid QaReportId { get; set; }
    public string IssueType { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
}

public class Conversation
{
    public Guid ConversationId { get; set; }
    public Guid OrderId { get; set; }
    public string ConversationStatus { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public class ConversationParticipant
{
    public Guid ConversationParticipantId { get; set; }
    public Guid ConversationId { get; set; }
    public Guid UserId { get; set; }
    public string ParticipantRoleCode { get; set; } = string.Empty;
    public DateTimeOffset JoinedAt { get; set; }
    public DateTimeOffset? LeftAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class Message
{
    public Guid MessageId { get; set; }
    public Guid ConversationId { get; set; }
    public Guid SenderUserId { get; set; }
    public string MessageBody { get; set; } = string.Empty;
    public string MessageType { get; set; } = string.Empty;
    public DateTimeOffset SentAt { get; set; }
    public DateTimeOffset? EditedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class Review
{
    public Guid ReviewId { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProduceListingId { get; set; }
    public Guid ReviewerUserId { get; set; }
    public short StarRating { get; set; }
    public string? ReviewText { get; set; }
    public string ReviewStatus { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public class AuditLog
{
    public Guid AuditLogId { get; set; }
    public Guid? ActorUserId { get; set; }
    public string EntityType { get; set; } = string.Empty;
    public Guid? EntityId { get; set; }
    public string ActionType { get; set; } = string.Empty;
    public string ActionSummary { get; set; } = string.Empty;
    public JsonDocument? Metadata { get; set; }
    public IPAddress? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
