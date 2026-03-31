using System;
using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaboAni.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "produce_categories",
                columns: table => new
                {
                    produce_category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_produce_categories", x => x.produce_category_id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    role_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    mobile_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    display_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    profile_photo_url = table.Column<string>(type: "text", nullable: true),
                    is_email_verified = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    is_mobile_verified = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    account_status = table.Column<string>(type: "text", nullable: false),
                    last_login_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.user_id);
                    table.CheckConstraint("ck_users_contact", "\"email\" IS NOT NULL OR \"mobile_number\" IS NOT NULL");
                });

            migrationBuilder.CreateTable(
                name: "vehicle_types",
                columns: table => new
                {
                    vehicle_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_type_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    max_capacity_kg = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vehicle_types", x => x.vehicle_type_id);
                });

            migrationBuilder.CreateTable(
                name: "audit_logs",
                columns: table => new
                {
                    audit_log_id = table.Column<Guid>(type: "uuid", nullable: false),
                    actor_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    entity_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    entity_id = table.Column<Guid>(type: "uuid", nullable: true),
                    action_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    action_summary = table.Column<string>(type: "text", nullable: false),
                    metadata = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    ip_address = table.Column<IPAddress>(type: "inet", nullable: true),
                    user_agent = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_audit_logs", x => x.audit_log_id);
                    table.ForeignKey(
                        name: "fk_audit_logs_users_actor_user_id",
                        column: x => x.actor_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "buyer_profiles",
                columns: table => new
                {
                    buyer_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    business_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    contact_person_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    business_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    business_location_text = table.Column<string>(type: "text", nullable: false),
                    business_latitude = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: true),
                    business_longitude = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_buyer_profiles", x => x.buyer_profile_id);
                    table.ForeignKey(
                        name: "fk_buyer_profiles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    cart_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cart_status = table.Column<string>(type: "text", nullable: false, defaultValue: "ACTIVE"),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_carts", x => x.cart_id);
                    table.ForeignKey(
                        name: "fk_carts_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "distributor_profiles",
                columns: table => new
                {
                    distributor_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    fleet_display_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    license_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    base_location_text = table.Column<string>(type: "text", nullable: false),
                    base_latitude = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: true),
                    base_longitude = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: true),
                    is_available = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_distributor_profiles", x => x.distributor_profile_id);
                    table.ForeignKey(
                        name: "fk_distributor_profiles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "farmer_profiles",
                columns: table => new
                {
                    farmer_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    farm_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    bio = table.Column<string>(type: "text", nullable: true),
                    farm_location_text = table.Column<string>(type: "text", nullable: false),
                    farm_latitude = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: true),
                    farm_longitude = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: true),
                    years_of_experience = table.Column<int>(type: "integer", nullable: true),
                    is_public = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_farmer_profiles", x => x.farmer_profile_id);
                    table.ForeignKey(
                        name: "fk_farmer_profiles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "kyc_applications",
                columns: table => new
                {
                    kyc_application_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    application_status = table.Column<string>(type: "text", nullable: false),
                    submitted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    reviewed_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    final_remarks = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_kyc_applications", x => x.kyc_application_id);
                    table.ForeignKey(
                        name: "fk_kyc_applications_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_kyc_applications_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    buyer_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    order_status = table.Column<string>(type: "text", nullable: false),
                    downpayment_due_amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    downpayment_paid_amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false, defaultValue: 0.00m),
                    final_payment_due_amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    final_payment_paid_amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false, defaultValue: 0.00m),
                    subtotal_amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    delivery_fee_amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false, defaultValue: 0.00m),
                    platform_fee_amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false, defaultValue: 0.00m),
                    refund_amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false, defaultValue: 0.00m),
                    delivery_location_text = table.Column<string>(type: "text", nullable: false),
                    delivery_latitude = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: true),
                    delivery_longitude = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: true),
                    requested_delivery_date = table.Column<DateOnly>(type: "date", nullable: true),
                    downpayment_paid_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    completed_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    cancelled_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.order_id);
                    table.ForeignKey(
                        name: "fk_orders_users_buyer_user_id",
                        column: x => x.buyer_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    user_role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    assigned_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => x.user_role_id);
                    table.ForeignKey(
                        name: "fk_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "wallets",
                columns: table => new
                {
                    wallet_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    available_balance = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false, defaultValue: 0.00m),
                    held_balance = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false, defaultValue: 0.00m),
                    wallet_status = table.Column<string>(type: "text", nullable: false, defaultValue: "ACTIVE"),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_wallets", x => x.wallet_id);
                    table.ForeignKey(
                        name: "fk_wallets_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "deliveries",
                columns: table => new
                {
                    delivery_id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    delivery_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    delivery_status = table.Column<string>(type: "text", nullable: false),
                    planned_pickup_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    actual_pickup_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    actual_arrival_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    total_reserved_capacity_kg = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: false, defaultValue: 0.000m),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_deliveries", x => x.delivery_id);
                    table.ForeignKey(
                        name: "fk_deliveries_vehicle_types_vehicle_type_id",
                        column: x => x.vehicle_type_id,
                        principalTable: "vehicle_types",
                        principalColumn: "vehicle_type_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "produce_listings",
                columns: table => new
                {
                    produce_listing_id = table.Column<Guid>(type: "uuid", nullable: false),
                    farmer_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    produce_category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    listing_title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    produce_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    price_per_kg = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    minimum_order_kg = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: false, defaultValue: 1.000m),
                    maximum_order_kg = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: true),
                    listing_status = table.Column<string>(type: "text", nullable: false),
                    primary_location_text = table.Column<string>(type: "text", nullable: false),
                    primary_latitude = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: true),
                    primary_longitude = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: true),
                    is_premium_boosted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_produce_listings", x => x.produce_listing_id);
                    table.ForeignKey(
                        name: "fk_produce_listings_farmer_profiles_farmer_profile_id",
                        column: x => x.farmer_profile_id,
                        principalTable: "farmer_profiles",
                        principalColumn: "farmer_profile_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_produce_listings_produce_categories_produce_category_id",
                        column: x => x.produce_category_id,
                        principalTable: "produce_categories",
                        principalColumn: "produce_category_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "kyc_documents",
                columns: table => new
                {
                    kyc_document_id = table.Column<Guid>(type: "uuid", nullable: false),
                    kyc_application_id = table.Column<Guid>(type: "uuid", nullable: false),
                    document_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    file_url = table.Column<string>(type: "text", nullable: false),
                    file_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    mime_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    file_size_bytes = table.Column<long>(type: "bigint", nullable: true),
                    document_status = table.Column<string>(type: "text", nullable: false, defaultValue: "PENDING"),
                    rejection_reason = table.Column<string>(type: "text", nullable: true),
                    uploaded_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_kyc_documents", x => x.kyc_document_id);
                    table.ForeignKey(
                        name: "fk_kyc_documents_kyc_applications_kyc_application_id",
                        column: x => x.kyc_application_id,
                        principalTable: "kyc_applications",
                        principalColumn: "kyc_application_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "kyc_reviews",
                columns: table => new
                {
                    kyc_review_id = table.Column<Guid>(type: "uuid", nullable: false),
                    kyc_application_id = table.Column<Guid>(type: "uuid", nullable: false),
                    reviewed_by_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    review_action = table.Column<string>(type: "text", nullable: false),
                    remarks = table.Column<string>(type: "text", nullable: true),
                    reviewed_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_kyc_reviews", x => x.kyc_review_id);
                    table.ForeignKey(
                        name: "fk_kyc_reviews_kyc_applications_kyc_application_id",
                        column: x => x.kyc_application_id,
                        principalTable: "kyc_applications",
                        principalColumn: "kyc_application_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_kyc_reviews_users_reviewed_by_user_id",
                        column: x => x.reviewed_by_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "conversations",
                columns: table => new
                {
                    conversation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    conversation_status = table.Column<string>(type: "text", nullable: false, defaultValue: "ACTIVE"),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_conversations", x => x.conversation_id);
                    table.ForeignKey(
                        name: "fk_conversations_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "farmer_payouts",
                columns: table => new
                {
                    farmer_payout_id = table.Column<Guid>(type: "uuid", nullable: false),
                    farmer_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    gross_amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    platform_fee_amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false, defaultValue: 0.00m),
                    net_amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    payout_status = table.Column<string>(type: "text", nullable: false),
                    released_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_farmer_payouts", x => x.farmer_payout_id);
                    table.ForeignKey(
                        name: "fk_farmer_payouts_farmer_profiles_farmer_profile_id",
                        column: x => x.farmer_profile_id,
                        principalTable: "farmer_profiles",
                        principalColumn: "farmer_profile_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_farmer_payouts_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order_cancellations",
                columns: table => new
                {
                    order_cancellation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cancelled_by_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cancelled_by_role_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    cancellation_reason = table.Column<string>(type: "text", nullable: false),
                    cancelled_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    refund_policy_applied = table.Column<string>(type: "text", nullable: false),
                    refund_percentage = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false, defaultValue: 0.00m),
                    refund_amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false, defaultValue: 0.00m),
                    farmer_kept_amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false, defaultValue: 0.00m),
                    platform_kept_amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false, defaultValue: 0.00m),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_cancellations", x => x.order_cancellation_id);
                    table.ForeignKey(
                        name: "fk_order_cancellations_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_order_cancellations_users_cancelled_by_user_id",
                        column: x => x.cancelled_by_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order_status_history",
                columns: table => new
                {
                    order_status_history_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    triggered_by_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    from_status = table.Column<string>(type: "text", nullable: true),
                    to_status = table.Column<string>(type: "text", nullable: false),
                    remarks = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_status_history", x => x.order_status_history_id);
                    table.ForeignKey(
                        name: "fk_order_status_history_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_order_status_history_users_triggered_by_user_id",
                        column: x => x.triggered_by_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    payment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    payment_type = table.Column<string>(type: "text", nullable: false),
                    payment_method = table.Column<string>(type: "text", nullable: false),
                    payment_status = table.Column<string>(type: "text", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    external_reference = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    paid_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payments", x => x.payment_id);
                    table.ForeignKey(
                        name: "fk_payments_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "delivery_assignments",
                columns: table => new
                {
                    delivery_assignment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    delivery_id = table.Column<Guid>(type: "uuid", nullable: false),
                    distributor_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    assignment_status = table.Column<string>(type: "text", nullable: false),
                    assigned_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    ended_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_delivery_assignments", x => x.delivery_assignment_id);
                    table.ForeignKey(
                        name: "fk_delivery_assignments_deliveries_delivery_id",
                        column: x => x.delivery_id,
                        principalTable: "deliveries",
                        principalColumn: "delivery_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_delivery_assignments_users_distributor_user_id",
                        column: x => x.distributor_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "delivery_orders",
                columns: table => new
                {
                    delivery_order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    delivery_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    reserved_capacity_kg = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_delivery_orders", x => x.delivery_order_id);
                    table.ForeignKey(
                        name: "fk_delivery_orders_deliveries_delivery_id",
                        column: x => x.delivery_id,
                        principalTable: "deliveries",
                        principalColumn: "delivery_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_delivery_orders_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "delivery_status_history",
                columns: table => new
                {
                    delivery_status_history_id = table.Column<Guid>(type: "uuid", nullable: false),
                    delivery_id = table.Column<Guid>(type: "uuid", nullable: false),
                    triggered_by_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    from_status = table.Column<string>(type: "text", nullable: true),
                    to_status = table.Column<string>(type: "text", nullable: false),
                    remarks = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_delivery_status_history", x => x.delivery_status_history_id);
                    table.ForeignKey(
                        name: "fk_delivery_status_history_deliveries_delivery_id",
                        column: x => x.delivery_id,
                        principalTable: "deliveries",
                        principalColumn: "delivery_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_delivery_status_history_users_triggered_by_user_id",
                        column: x => x.triggered_by_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "qa_reports",
                columns: table => new
                {
                    qa_report_id = table.Column<Guid>(type: "uuid", nullable: false),
                    delivery_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    submitted_by_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    qa_stage = table.Column<string>(type: "text", nullable: false),
                    fresh_percent = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    damaged_percent = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    expected_quantity_kg = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: false),
                    actual_quantity_kg = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: false),
                    overall_condition = table.Column<string>(type: "text", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    submitted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_qa_reports", x => x.qa_report_id);
                    table.CheckConstraint("ck_qa_reports_fresh_and_damaged_percent", "\"fresh_percent\" + \"damaged_percent\" <= 100.00");
                    table.ForeignKey(
                        name: "fk_qa_reports_deliveries_delivery_id",
                        column: x => x.delivery_id,
                        principalTable: "deliveries",
                        principalColumn: "delivery_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_qa_reports_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_qa_reports_users_submitted_by_user_id",
                        column: x => x.submitted_by_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cart_items",
                columns: table => new
                {
                    cart_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cart_id = table.Column<Guid>(type: "uuid", nullable: false),
                    produce_listing_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity_kg = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cart_items", x => x.cart_item_id);
                    table.ForeignKey(
                        name: "fk_cart_items_carts_cart_id",
                        column: x => x.cart_id,
                        principalTable: "carts",
                        principalColumn: "cart_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cart_items_produce_listings_produce_listing_id",
                        column: x => x.produce_listing_id,
                        principalTable: "produce_listings",
                        principalColumn: "produce_listing_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "farmer_listing_vehicle_types",
                columns: table => new
                {
                    farmer_listing_vehicle_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    produce_listing_id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_farmer_listing_vehicle_types", x => x.farmer_listing_vehicle_type_id);
                    table.ForeignKey(
                        name: "fk_farmer_listing_vehicle_types_produce_listings_produce_listi~",
                        column: x => x.produce_listing_id,
                        principalTable: "produce_listings",
                        principalColumn: "produce_listing_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_farmer_listing_vehicle_types_vehicle_types_vehicle_type_id",
                        column: x => x.vehicle_type_id,
                        principalTable: "vehicle_types",
                        principalColumn: "vehicle_type_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "listing_availability_windows",
                columns: table => new
                {
                    listing_availability_window_id = table.Column<Guid>(type: "uuid", nullable: false),
                    produce_listing_id = table.Column<Guid>(type: "uuid", nullable: false),
                    available_from_date = table.Column<DateOnly>(type: "date", nullable: false),
                    available_to_date = table.Column<DateOnly>(type: "date", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_listing_availability_windows", x => x.listing_availability_window_id);
                    table.CheckConstraint("ck_listing_availability_windows_date_range", "\"available_to_date\" >= \"available_from_date\"");
                    table.ForeignKey(
                        name: "fk_listing_availability_windows_produce_listings_produce_listi~",
                        column: x => x.produce_listing_id,
                        principalTable: "produce_listings",
                        principalColumn: "produce_listing_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "listing_price_history",
                columns: table => new
                {
                    listing_price_history_id = table.Column<Guid>(type: "uuid", nullable: false),
                    produce_listing_id = table.Column<Guid>(type: "uuid", nullable: false),
                    old_price_per_kg = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    new_price_per_kg = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    changed_by_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    effective_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_listing_price_history", x => x.listing_price_history_id);
                    table.ForeignKey(
                        name: "fk_listing_price_history_produce_listings_produce_listing_id",
                        column: x => x.produce_listing_id,
                        principalTable: "produce_listings",
                        principalColumn: "produce_listing_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_listing_price_history_users_changed_by_user_id",
                        column: x => x.changed_by_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    order_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    produce_listing_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity_kg = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: false),
                    unit_price_per_kg = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    line_subtotal_amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    listing_title_snapshot = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    produce_name_snapshot = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    farmer_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_items", x => x.order_item_id);
                    table.ForeignKey(
                        name: "fk_order_items_farmer_profiles_farmer_profile_id",
                        column: x => x.farmer_profile_id,
                        principalTable: "farmer_profiles",
                        principalColumn: "farmer_profile_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_order_items_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_order_items_produce_listings_produce_listing_id",
                        column: x => x.produce_listing_id,
                        principalTable: "produce_listings",
                        principalColumn: "produce_listing_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "produce_inventory_batches",
                columns: table => new
                {
                    produce_inventory_batch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    produce_listing_id = table.Column<Guid>(type: "uuid", nullable: false),
                    batch_code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    estimated_harvest_date = table.Column<DateOnly>(type: "date", nullable: true),
                    actual_harvest_date = table.Column<DateOnly>(type: "date", nullable: true),
                    available_quantity_kg = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: false),
                    reserved_quantity_kg = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: false, defaultValue: 0.000m),
                    inventory_status = table.Column<string>(type: "text", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_produce_inventory_batches", x => x.produce_inventory_batch_id);
                    table.ForeignKey(
                        name: "fk_produce_inventory_batches_produce_listings_produce_listing_~",
                        column: x => x.produce_listing_id,
                        principalTable: "produce_listings",
                        principalColumn: "produce_listing_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "produce_listing_images",
                columns: table => new
                {
                    produce_listing_image_id = table.Column<Guid>(type: "uuid", nullable: false),
                    produce_listing_id = table.Column<Guid>(type: "uuid", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: false),
                    display_order = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_produce_listing_images", x => x.produce_listing_image_id);
                    table.ForeignKey(
                        name: "fk_produce_listing_images_produce_listings_produce_listing_id",
                        column: x => x.produce_listing_id,
                        principalTable: "produce_listings",
                        principalColumn: "produce_listing_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    review_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    produce_listing_id = table.Column<Guid>(type: "uuid", nullable: false),
                    reviewer_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    star_rating = table.Column<short>(type: "smallint", nullable: false),
                    review_text = table.Column<string>(type: "text", nullable: true),
                    review_status = table.Column<string>(type: "text", nullable: false, defaultValue: "PUBLISHED"),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reviews", x => x.review_id);
                    table.CheckConstraint("ck_reviews_star_rating", "\"star_rating\" BETWEEN 1 AND 5");
                    table.ForeignKey(
                        name: "fk_reviews_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_reviews_produce_listings_produce_listing_id",
                        column: x => x.produce_listing_id,
                        principalTable: "produce_listings",
                        principalColumn: "produce_listing_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_reviews_users_reviewer_user_id",
                        column: x => x.reviewer_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "conversation_participants",
                columns: table => new
                {
                    conversation_participant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    conversation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    participant_role_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    joined_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    left_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_conversation_participants", x => x.conversation_participant_id);
                    table.ForeignKey(
                        name: "fk_conversation_participants_conversations_conversation_id",
                        column: x => x.conversation_id,
                        principalTable: "conversations",
                        principalColumn: "conversation_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_conversation_participants_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    message_id = table.Column<Guid>(type: "uuid", nullable: false),
                    conversation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sender_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    message_body = table.Column<string>(type: "text", nullable: false),
                    message_type = table.Column<string>(type: "text", nullable: false, defaultValue: "TEXT"),
                    sent_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    edited_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_messages", x => x.message_id);
                    table.ForeignKey(
                        name: "fk_messages_conversations_conversation_id",
                        column: x => x.conversation_id,
                        principalTable: "conversations",
                        principalColumn: "conversation_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_messages_users_sender_user_id",
                        column: x => x.sender_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "escrow_transactions",
                columns: table => new
                {
                    escrow_transaction_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    payment_id = table.Column<Guid>(type: "uuid", nullable: true),
                    farmer_payout_id = table.Column<Guid>(type: "uuid", nullable: true),
                    escrow_action = table.Column<string>(type: "text", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    action_status = table.Column<string>(type: "text", nullable: false),
                    acted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    remarks = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_escrow_transactions", x => x.escrow_transaction_id);
                    table.ForeignKey(
                        name: "fk_escrow_transactions_farmer_payouts_farmer_payout_id",
                        column: x => x.farmer_payout_id,
                        principalTable: "farmer_payouts",
                        principalColumn: "farmer_payout_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_escrow_transactions_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_escrow_transactions_payments_payment_id",
                        column: x => x.payment_id,
                        principalTable: "payments",
                        principalColumn: "payment_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "qa_issue_flags",
                columns: table => new
                {
                    qa_issue_flag_id = table.Column<Guid>(type: "uuid", nullable: false),
                    qa_report_id = table.Column<Guid>(type: "uuid", nullable: false),
                    issue_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    severity = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_qa_issue_flags", x => x.qa_issue_flag_id);
                    table.ForeignKey(
                        name: "fk_qa_issue_flags_qa_reports_qa_report_id",
                        column: x => x.qa_report_id,
                        principalTable: "qa_reports",
                        principalColumn: "qa_report_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "qa_report_images",
                columns: table => new
                {
                    qa_report_image_id = table.Column<Guid>(type: "uuid", nullable: false),
                    qa_report_id = table.Column<Guid>(type: "uuid", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: false),
                    display_order = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_qa_report_images", x => x.qa_report_image_id);
                    table.ForeignKey(
                        name: "fk_qa_report_images_qa_reports_qa_report_id",
                        column: x => x.qa_report_id,
                        principalTable: "qa_reports",
                        principalColumn: "qa_report_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "wallet_transactions",
                columns: table => new
                {
                    wallet_transaction_id = table.Column<Guid>(type: "uuid", nullable: false),
                    wallet_id = table.Column<Guid>(type: "uuid", nullable: false),
                    payment_id = table.Column<Guid>(type: "uuid", nullable: true),
                    escrow_transaction_id = table.Column<Guid>(type: "uuid", nullable: true),
                    farmer_payout_id = table.Column<Guid>(type: "uuid", nullable: true),
                    transaction_type = table.Column<string>(type: "text", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    balance_before = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    balance_after = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    reference_code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    remarks = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_wallet_transactions", x => x.wallet_transaction_id);
                    table.ForeignKey(
                        name: "fk_wallet_transactions_escrow_transactions_escrow_transaction_~",
                        column: x => x.escrow_transaction_id,
                        principalTable: "escrow_transactions",
                        principalColumn: "escrow_transaction_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_wallet_transactions_farmer_payouts_farmer_payout_id",
                        column: x => x.farmer_payout_id,
                        principalTable: "farmer_payouts",
                        principalColumn: "farmer_payout_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_wallet_transactions_payments_payment_id",
                        column: x => x.payment_id,
                        principalTable: "payments",
                        principalColumn: "payment_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_wallet_transactions_wallets_wallet_id",
                        column: x => x.wallet_id,
                        principalTable: "wallets",
                        principalColumn: "wallet_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_audit_logs_actor_user_id",
                table: "audit_logs",
                column: "actor_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_buyer_profiles_user_id",
                table: "buyer_profiles",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_cart_items_cart_id_produce_listing_id",
                table: "cart_items",
                columns: new[] { "cart_id", "produce_listing_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_cart_items_produce_listing_id",
                table: "cart_items",
                column: "produce_listing_id");

            migrationBuilder.CreateIndex(
                name: "ix_carts_user_id",
                table: "carts",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_conversation_participants_conversation_id_user_id",
                table: "conversation_participants",
                columns: new[] { "conversation_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_conversation_participants_user_id",
                table: "conversation_participants",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_conversations_order_id",
                table: "conversations",
                column: "order_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_deliveries_delivery_code",
                table: "deliveries",
                column: "delivery_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_deliveries_vehicle_type_id",
                table: "deliveries",
                column: "vehicle_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_delivery_assignments_delivery_id",
                table: "delivery_assignments",
                column: "delivery_id");

            migrationBuilder.CreateIndex(
                name: "ix_delivery_assignments_distributor_user_id",
                table: "delivery_assignments",
                column: "distributor_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_delivery_orders_delivery_id_order_id",
                table: "delivery_orders",
                columns: new[] { "delivery_id", "order_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_delivery_orders_order_id",
                table: "delivery_orders",
                column: "order_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_delivery_status_history_delivery_id",
                table: "delivery_status_history",
                column: "delivery_id");

            migrationBuilder.CreateIndex(
                name: "ix_delivery_status_history_triggered_by_user_id",
                table: "delivery_status_history",
                column: "triggered_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_distributor_profiles_user_id",
                table: "distributor_profiles",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_distributor_profiles_license_number",
                table: "distributor_profiles",
                column: "license_number",
                unique: true,
                filter: "\"license_number\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_escrow_transactions_farmer_payout_id",
                table: "escrow_transactions",
                column: "farmer_payout_id");

            migrationBuilder.CreateIndex(
                name: "ix_escrow_transactions_order_id",
                table: "escrow_transactions",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_escrow_transactions_payment_id",
                table: "escrow_transactions",
                column: "payment_id");

            migrationBuilder.CreateIndex(
                name: "ix_farmer_listing_vehicle_types_produce_listing_id_vehicle_typ~",
                table: "farmer_listing_vehicle_types",
                columns: new[] { "produce_listing_id", "vehicle_type_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_farmer_listing_vehicle_types_vehicle_type_id",
                table: "farmer_listing_vehicle_types",
                column: "vehicle_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_farmer_payouts_farmer_profile_id",
                table: "farmer_payouts",
                column: "farmer_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_farmer_payouts_order_id_farmer_profile_id",
                table: "farmer_payouts",
                columns: new[] { "order_id", "farmer_profile_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_farmer_profiles_user_id",
                table: "farmer_profiles",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_kyc_applications_role_id",
                table: "kyc_applications",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_kyc_applications_user_id",
                table: "kyc_applications",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_kyc_documents_kyc_application_id",
                table: "kyc_documents",
                column: "kyc_application_id");

            migrationBuilder.CreateIndex(
                name: "ix_kyc_reviews_kyc_application_id",
                table: "kyc_reviews",
                column: "kyc_application_id");

            migrationBuilder.CreateIndex(
                name: "ix_kyc_reviews_reviewed_by_user_id",
                table: "kyc_reviews",
                column: "reviewed_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_listing_availability_windows_produce_listing_id",
                table: "listing_availability_windows",
                column: "produce_listing_id");

            migrationBuilder.CreateIndex(
                name: "ix_listing_price_history_changed_by_user_id",
                table: "listing_price_history",
                column: "changed_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_listing_price_history_produce_listing_id",
                table: "listing_price_history",
                column: "produce_listing_id");

            migrationBuilder.CreateIndex(
                name: "ix_messages_conversation_id",
                table: "messages",
                column: "conversation_id");

            migrationBuilder.CreateIndex(
                name: "ix_messages_sender_user_id",
                table: "messages",
                column: "sender_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_cancellations_cancelled_by_user_id",
                table: "order_cancellations",
                column: "cancelled_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_cancellations_order_id",
                table: "order_cancellations",
                column: "order_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_order_items_farmer_profile_id",
                table: "order_items",
                column: "farmer_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_order_id",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_produce_listing_id",
                table: "order_items",
                column: "produce_listing_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_status_history_order_id",
                table: "order_status_history",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_status_history_triggered_by_user_id",
                table: "order_status_history",
                column: "triggered_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_buyer_user_id",
                table: "orders",
                column: "buyer_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_order_number",
                table: "orders",
                column: "order_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_payments_order_id",
                table: "payments",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_produce_categories_category_name",
                table: "produce_categories",
                column: "category_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_produce_inventory_batches_listing_batch_code",
                table: "produce_inventory_batches",
                columns: new[] { "produce_listing_id", "batch_code" },
                unique: true,
                filter: "\"batch_code\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_produce_listing_images_produce_listing_id_display_order",
                table: "produce_listing_images",
                columns: new[] { "produce_listing_id", "display_order" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_produce_listings_farmer_profile_id",
                table: "produce_listings",
                column: "farmer_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_produce_listings_produce_category_id",
                table: "produce_listings",
                column: "produce_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_qa_issue_flags_qa_report_id",
                table: "qa_issue_flags",
                column: "qa_report_id");

            migrationBuilder.CreateIndex(
                name: "ix_qa_report_images_qa_report_id_display_order",
                table: "qa_report_images",
                columns: new[] { "qa_report_id", "display_order" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_qa_reports_delivery_id",
                table: "qa_reports",
                column: "delivery_id");

            migrationBuilder.CreateIndex(
                name: "ix_qa_reports_order_id",
                table: "qa_reports",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_qa_reports_submitted_by_user_id",
                table: "qa_reports",
                column: "submitted_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_order_id",
                table: "reviews",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_produce_listing_id",
                table: "reviews",
                column: "produce_listing_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_reviewer_user_id",
                table: "reviews",
                column: "reviewer_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_roles_role_code",
                table: "roles",
                column: "role_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_roles_role_name",
                table: "roles",
                column: "role_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_role_id",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_user_id_role_id",
                table: "user_roles",
                columns: new[] { "user_id", "role_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_users_email",
                table: "users",
                column: "email",
                unique: true,
                filter: "\"email\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "uq_users_mobile_number",
                table: "users",
                column: "mobile_number",
                unique: true,
                filter: "\"mobile_number\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_vehicle_types_vehicle_type_name",
                table: "vehicle_types",
                column: "vehicle_type_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_wallet_transactions_escrow_transaction_id",
                table: "wallet_transactions",
                column: "escrow_transaction_id");

            migrationBuilder.CreateIndex(
                name: "ix_wallet_transactions_farmer_payout_id",
                table: "wallet_transactions",
                column: "farmer_payout_id");

            migrationBuilder.CreateIndex(
                name: "ix_wallet_transactions_payment_id",
                table: "wallet_transactions",
                column: "payment_id");

            migrationBuilder.CreateIndex(
                name: "ix_wallet_transactions_wallet_id",
                table: "wallet_transactions",
                column: "wallet_id");

            migrationBuilder.CreateIndex(
                name: "ix_wallets_user_id",
                table: "wallets",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audit_logs");

            migrationBuilder.DropTable(
                name: "buyer_profiles");

            migrationBuilder.DropTable(
                name: "cart_items");

            migrationBuilder.DropTable(
                name: "conversation_participants");

            migrationBuilder.DropTable(
                name: "delivery_assignments");

            migrationBuilder.DropTable(
                name: "delivery_orders");

            migrationBuilder.DropTable(
                name: "delivery_status_history");

            migrationBuilder.DropTable(
                name: "distributor_profiles");

            migrationBuilder.DropTable(
                name: "farmer_listing_vehicle_types");

            migrationBuilder.DropTable(
                name: "kyc_documents");

            migrationBuilder.DropTable(
                name: "kyc_reviews");

            migrationBuilder.DropTable(
                name: "listing_availability_windows");

            migrationBuilder.DropTable(
                name: "listing_price_history");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "order_cancellations");

            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "order_status_history");

            migrationBuilder.DropTable(
                name: "produce_inventory_batches");

            migrationBuilder.DropTable(
                name: "produce_listing_images");

            migrationBuilder.DropTable(
                name: "qa_issue_flags");

            migrationBuilder.DropTable(
                name: "qa_report_images");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "wallet_transactions");

            migrationBuilder.DropTable(
                name: "carts");

            migrationBuilder.DropTable(
                name: "kyc_applications");

            migrationBuilder.DropTable(
                name: "conversations");

            migrationBuilder.DropTable(
                name: "qa_reports");

            migrationBuilder.DropTable(
                name: "produce_listings");

            migrationBuilder.DropTable(
                name: "escrow_transactions");

            migrationBuilder.DropTable(
                name: "wallets");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "deliveries");

            migrationBuilder.DropTable(
                name: "produce_categories");

            migrationBuilder.DropTable(
                name: "farmer_payouts");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "vehicle_types");

            migrationBuilder.DropTable(
                name: "farmer_profiles");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
