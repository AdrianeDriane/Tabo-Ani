using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaboAni.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddQueryIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_wallet_transactions_wallet_id",
                table: "wallet_transactions");

            migrationBuilder.DropIndex(
                name: "ix_reviews_produce_listing_id",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "ix_produce_listings_produce_category_id",
                table: "produce_listings");

            migrationBuilder.DropIndex(
                name: "ix_orders_buyer_user_id",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "ix_order_status_history_order_id",
                table: "order_status_history");

            migrationBuilder.DropIndex(
                name: "ix_messages_conversation_id",
                table: "messages");

            migrationBuilder.DropIndex(
                name: "ix_listing_price_history_produce_listing_id",
                table: "listing_price_history");

            migrationBuilder.DropIndex(
                name: "ix_delivery_status_history_delivery_id",
                table: "delivery_status_history");

            migrationBuilder.CreateIndex(
                name: "ix_wallet_transactions_wallet_created_at",
                table: "wallet_transactions",
                columns: new[] { "wallet_id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_reviews_listing_status_created_at",
                table: "reviews",
                columns: new[] { "produce_listing_id", "review_status", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_produce_listings_category_status_created_at",
                table: "produce_listings",
                columns: new[] { "produce_category_id", "listing_status", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_orders_buyer_status_created_at",
                table: "orders",
                columns: new[] { "buyer_user_id", "order_status", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_order_status_history_order_created_at",
                table: "order_status_history",
                columns: new[] { "order_id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_messages_conversation_sent_at",
                table: "messages",
                columns: new[] { "conversation_id", "sent_at" });

            migrationBuilder.CreateIndex(
                name: "ix_listing_price_history_listing_effective_at",
                table: "listing_price_history",
                columns: new[] { "produce_listing_id", "effective_at" });

            migrationBuilder.CreateIndex(
                name: "ix_kyc_applications_status_submitted_at",
                table: "kyc_applications",
                columns: new[] { "application_status", "submitted_at" });

            migrationBuilder.CreateIndex(
                name: "ix_delivery_status_history_delivery_created_at",
                table: "delivery_status_history",
                columns: new[] { "delivery_id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_deliveries_status_planned_pickup",
                table: "deliveries",
                columns: new[] { "delivery_status", "planned_pickup_date" });

            migrationBuilder.CreateIndex(
                name: "ix_audit_logs_entity_lookup",
                table: "audit_logs",
                columns: new[] { "entity_type", "entity_id", "created_at" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_wallet_transactions_wallet_created_at",
                table: "wallet_transactions");

            migrationBuilder.DropIndex(
                name: "ix_reviews_listing_status_created_at",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "ix_produce_listings_category_status_created_at",
                table: "produce_listings");

            migrationBuilder.DropIndex(
                name: "ix_orders_buyer_status_created_at",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "ix_order_status_history_order_created_at",
                table: "order_status_history");

            migrationBuilder.DropIndex(
                name: "ix_messages_conversation_sent_at",
                table: "messages");

            migrationBuilder.DropIndex(
                name: "ix_listing_price_history_listing_effective_at",
                table: "listing_price_history");

            migrationBuilder.DropIndex(
                name: "ix_kyc_applications_status_submitted_at",
                table: "kyc_applications");

            migrationBuilder.DropIndex(
                name: "ix_delivery_status_history_delivery_created_at",
                table: "delivery_status_history");

            migrationBuilder.DropIndex(
                name: "ix_deliveries_status_planned_pickup",
                table: "deliveries");

            migrationBuilder.DropIndex(
                name: "ix_audit_logs_entity_lookup",
                table: "audit_logs");

            migrationBuilder.CreateIndex(
                name: "ix_wallet_transactions_wallet_id",
                table: "wallet_transactions",
                column: "wallet_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_produce_listing_id",
                table: "reviews",
                column: "produce_listing_id");

            migrationBuilder.CreateIndex(
                name: "ix_produce_listings_produce_category_id",
                table: "produce_listings",
                column: "produce_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_buyer_user_id",
                table: "orders",
                column: "buyer_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_status_history_order_id",
                table: "order_status_history",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_messages_conversation_id",
                table: "messages",
                column: "conversation_id");

            migrationBuilder.CreateIndex(
                name: "ix_listing_price_history_produce_listing_id",
                table: "listing_price_history",
                column: "produce_listing_id");

            migrationBuilder.CreateIndex(
                name: "ix_delivery_status_history_delivery_id",
                table: "delivery_status_history",
                column: "delivery_id");
        }
    }
}
