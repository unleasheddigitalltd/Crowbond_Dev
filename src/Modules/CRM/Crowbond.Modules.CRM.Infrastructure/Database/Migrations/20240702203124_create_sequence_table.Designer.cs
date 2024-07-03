﻿// <auto-generated />
using System;
using Crowbond.Modules.CRM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Crowbond.Modules.CRM.Infrastructure.Database.Migrations
{
    [DbContext(typeof(CrmDbContext))]
    [Migration("20240702203124_create_sequence_table")]
    partial class create_sequence_table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("crm")
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Crowbond.Modules.CRM.Domain.Customers.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("account_number");

                    b.Property<string>("BillingAddressLine1")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("billing_address_line1");

                    b.Property<string>("BillingAddressLine2")
                        .HasColumnType("text")
                        .HasColumnName("billing_address_line2");

                    b.Property<string>("BillingCountry")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("billing_country");

                    b.Property<string>("BillingCounty")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("billing_county");

                    b.Property<string>("BillingPostalCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("billing_postal_code");

                    b.Property<string>("BillingTownCity")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("billing_town_city");

                    b.Property<string>("BusinessName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("business_name");

                    b.Property<string>("CustomerContact")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("customer_contact");

                    b.Property<string>("CustomerEmail")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("customer_email");

                    b.Property<string>("CustomerNotes")
                        .HasColumnType("text")
                        .HasColumnName("customer_notes");

                    b.Property<string>("CustomerPhone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("customer_phone");

                    b.Property<bool>("DetailedInvoice")
                        .HasColumnType("boolean")
                        .HasColumnName("detailed_invoice");

                    b.Property<string>("DriverCode")
                        .HasColumnType("text")
                        .HasColumnName("driver_code");

                    b.Property<Guid>("InvoicePeriodId")
                        .HasColumnType("uuid")
                        .HasColumnName("invoice_period_id");

                    b.Property<int>("PaymentTerms")
                        .HasColumnType("integer")
                        .HasColumnName("payment_terms");

                    b.Property<int>("PriceGroupId")
                        .HasColumnType("integer")
                        .HasColumnName("price_group_id");

                    b.Property<string>("ShippingAddressLine1")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("shipping_address_line1");

                    b.Property<string>("ShippingAddressLine2")
                        .HasColumnType("text")
                        .HasColumnName("shipping_address_line2");

                    b.Property<string>("ShippingCountry")
                        .HasColumnType("text")
                        .HasColumnName("shipping_country");

                    b.Property<string>("ShippingCounty")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("shipping_county");

                    b.Property<string>("ShippingPostalCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("shipping_postal_code");

                    b.Property<string>("ShippingTownCity")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("shipping_town_city");

                    b.HasKey("Id")
                        .HasName("pk_customers");

                    b.ToTable("customers", "crm");
                });

            modelBuilder.Entity("Crowbond.Modules.CRM.Domain.Sequences.Sequence", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Context")
                        .HasColumnType("integer")
                        .HasColumnName("context");

                    b.Property<int>("LastNumber")
                        .HasColumnType("integer")
                        .HasColumnName("last_number");

                    b.HasKey("Id")
                        .HasName("pk_sequences");

                    b.ToTable("sequences", "crm");
                });

            modelBuilder.Entity("Crowbond.Modules.CRM.Domain.Suppliers.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("account_number");

                    b.Property<string>("AddressCountry")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address_country");

                    b.Property<string>("AddressCounty")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address_county");

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address_line1");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("text")
                        .HasColumnName("address_line2");

                    b.Property<string>("AddressPostalCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address_postal_code");

                    b.Property<string>("AddressTownCity")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address_town_city");

                    b.Property<string>("BillingAddressCountry")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("billing_address_country");

                    b.Property<string>("BillingAddressCounty")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("billing_address_county");

                    b.Property<string>("BillingAddressLine1")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("billing_address_line1");

                    b.Property<string>("BillingAddressLine2")
                        .HasColumnType("text")
                        .HasColumnName("billing_address_line2");

                    b.Property<string>("BillingAddressPostalCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("billing_address_postal_code");

                    b.Property<string>("BillingAddressTownCity")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("billing_address_town_city");

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("contact_name");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email_address");

                    b.Property<int>("PaymentTerms")
                        .HasColumnType("integer")
                        .HasColumnName("payment_terms");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("supplier_name");

                    b.Property<string>("SupplierNotes")
                        .HasColumnType("text")
                        .HasColumnName("supplier_notes");

                    b.HasKey("Id")
                        .HasName("pk_suppliers");

                    b.ToTable("suppliers", "crm");
                });
#pragma warning restore 612, 618
        }
    }
}
