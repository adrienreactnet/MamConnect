using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MamConnect.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vaccines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgesInMonths = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Children",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    AssistantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Children", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Children_Users_AssistantId",
                        column: x => x.AssistantId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DailyReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChildId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyReports_Children_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Children",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParentChildren",
                columns: table => new
                {
                    ChildrenId = table.Column<int>(type: "int", nullable: false),
                    ParentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentChildren", x => new { x.ChildrenId, x.ParentsId });
                    table.ForeignKey(
                        name: "FK_ParentChildren_Children_ChildrenId",
                        column: x => x.ChildrenId,
                        principalTable: "Children",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParentChildren_Users_ParentsId",
                        column: x => x.ParentsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "Role" },
                values: new object[,]
                {
                    { 1, "admin@example.com", "Admin", "User", "AQAAAAEAACcQAAAAEBpXW/zlBa5UwW0tpsWsFmvZSK4bsEq+aPNbw+GQYlS2C/Zx6ujWmAoBrtWEDegbPA==", "0123456789", 2 },
                    { 2, "assistant1@example.com", "Alice", "Martin", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0100000001", 1 },
                    { 3, "assistant2@example.com", "Bruno", "Petit", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0100000002", 1 },
                    { 4, "assistant3@example.com", "Claire", "Leroy", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0100000003", 1 },
                    { 5, "assistant4@example.com", "David", "Morel", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "0100000004", 1 },
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
                table: "Vaccines",
                columns: new[] { "Id", "AgesInMonths", "Name" },
                values: new object[,]
                {
                    { 1, "2,4,11", "DTP et Coqueluche" },
                    { 2, "2,4,11", "Hib" },
                    { 3, "2,4,11", "Hépatite B" },
                    { 4, "2,4,11", "Pneumocoque" },
                    { 5, "12,16", "ROR" },
                    { 6, "6,12", "Méningocoques ACWY" },
                    { 7, "3,5,12", "Méningocoque B" }
                });

            migrationBuilder.InsertData(
                table: "Children",
                columns: new[] { "Id", "AssistantId", "BirthDate", "FirstName" },
                values: new object[,]
                {
                    { 1, 2, new DateOnly(2019, 5, 21), "Liam" },
                    { 2, 2, new DateOnly(2020, 3, 14), "Emma" },
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
                    { 15, 5, new DateOnly(2018, 4, 7), "Benjamin" },
                    { 16, 6, new DateOnly(2019, 12, 1), "Charlotte" },
                    { 17, 6, new DateOnly(2020, 7, 19), "Elijah" },
                    { 18, 6, new DateOnly(2021, 9, 23), "Evelyn" },
                    { 19, 6, new DateOnly(2018, 3, 13), "Henry" }
                });

            migrationBuilder.InsertData(
                table: "DailyReports",
                columns: new[] { "Id", "ChildId", "Content", "CreatedAt" },
                values: new object[,]
                {
                    { 1, 1, "First report Liam", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, "First report Emma", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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

            migrationBuilder.CreateIndex(
                name: "IX_Children_AssistantId",
                table: "Children",
                column: "AssistantId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyReports_ChildId",
                table: "DailyReports",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentChildren_ParentsId",
                table: "ParentChildren",
                column: "ParentsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyReports");

            migrationBuilder.DropTable(
                name: "ParentChildren");

            migrationBuilder.DropTable(
                name: "Vaccines");

            migrationBuilder.DropTable(
                name: "Children");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
