﻿// <auto-generated />
using System;
using Crowbond.Modules.Products.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Crowbond.Modules.Products.Infrastructure.Database.Migrations
{
    [DbContext(typeof(ProductsDbContext))]
    [Migration("20240601030747_Create_Database")]
    partial class Create_Database
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("products")
                .HasAnnotation("ProductVersion", "8.0.4")
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

                    b.ToTable("inbox_messages", "products");
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

                    b.ToTable("inbox_message_consumers", "products");
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

                    b.ToTable("outbox_messages", "products");
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

                    b.ToTable("outbox_message_consumers", "products");
                });

            modelBuilder.Entity("Crowbond.Modules.Products.Domain.Categories.Category", b =>
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
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_categories");

                    b.ToTable("categories", "products");
                });

            modelBuilder.Entity("Crowbond.Modules.Products.Domain.Products.FilterType", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("name");

                    b.HasKey("Name")
                        .HasName("pk_filter_types");

                    b.ToTable("filter_types", "products");

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

            modelBuilder.Entity("Crowbond.Modules.Products.Domain.Products.InventoryType", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("name");

                    b.HasKey("Name")
                        .HasName("pk_inventory_types");

                    b.ToTable("inventory_types", "products");

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

            modelBuilder.Entity("Crowbond.Modules.Products.Domain.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean")
                        .HasColumnName("active");

                    b.Property<int>("Barcode")
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

                    b.Property<int>("PackSize")
                        .HasColumnType("integer")
                        .HasColumnName("pack_size");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid")
                        .HasColumnName("parent_id");

                    b.Property<bool>("QiCheck")
                        .HasColumnType("boolean")
                        .HasColumnName("qi_check");

                    b.Property<int>("ReorderLevel")
                        .HasColumnType("integer")
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

                    b.ToTable("products", "products");
                });

            modelBuilder.Entity("Crowbond.Modules.Products.Domain.Products.UnitOfMeasure", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("name");

                    b.HasKey("Name")
                        .HasName("pk_unit_of_measures");

                    b.ToTable("unit_of_measures", "products");

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

            modelBuilder.Entity("Crowbond.Modules.Products.Domain.Products.Product", b =>
                {
                    b.HasOne("Crowbond.Modules.Products.Domain.Categories.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_products_category_category_id");

                    b.HasOne("Crowbond.Modules.Products.Domain.Products.FilterType", null)
                        .WithMany()
                        .HasForeignKey("FilterTypeName")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_products_filter_types_filter_type_name");

                    b.HasOne("Crowbond.Modules.Products.Domain.Products.InventoryType", null)
                        .WithMany()
                        .HasForeignKey("InventoryTypeName")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_products_inventory_types_inventory_type_name");

                    b.HasOne("Crowbond.Modules.Products.Domain.Products.Product", null)
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_products_products_parent_id");

                    b.HasOne("Crowbond.Modules.Products.Domain.Products.UnitOfMeasure", null)
                        .WithMany()
                        .HasForeignKey("UnitOfMeasureName")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_products_unit_of_measures_unit_of_measure_name");
                });
#pragma warning restore 612, 618
        }
    }
}
