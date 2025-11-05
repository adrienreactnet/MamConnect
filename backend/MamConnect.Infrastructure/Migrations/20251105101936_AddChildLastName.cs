using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MamConnect.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddChildLastName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Children",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 3,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 5,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 6,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 9,
                column: "Status",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 11,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 12,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 15,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 17,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 18,
                column: "Status",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 21,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 23,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 24,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 27,
                column: "Status",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 29,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 30,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 33,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 35,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 36,
                column: "Status",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 39,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 41,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 42,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 45,
                column: "Status",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 47,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 48,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 51,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 53,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 54,
                column: "Status",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 57,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastName",
                value: "Durand");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastName",
                value: "Durand");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastName",
                value: "Martin");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 4,
                column: "LastName",
                value: "Lambert");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 5,
                column: "LastName",
                value: "Lambert");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 6,
                column: "LastName",
                value: "Bernard");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 7,
                column: "LastName",
                value: "Lefevre");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 8,
                column: "LastName",
                value: "Petit");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 9,
                column: "LastName",
                value: "Petit");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 10,
                column: "LastName",
                value: "Moreau");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 11,
                column: "LastName",
                value: "Moreau");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 12,
                column: "LastName",
                value: "Garnier");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 13,
                column: "LastName",
                value: "Robert");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 14,
                column: "LastName",
                value: "Robert");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 15,
                column: "LastName",
                value: "Caron");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 16,
                column: "LastName",
                value: "Marchand");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 17,
                column: "LastName",
                value: "Marchand");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 18,
                column: "LastName",
                value: "Dupont");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 19,
                column: "LastName",
                value: "Renard");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Children");

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 3,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 5,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 6,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 9,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 11,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 12,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 15,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 17,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 18,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 21,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 23,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 24,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 27,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 29,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 30,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 33,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 35,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 36,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 39,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 41,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 42,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 45,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 47,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 48,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 51,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 53,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 54,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ChildVaccines",
                keyColumn: "Id",
                keyValue: 57,
                column: "Status",
                value: 1);
        }
    }
}
