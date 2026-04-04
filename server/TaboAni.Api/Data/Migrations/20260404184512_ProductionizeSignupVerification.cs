using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaboAni.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductionizeSignupVerification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_email_verification_tokens_user_id_token_hash",
                table: "email_verification_tokens");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "invalidated_at",
                table: "email_verification_tokens",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "user_policy_acceptances",
                columns: table => new
                {
                    user_policy_acceptance_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    policy_type = table.Column<string>(type: "text", nullable: false),
                    policy_version = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    accepted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_policy_acceptances", x => x.user_policy_acceptance_id);
                    table.ForeignKey(
                        name: "fk_user_policy_acceptances_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_email_verification_tokens_user_id_created_at",
                table: "email_verification_tokens",
                columns: new[] { "user_id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ux_email_verification_tokens_token_hash",
                table: "email_verification_tokens",
                column: "token_hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_policy_acceptances_user_id_policy_type_accepted_at",
                table: "user_policy_acceptances",
                columns: new[] { "user_id", "policy_type", "accepted_at" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_policy_acceptances");

            migrationBuilder.DropIndex(
                name: "ix_email_verification_tokens_user_id_created_at",
                table: "email_verification_tokens");

            migrationBuilder.DropIndex(
                name: "ux_email_verification_tokens_token_hash",
                table: "email_verification_tokens");

            migrationBuilder.DropColumn(
                name: "invalidated_at",
                table: "email_verification_tokens");

            migrationBuilder.CreateIndex(
                name: "ix_email_verification_tokens_user_id_token_hash",
                table: "email_verification_tokens",
                columns: new[] { "user_id", "token_hash" });
        }
    }
}
