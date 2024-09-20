﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Crowbond.Modules.OMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Alter_Sequences_Table : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "pk_sequences",
            schema: "oms",
            table: "sequences");

        migrationBuilder.DropColumn(
            name: "id",
            schema: "oms",
            table: "sequences");

        migrationBuilder.AddColumn<string>(
            name: "prefix",
            schema: "oms",
            table: "sequences",
            type: "character varying(3)",
            maxLength: 3,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddPrimaryKey(
            name: "pk_sequences",
            schema: "oms",
            table: "sequences",
            column: "context");

        migrationBuilder.InsertData(
            schema: "oms",
            table: "sequences",
            columns: new[] { "context", "last_number", "prefix" },
            values: new object[,]
            {
                { 0, 10001, "INV" },
                { 1, 10001, "SOR" },
                { 2, 10001, "POR" }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "pk_sequences",
            schema: "oms",
            table: "sequences");

        migrationBuilder.DeleteData(
            schema: "oms",
            table: "sequences",
            keyColumn: "context",
            keyValue: 0);

        migrationBuilder.DeleteData(
            schema: "oms",
            table: "sequences",
            keyColumn: "context",
            keyValue: 1);

        migrationBuilder.DeleteData(
            schema: "oms",
            table: "sequences",
            keyColumn: "context",
            keyValue: 2);

        migrationBuilder.DropColumn(
            name: "prefix",
            schema: "oms",
            table: "sequences");

        migrationBuilder.AddColumn<Guid>(
            name: "id",
            schema: "oms",
            table: "sequences",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.AddPrimaryKey(
            name: "pk_sequences",
            schema: "oms",
            table: "sequences",
            column: "id");
    }
}