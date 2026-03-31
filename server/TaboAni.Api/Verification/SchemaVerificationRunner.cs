using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TaboAni.Api.Data;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Verification;

public static class SchemaVerificationRunner
{
    public static int Run()
    {
        var failures = new List<string>();

        var expectedTables = new HashSet<string>(StringComparer.Ordinal)
        {
            "audit_logs",
            "buyer_profiles",
            "cart_items",
            "carts",
            "conversation_participants",
            "conversations",
            "deliveries",
            "delivery_assignments",
            "delivery_orders",
            "delivery_status_history",
            "distributor_profiles",
            "escrow_transactions",
            "farmer_listing_vehicle_types",
            "farmer_payouts",
            "farmer_profiles",
            "kyc_applications",
            "kyc_documents",
            "kyc_reviews",
            "listing_availability_windows",
            "listing_price_history",
            "messages",
            "order_cancellations",
            "order_items",
            "order_status_history",
            "orders",
            "payments",
            "produce_categories",
            "produce_inventory_batches",
            "produce_listing_images",
            "produce_listings",
            "qa_issue_flags",
            "qa_report_images",
            "qa_reports",
            "reviews",
            "roles",
            "user_roles",
            "users",
            "vehicle_types",
            "wallet_transactions",
            "wallets"
        };

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql("Host=localhost;Database=tabo_ani_schema_verification;Username=postgres;Password=postgres")
            .Options;

        using var context = new AppDbContext(options);

        var actualTables = context.Model.GetEntityTypes()
            .Select(entityType => entityType.GetTableName())
            .Where(tableName => !string.IsNullOrWhiteSpace(tableName))
            .Select(tableName => tableName!)
            .ToHashSet(StringComparer.Ordinal);

        Expect(
            expectedTables.SetEquals(actualTables),
            $"Expected {expectedTables.Count} tables but found {actualTables.Count}. Missing: {string.Join(", ", expectedTables.Except(actualTables))}. Extra: {string.Join(", ", actualTables.Except(expectedTables))}.",
            failures);

        Expect(
            !actualTables.Contains("ProduceListings"),
            "Legacy table name 'ProduceListings' should not be present in the model.",
            failures);

        AssertIndex(
            context.Model.FindEntityType(typeof(User))!,
            "uq_users_email",
            "\"email\" IS NOT NULL",
            failures);

        AssertIndex(
            context.Model.FindEntityType(typeof(User))!,
            "uq_users_mobile_number",
            "\"mobile_number\" IS NOT NULL",
            failures);

        AssertIndex(
            context.Model.FindEntityType(typeof(DistributorProfile))!,
            "uq_distributor_profiles_license_number",
            "\"license_number\" IS NOT NULL",
            failures);

        AssertIndex(
            context.Model.FindEntityType(typeof(ProduceInventoryBatch))!,
            "uq_produce_inventory_batches_listing_batch_code",
            "\"batch_code\" IS NOT NULL",
            failures);

        AssertIndex(
            context.Model.FindEntityType(typeof(KycApplication))!,
            "ix_kyc_applications_status_submitted_at",
            failures);

        AssertIndex(
            context.Model.FindEntityType(typeof(ProduceListing))!,
            "ix_produce_listings_category_status_created_at",
            failures);

        AssertIndex(
            context.Model.FindEntityType(typeof(ListingPriceHistory))!,
            "ix_listing_price_history_listing_effective_at",
            failures);

        AssertIndex(
            context.Model.FindEntityType(typeof(Order))!,
            "ix_orders_buyer_status_created_at",
            failures);

        AssertIndex(
            context.Model.FindEntityType(typeof(Message))!,
            "ix_messages_conversation_sent_at",
            failures);

        AssertIndex(
            context.Model.FindEntityType(typeof(WalletTransaction))!,
            "ix_wallet_transactions_wallet_created_at",
            failures);

        AssertIndex(
            context.Model.FindEntityType(typeof(AuditLog))!,
            "ix_audit_logs_entity_lookup",
            failures);

        var createScript = context.Database.GenerateCreateScript();

        Expect(
            Regex.Matches(createScript, @"CREATE TABLE", RegexOptions.IgnoreCase).Count == expectedTables.Count,
            $"Expected {expectedTables.Count} CREATE TABLE statements in the generated script.",
            failures);

        AssertScriptContains(createScript, "CONSTRAINT ck_users_contact CHECK", failures);
        AssertScriptContains(createScript, "CONSTRAINT ck_listing_availability_windows_date_range CHECK", failures);
        AssertScriptContains(createScript, "CONSTRAINT ck_qa_reports_fresh_and_damaged_percent CHECK", failures);
        AssertScriptContains(createScript, "CONSTRAINT ck_reviews_star_rating CHECK", failures);
        AssertScriptContains(createScript, "CREATE UNIQUE INDEX uq_delivery_orders_order_id", failures);
        AssertScriptContains(createScript, "CREATE UNIQUE INDEX uq_users_email", failures);
        AssertScriptContains(createScript, "CREATE UNIQUE INDEX uq_users_mobile_number", failures);
        AssertScriptContains(createScript, "CREATE UNIQUE INDEX uq_distributor_profiles_license_number", failures);
        AssertScriptContains(createScript, "CREATE UNIQUE INDEX uq_produce_inventory_batches_listing_batch_code", failures);
        AssertScriptContains(createScript, "CREATE INDEX ix_kyc_applications_status_submitted_at", failures);
        AssertScriptContains(createScript, "CREATE INDEX ix_produce_listings_category_status_created_at", failures);
        AssertScriptContains(createScript, "CREATE INDEX ix_listing_price_history_listing_effective_at", failures);
        AssertScriptContains(createScript, "CREATE INDEX ix_orders_buyer_status_created_at", failures);
        AssertScriptContains(createScript, "CREATE INDEX ix_messages_conversation_sent_at", failures);
        AssertScriptContains(createScript, "CREATE INDEX ix_wallet_transactions_wallet_created_at", failures);
        AssertScriptContains(createScript, "CREATE INDEX ix_audit_logs_entity_lookup", failures);

        if (failures.Count > 0)
        {
            Console.Error.WriteLine("Schema verification failed:");

            foreach (var failure in failures)
            {
                Console.Error.WriteLine($"- {failure}");
            }

            return 1;
        }

        Console.WriteLine("Schema verification passed.");
        return 0;
    }

    private static void AssertIndex(
        IReadOnlyEntityType entityType,
        string expectedName,
        ICollection<string> failures)
    {
        var index = entityType.GetIndexes().SingleOrDefault(candidate => candidate.GetDatabaseName() == expectedName);

        if (index is null)
        {
            failures.Add($"Expected index '{expectedName}' was not found on '{entityType.GetTableName()}'.");
        }
    }

    private static void AssertIndex(
        IReadOnlyEntityType entityType,
        string expectedName,
        string expectedFilter,
        ICollection<string> failures)
    {
        var index = entityType.GetIndexes().SingleOrDefault(candidate => candidate.GetDatabaseName() == expectedName);

        if (index is null)
        {
            failures.Add($"Expected index '{expectedName}' was not found on '{entityType.GetTableName()}'.");
            return;
        }

        if (!index.IsUnique)
        {
            failures.Add($"Index '{expectedName}' on '{entityType.GetTableName()}' should be unique.");
        }

        if (!string.Equals(index.GetFilter(), expectedFilter, StringComparison.Ordinal))
        {
            failures.Add(
                $"Index '{expectedName}' on '{entityType.GetTableName()}' should use filter '{expectedFilter}', but found '{index.GetFilter()}'.");
        }
    }

    private static void AssertScriptContains(string script, string expectedValue, ICollection<string> failures)
    {
        if (!script.Contains(expectedValue, StringComparison.Ordinal))
        {
            failures.Add($"Generated create script did not contain '{expectedValue}'.");
        }
    }

    private static void Expect(bool condition, string failureMessage, ICollection<string> failures)
    {
        if (!condition)
        {
            failures.Add(failureMessage);
        }
    }
}

