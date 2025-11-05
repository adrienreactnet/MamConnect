using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MamConnect.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddChildAllergies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Allergies",
                table: "Children",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 1,
                column: "Allergies",
                value: "Arachides");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 2,
                column: "Allergies",
                value: null);

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 3,
                column: "Allergies",
                value: "Proteines de lait de vache");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 4,
                column: "Allergies",
                value: null);

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 5,
                column: "Allergies",
                value: null);

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 6,
                column: "Allergies",
                value: "Oeuf");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 7,
                column: "Allergies",
                value: null);

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 8,
                column: "Allergies",
                value: null);

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 9,
                column: "Allergies",
                value: "Pollen de bouleau");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 10,
                column: "Allergies",
                value: null);

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 11,
                column: "Allergies",
                value: null);

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 12,
                column: "Allergies",
                value: "Fruits a coque");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 13,
                column: "Allergies",
                value: null);

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 14,
                column: "Allergies",
                value: null);

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 15,
                column: "Allergies",
                value: null);

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 16,
                column: "Allergies",
                value: "Latex");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 17,
                column: "Allergies",
                value: null);

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 18,
                column: "Allergies",
                value: null);

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 19,
                column: "Allergies",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Allergies",
                table: "Children");
        }
    }
}
