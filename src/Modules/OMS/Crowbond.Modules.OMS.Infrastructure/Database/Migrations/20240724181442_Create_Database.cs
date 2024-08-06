﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.OMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Create_Database : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "oms");

        migrationBuilder.CreateTable(
            name: "drivers",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                mobile = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                vehicle_regn = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_drivers", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "inbox_message_consumers",
            schema: "oms",
            columns: table => new
            {
                inbox_message_id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_inbox_message_consumers", x => new { x.inbox_message_id, x.name });
            });

        migrationBuilder.CreateTable(
            name: "inbox_messages",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                type = table.Column<string>(type: "text", nullable: false),
                content = table.Column<string>(type: "jsonb", maxLength: 2000, nullable: false),
                occurred_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                processed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                error = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_inbox_messages", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "outbox_message_consumers",
            schema: "oms",
            columns: table => new
            {
                outbox_message_id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_outbox_message_consumers", x => new { x.outbox_message_id, x.name });
            });

        migrationBuilder.CreateTable(
            name: "outbox_messages",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                type = table.Column<string>(type: "text", nullable: false),
                content = table.Column<string>(type: "jsonb", maxLength: 2000, nullable: false),
                occurred_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                processed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                error = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_outbox_messages", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "purchase_order_headers",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                purchase_order_no = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                paid_by = table.Column<string>(type: "text", nullable: true),
                paid_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                supplier_id = table.Column<Guid>(type: "uuid", nullable: false),
                supplier_name = table.Column<string>(type: "text", nullable: false),
                supplier_phone = table.Column<string>(type: "text", nullable: false),
                supplier_email = table.Column<string>(type: "text", nullable: false),
                supplier_contact = table.Column<string>(type: "text", nullable: false),
                purchase_order_amount = table.Column<decimal>(type: "numeric", nullable: false),
                location_name = table.Column<string>(type: "text", nullable: false),
                shipping_address_line1 = table.Column<string>(type: "text", nullable: false),
                shipping_address_line2 = table.Column<string>(type: "text", nullable: true),
                shipping_address_town_city = table.Column<string>(type: "text", nullable: false),
                shipping_address_county = table.Column<string>(type: "text", nullable: true),
                shipping_address_country = table.Column<string>(type: "text", nullable: true),
                shipping_address_postal_code = table.Column<string>(type: "text", nullable: false),
                required_date = table.Column<DateOnly>(type: "date", nullable: false),
                purchase_date = table.Column<DateOnly>(type: "date", nullable: false),
                expected_shipping_date = table.Column<DateOnly>(type: "date", nullable: false),
                supplier_reference = table.Column<string>(type: "text", nullable: true),
                purchase_order_tax = table.Column<decimal>(type: "numeric", nullable: false),
                delivery_method = table.Column<int>(type: "integer", nullable: true),
                delivery_charge = table.Column<decimal>(type: "numeric", nullable: false),
                payment_method = table.Column<int>(type: "integer", nullable: false),
                payment_status = table.Column<int>(type: "integer", nullable: false),
                purchase_order_notes = table.Column<string>(type: "text", nullable: true),
                sales_order_ref = table.Column<string>(type: "text", nullable: false),
                tags = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_purchase_order_headers", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "routes",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                position = table.Column<int>(type: "integer", nullable: false),
                cut_off_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                is_active = table.Column<bool>(type: "boolean", nullable: false),
                days_of_week = table.Column<string>(type: "CHAR(7)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_routes", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "sequences",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                context = table.Column<int>(type: "integer", nullable: false),
                last_number = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_sequences", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "purchase_order_lines",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                purchase_order_id = table.Column<Guid>(type: "uuid", nullable: false),
                product_id = table.Column<Guid>(type: "uuid", nullable: false),
                product_sku = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                product_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                unit_price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                qty = table.Column<int>(type: "integer", precision: 10, scale: 2, nullable: false),
                sub_total = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                tax = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                line_total = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                foc = table.Column<bool>(type: "boolean", nullable: false),
                taxable = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_purchase_order_lines", x => x.id);
                table.ForeignKey(
                    name: "fk_purchase_order_lines_purchase_order_headers_purchase_order_",
                    column: x => x.purchase_order_id,
                    principalSchema: "oms",
                    principalTable: "purchase_order_headers",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "route_trips",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                date = table.Column<DateOnly>(type: "date", nullable: false),
                route_id = table.Column<Guid>(type: "uuid", nullable: false),
                comments = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                status = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_route_trips", x => x.id);
                table.ForeignKey(
                    name: "fk_route_trips_routes_route_id",
                    column: x => x.route_id,
                    principalSchema: "oms",
                    principalTable: "routes",
                    principalColumn: "id");
            });

        migrationBuilder.CreateTable(
            name: "order_headers",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                order_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                purchase_order_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                customer_business_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                delivery_location_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                delivery_full_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                delivery_email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                delivery_phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                delivery_mobile = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                delivery_notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                delivery_address_line1 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                delivery_address_line2 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                delivery_town_city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                delivery_county = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                delivery_country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                delivery_postal_code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                shipping_date = table.Column<DateOnly>(type: "date", nullable: false),
                route_trip_id = table.Column<Guid>(type: "uuid", nullable: true),
                route_name = table.Column<string>(type: "text", nullable: true),
                delivery_method = table.Column<int>(type: "integer", nullable: false),
                delivery_charge = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                order_amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                order_tax = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                payment_status = table.Column<int>(type: "integer", nullable: false),
                payment_method = table.Column<int>(type: "integer", nullable: false),
                customer_comment = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: true),
                original_source = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                external_order_ref = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                tags = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                status = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_order_headers", x => x.id);
                table.ForeignKey(
                    name: "fk_order_headers_route_trips_route_trip_id",
                    column: x => x.route_trip_id,
                    principalSchema: "oms",
                    principalTable: "route_trips",
                    principalColumn: "id");
            });

        migrationBuilder.CreateTable(
            name: "route_trip_logs",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                route_trip_id = table.Column<Guid>(type: "uuid", nullable: false),
                driver_id = table.Column<Guid>(type: "uuid", nullable: false),
                vehicle_regn = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                logged_on_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                logged_off_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                temperature = table.Column<decimal>(type: "numeric(2,2)", precision: 2, scale: 2, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_route_trip_logs", x => x.id);
                table.ForeignKey(
                    name: "fk_route_trip_logs_drivers_driver_id",
                    column: x => x.driver_id,
                    principalSchema: "oms",
                    principalTable: "drivers",
                    principalColumn: "id");
                table.ForeignKey(
                    name: "fk_route_trip_logs_route_trips_route_trip_id",
                    column: x => x.route_trip_id,
                    principalSchema: "oms",
                    principalTable: "route_trips",
                    principalColumn: "id");
            });

        migrationBuilder.CreateTable(
            name: "route_trip_status_histories",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                route_trip_id = table.Column<Guid>(type: "uuid", nullable: false),
                status = table.Column<int>(type: "integer", nullable: false),
                changed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                changed_by = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_route_trip_status_histories", x => x.id);
                table.ForeignKey(
                    name: "fk_route_trip_status_histories_route_trips_route_trip_id",
                    column: x => x.route_trip_id,
                    principalSchema: "oms",
                    principalTable: "route_trips",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "order_lines",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                order_id = table.Column<Guid>(type: "uuid", nullable: false),
                product_id = table.Column<Guid>(type: "uuid", nullable: false),
                product_sku = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                product_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                unit_of_measure_name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                unit_price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                qty = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                sub_total = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                tax = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                line_total = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                foc = table.Column<bool>(type: "boolean", nullable: false),
                taxable = table.Column<bool>(type: "boolean", nullable: false),
                status = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_order_lines", x => x.id);
                table.ForeignKey(
                    name: "fk_order_lines_order_headers_order_id",
                    column: x => x.order_id,
                    principalSchema: "oms",
                    principalTable: "order_headers",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "order_status_histories",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                order_header_id = table.Column<Guid>(type: "uuid", nullable: false),
                status = table.Column<int>(type: "integer", nullable: false),
                changed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                changed_by = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_order_status_histories", x => x.id);
                table.ForeignKey(
                    name: "fk_order_status_histories_order_headers_order_header_id",
                    column: x => x.order_header_id,
                    principalSchema: "oms",
                    principalTable: "order_headers",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "route_trip_log_datails",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                route_trip_log_id = table.Column<Guid>(type: "uuid", nullable: false),
                order_header_id = table.Column<Guid>(type: "uuid", nullable: false),
                status = table.Column<int>(type: "integer", nullable: false),
                date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_route_trip_log_datails", x => x.id);
                table.ForeignKey(
                    name: "fk_route_trip_log_datails_order_headers_order_header_id",
                    column: x => x.order_header_id,
                    principalSchema: "oms",
                    principalTable: "order_headers",
                    principalColumn: "id");
                table.ForeignKey(
                    name: "fk_route_trip_log_datails_route_trip_logs_route_trip_log_id",
                    column: x => x.route_trip_log_id,
                    principalSchema: "oms",
                    principalTable: "route_trip_logs",
                    principalColumn: "id");
            });

        migrationBuilder.CreateTable(
            name: "deliveries",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                route_trip_log_detail_id = table.Column<Guid>(type: "uuid", nullable: false),
                date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                comments = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_deliveries", x => x.id);
                table.ForeignKey(
                    name: "fk_deliveries_route_trip_log_datails_route_trip_log_detail_id",
                    column: x => x.route_trip_log_detail_id,
                    principalSchema: "oms",
                    principalTable: "route_trip_log_datails",
                    principalColumn: "id");
            });

        migrationBuilder.CreateTable(
            name: "delivery_images",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                delivery_id = table.Column<Guid>(type: "uuid", nullable: false),
                image_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_delivery_images", x => x.id);
                table.ForeignKey(
                    name: "fk_delivery_images_deliveries_delivery_id",
                    column: x => x.delivery_id,
                    principalSchema: "oms",
                    principalTable: "deliveries",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_deliveries_route_trip_log_detail_id",
            schema: "oms",
            table: "deliveries",
            column: "route_trip_log_detail_id");

        migrationBuilder.CreateIndex(
            name: "ix_delivery_images_delivery_id",
            schema: "oms",
            table: "delivery_images",
            column: "delivery_id");

        migrationBuilder.CreateIndex(
            name: "ix_drivers_email",
            schema: "oms",
            table: "drivers",
            column: "email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_drivers_username",
            schema: "oms",
            table: "drivers",
            column: "username",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_order_headers_route_trip_id",
            schema: "oms",
            table: "order_headers",
            column: "route_trip_id");

        migrationBuilder.CreateIndex(
            name: "ix_order_lines_order_id",
            schema: "oms",
            table: "order_lines",
            column: "order_id");

        migrationBuilder.CreateIndex(
            name: "ix_order_status_histories_order_header_id",
            schema: "oms",
            table: "order_status_histories",
            column: "order_header_id");

        migrationBuilder.CreateIndex(
            name: "ix_purchase_order_lines_purchase_order_id",
            schema: "oms",
            table: "purchase_order_lines",
            column: "purchase_order_id");

        migrationBuilder.CreateIndex(
            name: "ix_route_trip_log_datails_order_header_id",
            schema: "oms",
            table: "route_trip_log_datails",
            column: "order_header_id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_route_trip_log_datails_route_trip_log_id",
            schema: "oms",
            table: "route_trip_log_datails",
            column: "route_trip_log_id");

        migrationBuilder.CreateIndex(
            name: "ix_route_trip_logs_driver_id",
            schema: "oms",
            table: "route_trip_logs",
            column: "driver_id");

        migrationBuilder.CreateIndex(
            name: "ix_route_trip_logs_route_trip_id",
            schema: "oms",
            table: "route_trip_logs",
            column: "route_trip_id");

        migrationBuilder.CreateIndex(
            name: "ix_route_trip_status_histories_route_trip_id",
            schema: "oms",
            table: "route_trip_status_histories",
            column: "route_trip_id");

        migrationBuilder.CreateIndex(
            name: "ix_route_trips_route_id",
            schema: "oms",
            table: "route_trips",
            column: "route_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "delivery_images",
            schema: "oms");

        migrationBuilder.DropTable(
            name: "inbox_message_consumers",
            schema: "oms");

        migrationBuilder.DropTable(
            name: "inbox_messages",
            schema: "oms");

        migrationBuilder.DropTable(
            name: "order_lines",
            schema: "oms");

        migrationBuilder.DropTable(
            name: "order_status_histories",
            schema: "oms");

        migrationBuilder.DropTable(
            name: "outbox_message_consumers",
            schema: "oms");

        migrationBuilder.DropTable(
            name: "outbox_messages",
            schema: "oms");

        migrationBuilder.DropTable(
            name: "purchase_order_lines",
            schema: "oms");

        migrationBuilder.DropTable(
            name: "route_trip_status_histories",
            schema: "oms");

        migrationBuilder.DropTable(
            name: "sequences",
            schema: "oms");

        migrationBuilder.DropTable(
            name: "deliveries",
            schema: "oms");

        migrationBuilder.DropTable(
            name: "purchase_order_headers",
            schema: "oms");

        migrationBuilder.DropTable(
            name: "route_trip_log_datails",
            schema: "oms");

        migrationBuilder.DropTable(
            name: "order_headers",
            schema: "oms");

        migrationBuilder.DropTable(
            name: "route_trip_logs",
            schema: "oms");

        migrationBuilder.DropTable(
            name: "drivers",
            schema: "oms");

        migrationBuilder.DropTable(
            name: "route_trips",
            schema: "oms");

        migrationBuilder.DropTable(
            name: "routes",
            schema: "oms");
    }
}