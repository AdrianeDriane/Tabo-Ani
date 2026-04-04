using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaboAni.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixCartMarketPlaceListingEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_farmer_listing_vehicle_types",
                table: "farmer_listing_vehicle_types");

            migrationBuilder.DropIndex(
                name: "ix_farmer_listing_vehicle_types_produce_listing_id_vehicle_typ~",
                table: "farmer_listing_vehicle_types");

            migrationBuilder.DropColumn(
                name: "farmer_listing_vehicle_type_id",
                table: "farmer_listing_vehicle_types");

            migrationBuilder.AddPrimaryKey(
                name: "pk_farmer_listing_vehicle_types",
                table: "farmer_listing_vehicle_types",
                columns: new[] { "produce_listing_id", "vehicle_type_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_farmer_listing_vehicle_types",
                table: "farmer_listing_vehicle_types");

            migrationBuilder.AddColumn<Guid>(
                name: "farmer_listing_vehicle_type_id",
                table: "farmer_listing_vehicle_types",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "pk_farmer_listing_vehicle_types",
                table: "farmer_listing_vehicle_types",
                column: "farmer_listing_vehicle_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_farmer_listing_vehicle_types_produce_listing_id_vehicle_typ~",
                table: "farmer_listing_vehicle_types",
                columns: new[] { "produce_listing_id", "vehicle_type_id" },
                unique: true);
        }
    }
}
