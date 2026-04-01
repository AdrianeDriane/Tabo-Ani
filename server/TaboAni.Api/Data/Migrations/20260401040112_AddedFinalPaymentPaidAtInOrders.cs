using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaboAni.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedFinalPaymentPaidAtInOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "final_payment_paid_at",
                table: "orders",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "final_payment_paid_at",
                table: "orders");
        }
    }
}
