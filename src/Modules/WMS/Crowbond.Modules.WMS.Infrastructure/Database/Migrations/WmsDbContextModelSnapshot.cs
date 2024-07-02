﻿// <auto-generated />
using System;
using Crowbond.Modules.WMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Crowbond.Modules.WMS.Infrastructure.Database.Migrations
{
    [DbContext(typeof(WmsDbContext))]
    partial class WmsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("wms")
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

                    b.ToTable("inbox_messages", "wms");
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

                    b.ToTable("inbox_message_consumers", "wms");
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

                    b.ToTable("outbox_messages", "wms");
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

                    b.ToTable("outbox_message_consumers", "wms");
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Categories.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean")
                        .HasColumnName("is_archived");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_categories");

                    b.ToTable("categories", "wms");
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Locations.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("LocationTypeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("location_type_name");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid")
                        .HasColumnName("parent_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_locations");

                    b.HasIndex("LocationTypeName")
                        .HasDatabaseName("ix_locations_location_type_name");

                    b.HasIndex("ParentId")
                        .HasDatabaseName("ix_locations_parent_id");

                    b.ToTable("locations", "wms");
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Locations.LocationType", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Name")
                        .HasName("pk_locations_type");

                    b.ToTable("locations_type", "wms");

                    b.HasData(
                        new
                        {
                            Name = "Site"
                        },
                        new
                        {
                            Name = "Area"
                        },
                        new
                        {
                            Name = "Location"
                        });
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Products.FilterType", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("name");

                    b.HasKey("Name")
                        .HasName("pk_filter_types");

                    b.ToTable("filter_types", "wms");

                    b.HasData(
                        new
                        {
                            Name = "Case"
                        },
                        new
                        {
                            Name = "Box"
                        },
                        new
                        {
                            Name = "Each"
                        },
                        new
                        {
                            Name = "Kg"
                        },
                        new
                        {
                            Name = "Processed"
                        });
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Products.InventoryType", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("name");

                    b.HasKey("Name")
                        .HasName("pk_inventory_types");

                    b.ToTable("inventory_types", "wms");

                    b.HasData(
                        new
                        {
                            Name = "Exclusive"
                        },
                        new
                        {
                            Name = "Standard"
                        });
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean")
                        .HasColumnName("active");

                    b.Property<int?>("Barcode")
                        .HasColumnType("integer")
                        .HasColumnName("barcode");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<string>("FilterTypeName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("filter_type_name");

                    b.Property<string>("HandlingNotes")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("handling_notes");

                    b.Property<decimal?>("Height")
                        .HasPrecision(19)
                        .HasColumnType("numeric(19,0)")
                        .HasColumnName("height");

                    b.Property<string>("InventoryTypeName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("inventory_type_name");

                    b.Property<decimal?>("Length")
                        .HasPrecision(19)
                        .HasColumnType("numeric(19,0)")
                        .HasColumnName("length");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<string>("Notes")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("notes");

                    b.Property<decimal?>("PackSize")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("pack_size");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid")
                        .HasColumnName("parent_id");

                    b.Property<bool>("QiCheck")
                        .HasColumnType("boolean")
                        .HasColumnName("qi_check");

                    b.Property<decimal?>("ReorderLevel")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("reorder_level");

                    b.Property<string>("Sku")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("sku");

                    b.Property<string>("UnitOfMeasureName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("unit_of_measure_name");

                    b.Property<bool>("WeightInput")
                        .HasColumnType("boolean")
                        .HasColumnName("weight_input");

                    b.Property<decimal?>("Width")
                        .HasPrecision(19)
                        .HasColumnType("numeric(19,0)")
                        .HasColumnName("width");

                    b.HasKey("Id")
                        .HasName("pk_products");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("ix_products_category_id");

                    b.HasIndex("FilterTypeName")
                        .HasDatabaseName("ix_products_filter_type_name");

                    b.HasIndex("InventoryTypeName")
                        .HasDatabaseName("ix_products_inventory_type_name");

                    b.HasIndex("ParentId")
                        .HasDatabaseName("ix_products_parent_id");

                    b.HasIndex("UnitOfMeasureName")
                        .HasDatabaseName("ix_products_unit_of_measure_name");

                    b.ToTable("products", "wms");
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Products.UnitOfMeasure", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("name");

                    b.HasKey("Name")
                        .HasName("pk_unit_of_measures");

                    b.ToTable("unit_of_measures", "wms");

                    b.HasData(
                        new
                        {
                            Name = "Bag"
                        },
                        new
                        {
                            Name = "Box"
                        },
                        new
                        {
                            Name = "Each"
                        },
                        new
                        {
                            Name = "Kg"
                        },
                        new
                        {
                            Name = "Pack"
                        });
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Receipts.ReceiptHeader", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatetimeStamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createtime_stamp");

                    b.Property<string>("DeliveryNoteNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("delivery_note_number");

                    b.Property<Guid>("PurchaseOrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("purchase_order_id");

                    b.Property<DateTime>("ReceivedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("received_date");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_receipt_headers");

                    b.ToTable("receipt_headers", "wms");
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Receipts.ReceiptLine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("BatchNumber")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("batch_number");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<decimal>("QuantityReceived")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("quantity_received");

                    b.Property<Guid>("ReceiptHeaderId")
                        .HasColumnType("uuid")
                        .HasColumnName("receipt_header_id");

                    b.Property<DateOnly?>("SellByDate")
                        .HasColumnType("date")
                        .HasColumnName("sell_by_date");

                    b.Property<decimal>("UnitPrice")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("unit_price");

                    b.Property<DateOnly?>("UseByDate")
                        .HasColumnType("date")
                        .HasColumnName("use_by_date");

                    b.HasKey("Id")
                        .HasName("pk_receipt_lines");

                    b.HasIndex("ReceiptHeaderId")
                        .HasDatabaseName("ix_receipt_lines_receipt_header_id");

                    b.ToTable("receipt_lines", "wms");
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Settings.Setting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<bool>("HasMixBatchLocation")
                        .HasColumnType("boolean")
                        .HasColumnName("has_mix_batch_location");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.HasKey("Id")
                        .HasName("pk_settings");

                    b.ToTable("settings", "wms");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c1f962a8-5ad6-4a3a-9921-d085f31c4c5c"),
                            CreatedDate = new DateTime(2024, 7, 2, 11, 26, 46, 786, DateTimeKind.Utc).AddTicks(1521),
                            HasMixBatchLocation = false,
                            IsActive = true
                        });
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Stocks.ActionType", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Name")
                        .HasName("pk_action_types");

                    b.ToTable("action_types", "wms");

                    b.HasData(
                        new
                        {
                            Name = "Adjustment"
                        },
                        new
                        {
                            Name = "Relocating"
                        });
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Stocks.Stock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("BatchNumber")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("batch_number");

                    b.Property<decimal>("CurrentQty")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("current_qty");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uuid")
                        .HasColumnName("location_id");

                    b.Property<string>("Note")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("note");

                    b.Property<decimal>("OriginalQty")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("original_qty");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<Guid>("ReceiptId")
                        .HasColumnType("uuid")
                        .HasColumnName("receipt_id");

                    b.Property<DateTime>("ReceivedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("received_date");

                    b.Property<DateTime?>("SellByDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("sell_by_date");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<DateTime?>("UseByDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("use_by_date");

                    b.HasKey("Id")
                        .HasName("pk_stocks");

                    b.HasIndex("LocationId")
                        .HasDatabaseName("ix_stocks_location_id");

                    b.HasIndex("ReceiptId")
                        .HasDatabaseName("ix_stocks_receipt_id");

                    b.ToTable("stocks", "wms");
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Stocks.StockTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("ActionTypeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("action_type_name");

                    b.Property<bool>("PosAdjustment")
                        .HasColumnType("boolean")
                        .HasColumnName("pos_adjustment");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<decimal>("Quantity")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("quantity");

                    b.Property<Guid?>("ReasonId")
                        .HasColumnType("uuid")
                        .HasColumnName("reason_id");

                    b.Property<Guid>("StockId")
                        .HasColumnType("uuid")
                        .HasColumnName("stock_id");

                    b.Property<Guid?>("TaskId")
                        .HasColumnType("uuid")
                        .HasColumnName("task_id");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("transaction_date");

                    b.Property<string>("TransactionNote")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("transaction_note");

                    b.HasKey("Id")
                        .HasName("pk_stock_transactions");

                    b.HasIndex("ActionTypeName")
                        .HasDatabaseName("ix_stock_transactions_action_type_name");

                    b.HasIndex("ReasonId")
                        .HasDatabaseName("ix_stock_transactions_reason_id");

                    b.HasIndex("StockId")
                        .HasDatabaseName("ix_stock_transactions_stock_id");

                    b.HasIndex("TaskId")
                        .HasDatabaseName("ix_stock_transactions_task_id");

                    b.ToTable("stock_transactions", "wms");
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Stocks.StockTransactionReason", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("ActionTypeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("action_type_name");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_stock_transaction_reasons");

                    b.HasIndex("ActionTypeName")
                        .HasDatabaseName("ix_stock_transaction_reasons_action_type_name");

                    b.ToTable("stock_transaction_reasons", "wms");
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Tasks.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("TaskTypeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("task_type_name");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_tasks");

                    b.HasIndex("TaskTypeName")
                        .HasDatabaseName("ix_tasks_task_type_name");

                    b.ToTable("tasks", "wms");
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Tasks.TaskType", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Name")
                        .HasName("pk_task_types");

                    b.ToTable("task_types", "wms");
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Locations.Location", b =>
                {
                    b.HasOne("Crowbond.Modules.WMS.Domain.Locations.LocationType", null)
                        .WithMany()
                        .HasForeignKey("LocationTypeName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_locations_locations_type_location_type_name");

                    b.HasOne("Crowbond.Modules.WMS.Domain.Locations.Location", null)
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .HasConstraintName("fk_locations_locations_parent_id");
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Products.Product", b =>
                {
                    b.HasOne("Crowbond.Modules.WMS.Domain.Categories.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_products_categories_category_id");

                    b.HasOne("Crowbond.Modules.WMS.Domain.Products.FilterType", null)
                        .WithMany()
                        .HasForeignKey("FilterTypeName")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_products_filter_types_filter_type_name");

                    b.HasOne("Crowbond.Modules.WMS.Domain.Products.InventoryType", null)
                        .WithMany()
                        .HasForeignKey("InventoryTypeName")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_products_inventory_types_inventory_type_name");

                    b.HasOne("Crowbond.Modules.WMS.Domain.Products.Product", null)
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_products_products_parent_id");

                    b.HasOne("Crowbond.Modules.WMS.Domain.Products.UnitOfMeasure", null)
                        .WithMany()
                        .HasForeignKey("UnitOfMeasureName")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_products_unit_of_measures_unit_of_measure_name");
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Receipts.ReceiptLine", b =>
                {
                    b.HasOne("Crowbond.Modules.WMS.Domain.Receipts.ReceiptHeader", null)
                        .WithMany()
                        .HasForeignKey("ReceiptHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_receipt_lines_receipt_headers_receipt_header_id");
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Stocks.Stock", b =>
                {
                    b.HasOne("Crowbond.Modules.WMS.Domain.Locations.Location", null)
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_stocks_locations_location_id");

                    b.HasOne("Crowbond.Modules.WMS.Domain.Receipts.ReceiptLine", null)
                        .WithMany()
                        .HasForeignKey("ReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_stocks_receipt_lines_receipt_id");
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Stocks.StockTransaction", b =>
                {
                    b.HasOne("Crowbond.Modules.WMS.Domain.Stocks.ActionType", null)
                        .WithMany()
                        .HasForeignKey("ActionTypeName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_stock_transactions_action_types_action_type_name");

                    b.HasOne("Crowbond.Modules.WMS.Domain.Stocks.StockTransactionReason", null)
                        .WithMany()
                        .HasForeignKey("ReasonId")
                        .HasConstraintName("fk_stock_transactions_stock_transaction_reasons_reason_id");

                    b.HasOne("Crowbond.Modules.WMS.Domain.Stocks.Stock", null)
                        .WithMany()
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_stock_transactions_stocks_stock_id");

                    b.HasOne("Crowbond.Modules.WMS.Domain.Tasks.Task", null)
                        .WithMany()
                        .HasForeignKey("TaskId")
                        .HasConstraintName("fk_stock_transactions_tasks_task_id");
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Stocks.StockTransactionReason", b =>
                {
                    b.HasOne("Crowbond.Modules.WMS.Domain.Stocks.ActionType", null)
                        .WithMany()
                        .HasForeignKey("ActionTypeName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_stock_transaction_reasons_action_types_action_type_name");
                });

            modelBuilder.Entity("Crowbond.Modules.WMS.Domain.Tasks.Task", b =>
                {
                    b.HasOne("Crowbond.Modules.WMS.Domain.Tasks.TaskType", null)
                        .WithMany()
                        .HasForeignKey("TaskTypeName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tasks_task_types_task_type_name");
                });
#pragma warning restore 612, 618
        }
    }
}
