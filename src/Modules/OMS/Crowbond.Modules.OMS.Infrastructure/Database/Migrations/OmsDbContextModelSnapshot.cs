﻿// <auto-generated />
using System;
using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Crowbond.Modules.OMS.Infrastructure.Database.Migrations
{
    [DbContext(typeof(OmsDbContext))]
    partial class OmsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("oms")
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Crowbond.Common.Infrastructure.Inbox.InboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("jsonb")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("occurred_on_utc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("processed_on_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_inbox_messages");

                    b.ToTable("inbox_messages", "oms");
                });

            modelBuilder.Entity("Crowbond.Common.Infrastructure.Inbox.InboxMessageConsumer", b =>
                {
                    b.Property<Guid>("InboxMessageId")
                        .HasColumnType("uuid")
                        .HasColumnName("inbox_message_id");

                    b.Property<string>("Name")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("name");

                    b.HasKey("InboxMessageId", "Name")
                        .HasName("pk_inbox_message_consumers");

                    b.ToTable("inbox_message_consumers", "oms");
                });

            modelBuilder.Entity("Crowbond.Common.Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("jsonb")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("occurred_on_utc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("processed_on_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_outbox_messages");

                    b.ToTable("outbox_messages", "oms");
                });

            modelBuilder.Entity("Crowbond.Common.Infrastructure.Outbox.OutboxMessageConsumer", b =>
                {
                    b.Property<Guid>("OutboxMessageId")
                        .HasColumnType("uuid")
                        .HasColumnName("outbox_message_id");

                    b.Property<string>("Name")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("name");

                    b.HasKey("OutboxMessageId", "Name")
                        .HasName("pk_outbox_message_consumers");

                    b.ToTable("outbox_message_consumers", "oms");
                });

            modelBuilder.Entity("Crowbond.Modules.OMS.Domain.Orders.OrderHeader", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("CustomerComment")
                        .HasMaxLength(3000)
                        .HasColumnType("character varying(3000)")
                        .HasColumnName("customer_comment");

                    b.Property<string>("CustomerEmail")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("customer_email");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid")
                        .HasColumnName("customer_id");

                    b.Property<string>("CustomerMobile")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("customer_mobile");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("customer_name");

                    b.Property<decimal>("DeliveryCharge")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("delivery_charge");

                    b.Property<int?>("DeliveryMethod")
                        .HasColumnType("integer")
                        .HasColumnName("delivery_method");

                    b.Property<string>("ExternalOrderRef")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("external_order_ref");

                    b.Property<string>("InvoiceNo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("invoice_no");

                    b.Property<string>("InvoicedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("invoiced_by");

                    b.Property<DateTime?>("InvoicedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("invoiced_date");

                    b.Property<decimal>("OrderAmount")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("order_amount");

                    b.Property<decimal>("OrderTax")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("order_tax");

                    b.Property<string>("OriginalSource")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("original_source");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("integer")
                        .HasColumnName("payment_method");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("integer")
                        .HasColumnName("payment_status");

                    b.Property<string>("PurchaseOrderNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("purchase_order_number");

                    b.Property<Guid>("RouteId")
                        .HasColumnType("uuid")
                        .HasColumnName("route_id");

                    b.Property<string>("SalesOrderNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("sales_order_number");

                    b.Property<string>("ShippingAddressCompany")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("shipping_address_company");

                    b.Property<string>("ShippingAddressCountry")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("shipping_address_country");

                    b.Property<string>("ShippingAddressCounty")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("shipping_address_county");

                    b.Property<string>("ShippingAddressLine1")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("shipping_address_line1");

                    b.Property<string>("ShippingAddressLine2")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("shipping_address_line2");

                    b.Property<string>("ShippingAddressPostalCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("shipping_address_postal_code");

                    b.Property<string>("ShippingAddressTownCity")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("shipping_address_town_city");

                    b.Property<DateOnly>("ShippingDate")
                        .HasColumnType("date")
                        .HasColumnName("shipping_date");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("tags");

                    b.HasKey("Id")
                        .HasName("pk_order_headers");

                    b.ToTable("order_headers", "oms");
                });

            modelBuilder.Entity("Crowbond.Modules.OMS.Domain.Orders.OrderLine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("FOC")
                        .HasColumnType("boolean")
                        .HasColumnName("foc");

                    b.Property<decimal>("LineTotal")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("line_total");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("product_name");

                    b.Property<string>("ProductSku")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("product_sku");

                    b.Property<int>("Qty")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("qty");

                    b.Property<decimal>("SubTotal")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("sub_total");

                    b.Property<decimal>("Tax")
                        .HasPrecision(5, 2)
                        .HasColumnType("numeric(5,2)")
                        .HasColumnName("tax");

                    b.Property<bool>("Taxable")
                        .HasColumnType("boolean")
                        .HasColumnName("taxable");

                    b.Property<decimal>("UnitPrice")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("unit_price");

                    b.HasKey("Id")
                        .HasName("pk_order_lines");

                    b.HasIndex("OrderId")
                        .HasDatabaseName("ix_order_lines_order_id");

                    b.ToTable("order_lines", "oms");
                });

            modelBuilder.Entity("Crowbond.Modules.OMS.Domain.Orders.OrderLine", b =>
                {
                    b.HasOne("Crowbond.Modules.OMS.Domain.Orders.OrderHeader", null)
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_lines_order_headers_order_id");
                });
#pragma warning restore 612, 618
        }
    }
}
