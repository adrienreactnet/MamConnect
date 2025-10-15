using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MamConnect.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddChildVaccines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChildVaccines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChildId = table.Column<int>(type: "int", nullable: false),
                    VaccineId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ScheduledDate = table.Column<DateOnly>(type: "date", nullable: true),
                    AdministrationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildVaccines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChildVaccines_Children_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Children",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChildVaccines_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ChildVaccines",
                columns: new[] { "Id", "AdministrationDate", "ChildId", "Comments", "CreatedAt", "ScheduledDate", "Status", "UpdatedAt", "VaccineId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2019, 7, 21), 1, "Dose initiale administrée conformément au calendrier.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2019, 7, 21), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateOnly(2019, 9, 21), 1, "Dose réalisée lors de la visite mensuelle.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2019, 9, 21), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, null, 1, "Rappel prévu lors de la prochaine visite médicale.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2020, 5, 21), 1, null, 5 },
                    { 4, new DateOnly(2020, 5, 14), 2, "Dose initiale administrée conformément au calendrier.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2020, 5, 14), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5, null, 2, "Dose programmée avec la famille.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2020, 7, 14), 1, null, 2 },
                    { 6, null, 2, "Rappel prévu lors de la prochaine visite médicale.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2021, 3, 14), 1, null, 5 },
                    { 7, new DateOnly(2021, 1, 2), 3, "Dose initiale administrée conformément au calendrier.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2021, 1, 2), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 8, new DateOnly(2021, 3, 2), 3, "Dose réalisée lors de la visite mensuelle.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2021, 3, 2), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 9, null, 3, "Relance nécessaire auprès des parents.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2021, 11, 2), 2, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 10, new DateOnly(2018, 9, 9), 4, "Dose initiale administrée conformément au calendrier.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2018, 9, 9), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 11, null, 4, "Dose programmée avec la famille.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2018, 11, 9), 1, null, 2 },
                    { 12, null, 4, "Rappel prévu lors de la prochaine visite médicale.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2019, 7, 9), 1, null, 5 },
                    { 13, new DateOnly(2019, 3, 30), 5, "Dose initiale administrée conformément au calendrier.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2019, 3, 30), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 14, new DateOnly(2019, 5, 30), 5, "Dose réalisée lors de la visite mensuelle.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2019, 5, 30), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 15, null, 5, "Rappel prévu lors de la prochaine visite médicale.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2020, 1, 30), 1, null, 5 },
                    { 16, new DateOnly(2021, 6, 18), 6, "Dose initiale administrée conformément au calendrier.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2021, 6, 18), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 17, null, 6, "Dose programmée avec la famille.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2021, 8, 18), 1, null, 2 },
                    { 18, null, 6, "Relance nécessaire auprès des parents.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 4, 18), 2, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 19, new DateOnly(2020, 11, 5), 7, "Dose initiale administrée conformément au calendrier.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2020, 11, 5), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 20, new DateOnly(2021, 1, 5), 7, "Dose réalisée lors de la visite mensuelle.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2021, 1, 5), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 21, null, 7, "Rappel prévu lors de la prochaine visite médicale.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2021, 9, 5), 1, null, 5 },
                    { 22, new DateOnly(2019, 2, 12), 8, "Dose initiale administrée conformément au calendrier.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2019, 2, 12), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 23, null, 8, "Dose programmée avec la famille.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2019, 4, 12), 1, null, 2 },
                    { 24, null, 8, "Rappel prévu lors de la prochaine visite médicale.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2019, 12, 12), 1, null, 5 },
                    { 25, new DateOnly(2019, 8, 22), 9, "Dose initiale administrée conformément au calendrier.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2019, 8, 22), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 26, new DateOnly(2019, 10, 22), 9, "Dose réalisée lors de la visite mensuelle.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2019, 10, 22), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 27, null, 9, "Relance nécessaire auprès des parents.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2020, 6, 22), 2, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 28, new DateOnly(2020, 4, 11), 10, "Dose initiale administrée conformément au calendrier.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2020, 4, 11), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 29, null, 10, "Dose programmée avec la famille.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2020, 6, 11), 1, null, 2 },
                    { 30, null, 10, "Rappel prévu lors de la prochaine visite médicale.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2021, 2, 11), 1, null, 5 },
                    { 31, new DateOnly(2018, 12, 3), 11, "Dose initiale administrée conformément au calendrier.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2018, 12, 3), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 32, new DateOnly(2019, 2, 3), 11, "Dose réalisée lors de la visite mensuelle.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2019, 2, 3), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 33, null, 11, "Rappel prévu lors de la prochaine visite médicale.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2019, 10, 3), 1, null, 5 },
                    { 34, new DateOnly(2021, 3, 27), 12, "Dose initiale administrée conformément au calendrier.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2021, 3, 27), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 35, null, 12, "Dose programmée avec la famille.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2021, 5, 27), 1, null, 2 },
                    { 36, null, 12, "Relance nécessaire auprès des parents.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 1, 27), 2, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 37, new DateOnly(2019, 10, 15), 13, "Dose initiale administrée conformément au calendrier.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2019, 10, 15), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 38, new DateOnly(2019, 12, 15), 13, "Dose réalisée lors de la visite mensuelle.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2019, 12, 15), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 39, null, 13, "Rappel prévu lors de la prochaine visite médicale.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2020, 8, 15), 1, null, 5 },
                    { 40, new DateOnly(2020, 7, 16), 14, "Dose initiale administrée conformément au calendrier.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2020, 7, 16), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 41, null, 14, "Dose programmée avec la famille.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2020, 9, 16), 1, null, 2 },
                    { 42, null, 14, "Rappel prévu lors de la prochaine visite médicale.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2021, 5, 16), 1, null, 5 },
                    { 43, new DateOnly(2018, 6, 7), 15, "Dose initiale administrée conformément au calendrier.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2018, 6, 7), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 44, new DateOnly(2018, 8, 7), 15, "Dose réalisée lors de la visite mensuelle.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2018, 8, 7), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 45, null, 15, "Relance nécessaire auprès des parents.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2019, 4, 7), 2, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 46, new DateOnly(2020, 2, 1), 16, "Dose initiale administrée conformément au calendrier.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2020, 2, 1), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 47, null, 16, "Dose programmée avec la famille.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2020, 4, 1), 1, null, 2 },
                    { 48, null, 16, "Rappel prévu lors de la prochaine visite médicale.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2020, 12, 1), 1, null, 5 },
                    { 49, new DateOnly(2020, 9, 19), 17, "Dose initiale administrée conformément au calendrier.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2020, 9, 19), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 50, new DateOnly(2020, 11, 19), 17, "Dose réalisée lors de la visite mensuelle.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2020, 11, 19), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 51, null, 17, "Rappel prévu lors de la prochaine visite médicale.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2021, 7, 19), 1, null, 5 },
                    { 52, new DateOnly(2021, 11, 23), 18, "Dose initiale administrée conformément au calendrier.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2021, 11, 23), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 53, null, 18, "Dose programmée avec la famille.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 1, 23), 1, null, 2 },
                    { 54, null, 18, "Relance nécessaire auprès des parents.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 9, 23), 2, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 55, new DateOnly(2018, 5, 13), 19, "Dose initiale administrée conformément au calendrier.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2018, 5, 13), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 56, new DateOnly(2018, 7, 13), 19, "Dose réalisée lors de la visite mensuelle.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2018, 7, 13), 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 57, null, 19, "Rappel prévu lors de la prochaine visite médicale.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2019, 3, 13), 1, null, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChildVaccines_ChildId_VaccineId_ScheduledDate",
                table: "ChildVaccines",
                columns: new[] { "ChildId", "VaccineId", "ScheduledDate" },
                unique: true,
                filter: "[ScheduledDate] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ChildVaccines_VaccineId",
                table: "ChildVaccines",
                column: "VaccineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChildVaccines");
        }
    }
}
