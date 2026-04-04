using Microsoft.EntityFrameworkCore;
using TaboAni.Api.Data.Configurations;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<FarmerProfile> FarmerProfiles => Set<FarmerProfile>();
    public DbSet<BuyerProfile> BuyerProfiles => Set<BuyerProfile>();
    public DbSet<DistributorProfile> DistributorProfiles => Set<DistributorProfile>();
    public DbSet<KycApplication> KycApplications => Set<KycApplication>();
    public DbSet<EmailVerificationToken> EmailVerificationTokens => Set<EmailVerificationToken>();
    public DbSet<KycDocument> KycDocuments => Set<KycDocument>();
    public DbSet<KycReview> KycReviews => Set<KycReview>();
    public DbSet<ProduceCategory> ProduceCategories => Set<ProduceCategory>();
    public DbSet<ProduceListing> ProduceListings => Set<ProduceListing>();
    public DbSet<ProduceListingImage> ProduceListingImages => Set<ProduceListingImage>();
    public DbSet<ProduceInventoryBatch> ProduceInventoryBatches => Set<ProduceInventoryBatch>();
    public DbSet<ListingPriceHistory> ListingPriceHistory => Set<ListingPriceHistory>();
    public DbSet<ListingAvailabilityWindow> ListingAvailabilityWindows => Set<ListingAvailabilityWindow>();
    public DbSet<VehicleType> VehicleTypes => Set<VehicleType>();
    public DbSet<FarmerListingVehicleType> FarmerListingVehicleTypes => Set<FarmerListingVehicleType>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<OrderStatusHistory> OrderStatusHistory => Set<OrderStatusHistory>();
    public DbSet<OrderCancellation> OrderCancellations => Set<OrderCancellation>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Wallet> Wallets => Set<Wallet>();
    public DbSet<WalletTransaction> WalletTransactions => Set<WalletTransaction>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<EscrowTransaction> EscrowTransactions => Set<EscrowTransaction>();
    public DbSet<FarmerPayout> FarmerPayouts => Set<FarmerPayout>();
    public DbSet<Delivery> Deliveries => Set<Delivery>();
    public DbSet<DeliveryOrder> DeliveryOrders => Set<DeliveryOrder>();
    public DbSet<DeliveryAssignment> DeliveryAssignments => Set<DeliveryAssignment>();
    public DbSet<DeliveryStatusHistory> DeliveryStatusHistory => Set<DeliveryStatusHistory>();
    public DbSet<QaReport> QaReports => Set<QaReport>();
    public DbSet<QaReportImage> QaReportImages => Set<QaReportImage>();
    public DbSet<QaIssueFlag> QaIssueFlags => Set<QaIssueFlag>();
    public DbSet<Conversation> Conversations => Set<Conversation>();
    public DbSet<ConversationParticipant> ConversationParticipants => Set<ConversationParticipant>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelBuilder.ApplySnakeCaseNaming();
    }
}

