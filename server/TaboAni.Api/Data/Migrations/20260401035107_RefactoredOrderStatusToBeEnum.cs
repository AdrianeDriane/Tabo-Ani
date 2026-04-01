using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaboAni.Api.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredOrderStatusToBeEnum : Migration
    {
        /// <inheritdoc />
       protected override void Up(MigrationBuilder migrationBuilder)
{
    // Normalize old text enum names into numeric text first
    migrationBuilder.Sql(@"
        UPDATE orders
        SET order_status = CASE trim(order_status)
            WHEN 'PendingDownpayment' THEN '1'
            WHEN 'PendingFinalPayment' THEN '2'
            WHEN 'Completed' THEN '3'
            WHEN 'Cancelled' THEN '4'
            WHEN '1' THEN '1'
            WHEN '2' THEN '2'
            WHEN '3' THEN '3'
            WHEN '4' THEN '4'
            ELSE NULL
        END;
    ");

    // Optional but recommended: fail early if unexpected values exist
    migrationBuilder.Sql(@"
        DO $$
        BEGIN
            IF EXISTS (SELECT 1 FROM orders WHERE order_status IS NULL) THEN
                RAISE EXCEPTION 'orders.order_status contains unsupported values for enum conversion';
            END IF;
        END $$;
    ");

    // Use explicit cast for PostgreSQL
    migrationBuilder.Sql(@"
        ALTER TABLE orders
        ALTER COLUMN order_status TYPE integer
        USING order_status::integer;
    ");
}

protected override void Down(MigrationBuilder migrationBuilder)
{
    migrationBuilder.Sql(@"
        ALTER TABLE orders
        ALTER COLUMN order_status TYPE text
        USING order_status::text;
    ");

    // Optional: map integer text back to original names
    migrationBuilder.Sql(@"
        UPDATE orders
        SET order_status = CASE order_status
            WHEN '1' THEN 'PendingDownpayment'
            WHEN '2' THEN 'PendingFinalPayment'
            WHEN '3' THEN 'Completed'
            WHEN '4' THEN 'Cancelled'
            ELSE order_status
        END;
    ");
}
    }
}
