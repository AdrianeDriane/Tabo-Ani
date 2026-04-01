using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaboAni.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedUpdatedAtOnOrderItemsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "updated_at",
                table: "order_items",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "order_items");
        }
    }
}
