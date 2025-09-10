using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MamConnect.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NomMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Role" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 0 },
                    { 4, 0 }
                });

            migrationBuilder.InsertData(
                table: "Children",
                columns: new[] { "Id", "AssistantId", "BirthDate", "FirstName" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(2020, 1, 1), "Emeline" },
                    { 2, 2, new DateOnly(2020, 1, 1), "Alice" }
                });

            migrationBuilder.InsertData(
                table: "DailyReports",
                columns: new[] { "Id", "ChildId", "Content", "CreatedAt" },
                values: new object[,]
                {
                    { 1, 1, "First report Emeline", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, "First report Alice", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ParentChildren",
                columns: new[] { "ChildrenId", "ParentsId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 2, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DailyReports",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DailyReports",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
