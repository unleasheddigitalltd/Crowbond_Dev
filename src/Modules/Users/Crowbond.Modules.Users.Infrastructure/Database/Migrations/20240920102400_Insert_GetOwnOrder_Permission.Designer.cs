﻿// <auto-generated />
using System;
using Crowbond.Modules.Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Crowbond.Modules.Users.Infrastructure.Database.Migrations
{
    [DbContext(typeof(UsersDbContext))]
    [Migration("20240920102400_Insert_GetOwnOrder_Permission")]
    partial class Insert_GetOwnOrder_Permission
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("users")
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

                    b.ToTable("inbox_messages", "users");
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

                    b.ToTable("inbox_message_consumers", "users");
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

                    b.ToTable("outbox_messages", "users");
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

                    b.ToTable("outbox_message_consumers", "users");
                });

            modelBuilder.Entity("Crowbond.Modules.Users.Domain.Users.Permission", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("code");

                    b.HasKey("Code")
                        .HasName("pk_permissions");

                    b.ToTable("permissions", "users");

                    b.HasData(
                        new
                        {
                            Code = "users:read"
                        },
                        new
                        {
                            Code = "users:update"
                        },
                        new
                        {
                            Code = "events:read"
                        },
                        new
                        {
                            Code = "events:search"
                        },
                        new
                        {
                            Code = "events:update"
                        },
                        new
                        {
                            Code = "ticket-types:read"
                        },
                        new
                        {
                            Code = "ticket-types:update"
                        },
                        new
                        {
                            Code = "categories:read"
                        },
                        new
                        {
                            Code = "categories:update"
                        },
                        new
                        {
                            Code = "tickets:read"
                        },
                        new
                        {
                            Code = "tickets:check-in"
                        },
                        new
                        {
                            Code = "event-statistics:read"
                        },
                        new
                        {
                            Code = "products:read"
                        },
                        new
                        {
                            Code = "products:create"
                        },
                        new
                        {
                            Code = "products:update"
                        },
                        new
                        {
                            Code = "stocks:read"
                        },
                        new
                        {
                            Code = "stock-transactions:read"
                        },
                        new
                        {
                            Code = "stocks:adjust"
                        },
                        new
                        {
                            Code = "stocks:relocate"
                        },
                        new
                        {
                            Code = "receipts:read"
                        },
                        new
                        {
                            Code = "receipts:update"
                        },
                        new
                        {
                            Code = "receipts:create"
                        },
                        new
                        {
                            Code = "customers:read"
                        },
                        new
                        {
                            Code = "customers:update"
                        },
                        new
                        {
                            Code = "customers:create"
                        },
                        new
                        {
                            Code = "customers:contacts:create"
                        },
                        new
                        {
                            Code = "customers:contacts:update"
                        },
                        new
                        {
                            Code = "customers:outlets:create"
                        },
                        new
                        {
                            Code = "customers:outlets:update"
                        },
                        new
                        {
                            Code = "customers:products:update"
                        },
                        new
                        {
                            Code = "suppliers:read"
                        },
                        new
                        {
                            Code = "suppliers:update"
                        },
                        new
                        {
                            Code = "suppliers:create"
                        },
                        new
                        {
                            Code = "suppliers:contacts:create"
                        },
                        new
                        {
                            Code = "suppliers:contacts:update"
                        },
                        new
                        {
                            Code = "suppliers:products:update"
                        },
                        new
                        {
                            Code = "price-tiers:read"
                        },
                        new
                        {
                            Code = "price-tiers:update"
                        },
                        new
                        {
                            Code = "purchase-orders:create"
                        },
                        new
                        {
                            Code = "purchase-orders:read"
                        },
                        new
                        {
                            Code = "purchase-orders:update"
                        },
                        new
                        {
                            Code = "purchase-orders:approve"
                        },
                        new
                        {
                            Code = "purchase-orders:cancel"
                        },
                        new
                        {
                            Code = "drivers:read"
                        },
                        new
                        {
                            Code = "drivers:update"
                        },
                        new
                        {
                            Code = "drivers:create"
                        },
                        new
                        {
                            Code = "routes:read"
                        },
                        new
                        {
                            Code = "routes:update"
                        },
                        new
                        {
                            Code = "routes:create"
                        },
                        new
                        {
                            Code = "route-trip:read"
                        },
                        new
                        {
                            Code = "route-trip:update"
                        },
                        new
                        {
                            Code = "route-trip:create"
                        },
                        new
                        {
                            Code = "route-trip-log:update"
                        },
                        new
                        {
                            Code = "route-trip-log:update:other"
                        },
                        new
                        {
                            Code = "tasks:putaway:read"
                        },
                        new
                        {
                            Code = "tasks:putaway:update"
                        },
                        new
                        {
                            Code = "tasks:putaway:manage"
                        },
                        new
                        {
                            Code = "tasks:putaway:execute"
                        },
                        new
                        {
                            Code = "warehouse-operators:read"
                        },
                        new
                        {
                            Code = "warehouse-operators:update"
                        },
                        new
                        {
                            Code = "warehouse-operators:create"
                        },
                        new
                        {
                            Code = "carts:read"
                        },
                        new
                        {
                            Code = "carts:add"
                        },
                        new
                        {
                            Code = "carts:remove"
                        },
                        new
                        {
                            Code = "orders:read"
                        },
                        new
                        {
                            Code = "orders:read:own"
                        },
                        new
                        {
                            Code = "orders:create"
                        });
                });

            modelBuilder.Entity("Crowbond.Modules.Users.Domain.Users.Role", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.HasKey("Name")
                        .HasName("pk_roles");

                    b.ToTable("roles", "users");

                    b.HasData(
                        new
                        {
                            Name = "Administrator"
                        },
                        new
                        {
                            Name = "Customer"
                        },
                        new
                        {
                            Name = "Supplier"
                        },
                        new
                        {
                            Name = "Driver"
                        },
                        new
                        {
                            Name = "WarhouseOperator"
                        },
                        new
                        {
                            Name = "WarhouseManager"
                        });
                });

            modelBuilder.Entity("Crowbond.Modules.Users.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("first_name");

                    b.Property<string>("IdentityId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("identity_id");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("last_name");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email");

                    b.HasIndex("IdentityId")
                        .IsUnique()
                        .HasDatabaseName("ix_users_identity_id");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasDatabaseName("ix_users_username");

                    b.ToTable("users", "users");
                });

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.Property<string>("PermissionCode")
                        .HasColumnType("character varying(100)")
                        .HasColumnName("permission_code");

                    b.Property<string>("RoleName")
                        .HasColumnType("character varying(50)")
                        .HasColumnName("role_name");

                    b.HasKey("PermissionCode", "RoleName")
                        .HasName("pk_role_permissions");

                    b.HasIndex("RoleName")
                        .HasDatabaseName("ix_role_permissions_role_name");

                    b.ToTable("role_permissions", "users");

                    b.HasData(
                        new
                        {
                            PermissionCode = "route-trip:read",
                            RoleName = "Driver"
                        },
                        new
                        {
                            PermissionCode = "route-trip-log:update",
                            RoleName = "Driver"
                        },
                        new
                        {
                            PermissionCode = "tasks:putaway:read",
                            RoleName = "WarhouseOperator"
                        },
                        new
                        {
                            PermissionCode = "tasks:putaway:execute",
                            RoleName = "WarhouseOperator"
                        },
                        new
                        {
                            PermissionCode = "tasks:putaway:read",
                            RoleName = "WarhouseManager"
                        },
                        new
                        {
                            PermissionCode = "tasks:putaway:manage",
                            RoleName = "WarhouseManager"
                        },
                        new
                        {
                            PermissionCode = "carts:read",
                            RoleName = "Customer"
                        },
                        new
                        {
                            PermissionCode = "carts:add",
                            RoleName = "Customer"
                        },
                        new
                        {
                            PermissionCode = "carts:remove",
                            RoleName = "Customer"
                        },
                        new
                        {
                            PermissionCode = "orders:read:own",
                            RoleName = "Customer"
                        },
                        new
                        {
                            PermissionCode = "orders:create",
                            RoleName = "Customer"
                        },
                        new
                        {
                            PermissionCode = "users:read",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "users:update",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "events:read",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "events:search",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "events:update",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "ticket-types:read",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "ticket-types:update",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "categories:read",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "categories:update",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "tickets:read",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "tickets:check-in",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "event-statistics:read",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "products:read",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "products:create",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "products:update",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "stocks:read",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "stock-transactions:read",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "stocks:adjust",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "stocks:relocate",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "receipts:read",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "receipts:update",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "receipts:create",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "customers:read",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "customers:update",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "customers:create",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "customers:contacts:create",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "customers:contacts:update",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "customers:outlets:create",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "customers:outlets:update",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "customers:products:update",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "suppliers:read",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "suppliers:update",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "suppliers:create",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "suppliers:contacts:create",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "suppliers:contacts:update",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "suppliers:products:update",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "price-tiers:read",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "price-tiers:update",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "purchase-orders:create",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "purchase-orders:update",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "purchase-orders:read",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "purchase-orders:approve",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "purchase-orders:cancel",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "drivers:read",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "drivers:update",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "drivers:create",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "routes:read",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "routes:update",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "routes:create",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "route-trip:read",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "route-trip:update",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "route-trip:create",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "route-trip-log:update",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "route-trip-log:update:other",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "tasks:putaway:read",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "tasks:putaway:update",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "tasks:putaway:manage",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "tasks:putaway:execute",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "warehouse-operators:read",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "warehouse-operators:update",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "warehouse-operators:create",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "carts:read",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "carts:add",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "carts:remove",
                            RoleName = "Administrator"
                        },
                        new
                        {
                            PermissionCode = "orders:read",
                            RoleName = "Administrator"
                        });
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<string>("RolesName")
                        .HasColumnType("character varying(50)")
                        .HasColumnName("role_name");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("RolesName", "UserId")
                        .HasName("pk_user_roles");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_roles_user_id");

                    b.ToTable("user_roles", "users");
                });

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.HasOne("Crowbond.Modules.Users.Domain.Users.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_role_permissions_permissions_permission_code");

                    b.HasOne("Crowbond.Modules.Users.Domain.Users.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_role_permissions_roles_role_name");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("Crowbond.Modules.Users.Domain.Users.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_roles_roles_roles_name");

                    b.HasOne("Crowbond.Modules.Users.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_roles_users_user_id");
                });
#pragma warning restore 612, 618
        }
    }
}
