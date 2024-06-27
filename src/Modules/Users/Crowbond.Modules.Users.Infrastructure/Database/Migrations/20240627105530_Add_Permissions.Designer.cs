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
    [Migration("20240627105530_Add_Permissions")]
    partial class Add_Permissions
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
                            Code = "orders:create"
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
                            Code = "stocktransactions:read"
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
                            Name = "Member"
                        },
                        new
                        {
                            Name = "Administrator"
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
                        },
                        new
                        {
                            PermissionCode = "orders:create",
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
                            PermissionCode = "stocktransactions:read",
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
