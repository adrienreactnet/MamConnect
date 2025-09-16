using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MamConnect.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AssistantId", "BirthDate", "FirstName" },
                values: new object[] { 2, new DateOnly(2019, 5, 21), "Liam" });

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BirthDate", "FirstName" },
                values: new object[] { new DateOnly(2020, 3, 14), "Emma" });

            migrationBuilder.InsertData(
                table: "Children",
                columns: new[] { "Id", "AssistantId", "BirthDate", "FirstName" },
                values: new object[,]
                {
                    { 3, 2, new DateOnly(2020, 11, 2), "Noah" },
                    { 4, 3, new DateOnly(2018, 7, 9), "Olivia" },
                    { 5, 3, new DateOnly(2019, 1, 30), "Ava" },
                    { 6, 3, new DateOnly(2021, 4, 18), "Ethan" },
                    { 7, 3, new DateOnly(2020, 9, 5), "Mia" },
                    { 8, 4, new DateOnly(2018, 12, 12), "Lucas" },
                    { 9, 4, new DateOnly(2019, 6, 22), "Sophia" },
                    { 10, 4, new DateOnly(2020, 2, 11), "Mason" },
                    { 11, 4, new DateOnly(2018, 10, 3), "Isabella" },
                    { 12, 4, new DateOnly(2021, 1, 27), "Harper" },
                    { 13, 5, new DateOnly(2019, 8, 15), "James" },
                    { 14, 5, new DateOnly(2020, 5, 16), "Amelia" },
                    { 15, 5, new DateOnly(2018, 4, 7), "Benjamin" }
                });

            migrationBuilder.UpdateData(
                table: "DailyReports",
                keyColumn: "Id",
                keyValue: 1,
                column: "Content",
                value: "First report Liam");

            migrationBuilder.UpdateData(
                table: "DailyReports",
                keyColumn: "Id",
                keyValue: 2,
                column: "Content",
                value: "First report Emma");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FirstName", "LastName", "PhoneNumber" },
                values: new object[] { "Alice", "Martin", "0100000001" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FirstName", "LastName", "PhoneNumber" },
                values: new object[] { "Bruno", "Petit", "0100000002" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Email", "FirstName", "LastName", "PhoneNumber", "Role" },
                values: new object[] { "assistant3@example.com", "Claire", "Leroy", "0100000003", 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Email", "FirstName", "LastName", "PhoneNumber", "Role" },
                values: new object[] { "assistant4@example.com", "David", "Morel", "0100000004", 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "Role" },
                values: new object[,]
                {
                    { 6, "assistant5@example.com", "Emma", "Roussel", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0100000005", 1 },
                    { 7, "parent1@example.com", "Paul", "Durand", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0200000001", 0 },
                    { 8, "parent2@example.com", "Claire", "Durand", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0200000002", 0 },
                    { 9, "parent3@example.com", "Sophie", "Martin", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0200000003", 0 },
                    { 10, "parent4@example.com", "Julien", "Lambert", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0200000004", 0 },
                    { 11, "parent5@example.com", "Camille", "Lambert", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0200000005", 0 },
                    { 12, "parent6@example.com", "Nadia", "Bernard", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0200000006", 0 },
                    { 13, "parent7@example.com", "Hugo", "Lefevre", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0200000007", 0 },
                    { 14, "parent8@example.com", "Elise", "Lefevre", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0200000008", 0 },
                    { 15, "parent9@example.com", "Thomas", "Petit", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0200000009", 0 },
                    { 16, "parent10@example.com", "Laura", "Petit", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0200000010", 0 },
                    { 17, "parent11@example.com", "Antoine", "Moreau", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0200000011", 0 },
                    { 18, "parent12@example.com", "Marie", "Moreau", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0200000012", 0 },
                    { 19, "parent13@example.com", "Isabelle", "Garnier", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0200000013", 0 },
                    { 20, "parent14@example.com", "Victor", "Robert", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0200000014", 0 },
                    { 21, "parent15@example.com", "Amelie", "Robert", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0200000015", 0 },
                    { 22, "parent16@example.com", "Patrick", "Caron", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0200000016", 0 },
                    { 23, "parent17@example.com", "Helene", "Marchand", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0200000017", 0 },
                    { 24, "parent18@example.com", "Xavier", "Marchand", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0200000018", 0 },
                    { 25, "parent19@example.com", "Chantal", "Dupont", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0200000019", 0 },
                    { 26, "parent20@example.com", "Olivier", "Renard", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0200000020", 0 },
                    { 27, "parent21@example.com", "Lucie", "Renard", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0200000021", 0 }
                });

            migrationBuilder.InsertData(
                table: "Children",
                columns: new[] { "Id", "AssistantId", "BirthDate", "FirstName" },
                values: new object[,]
                {
                    { 16, 6, new DateOnly(2019, 12, 1), "Charlotte" },
                    { 17, 6, new DateOnly(2020, 7, 19), "Elijah" },
                    { 18, 6, new DateOnly(2021, 9, 23), "Evelyn" },
                    { 19, 6, new DateOnly(2018, 3, 13), "Henry" }
                });

            migrationBuilder.InsertData(
                table: "ParentChildren",
                columns: new[] { "ChildrenId", "ParentsId" },
                values: new object[,]
                {
                    { 1, 7 },
                    { 1, 8 },
                    { 2, 7 },
                    { 2, 8 },
                    { 3, 9 },
                    { 4, 10 },
                    { 4, 11 },
                    { 5, 10 },
                    { 5, 11 },
                    { 6, 12 },
                    { 7, 13 },
                    { 7, 14 },
                    { 8, 15 },
                    { 8, 16 },
                    { 9, 15 },
                    { 9, 16 },
                    { 10, 17 },
                    { 10, 18 },
                    { 11, 17 },
                    { 11, 18 },
                    { 12, 19 },
                    { 13, 20 },
                    { 13, 21 },
                    { 14, 20 },
                    { 14, 21 },
                    { 15, 22 },
                    { 16, 23 },
                    { 16, 24 },
                    { 17, 23 },
                    { 17, 24 },
                    { 18, 25 },
                    { 19, 26 },
                    { 19, 27 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 2, 8 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 3, 9 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 4, 10 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 4, 11 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 5, 10 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 5, 11 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 6, 12 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 7, 13 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 7, 14 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 8, 15 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 8, 16 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 9, 15 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 9, 16 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 10, 17 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 10, 18 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 11, 17 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 11, 18 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 12, 19 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 13, 20 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 13, 21 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 14, 20 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 14, 21 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 15, 22 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 16, 23 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 16, 24 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 17, 23 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 17, 24 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 18, 25 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 19, 26 });

            migrationBuilder.DeleteData(
                table: "ParentChildren",
                keyColumns: new[] { "ChildrenId", "ParentsId" },
                keyValues: new object[] { 19, 27 });

            migrationBuilder.DeleteData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AssistantId", "BirthDate", "FirstName" },
                values: new object[] { 1, new DateOnly(2020, 1, 1), "Emeline" });

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BirthDate", "FirstName" },
                values: new object[] { new DateOnly(2020, 1, 1), "Alice" });

            migrationBuilder.UpdateData(
                table: "DailyReports",
                keyColumn: "Id",
                keyValue: 1,
                column: "Content",
                value: "First report Emeline");

            migrationBuilder.UpdateData(
                table: "DailyReports",
                keyColumn: "Id",
                keyValue: 2,
                column: "Content",
                value: "First report Alice");

            migrationBuilder.InsertData(
                table: "ParentChildren",
                columns: new[] { "ChildrenId", "ParentsId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 2, 4 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FirstName", "LastName", "PhoneNumber" },
                values: new object[] { "", "", "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FirstName", "LastName", "PhoneNumber" },
                values: new object[] { "", "", "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Email", "FirstName", "LastName", "PhoneNumber", "Role" },
                values: new object[] { "parent1@example.com", "", "", "", 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Email", "FirstName", "LastName", "PhoneNumber", "Role" },
                values: new object[] { "parent2@example.com", "", "", "", 0 });
        }
    }
}
