using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MamConnect.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "Role" },
                values: new object[] { "admin@example.com", "Admin", "User", "AQAAAAEAACcQAAAAEBpXW/zlBa5UwW0tpsWsFmvZSK4bsEq+aPNbw+GQYlS2C/Zx6ujWmAoBrtWEDegbPA==", "0123456789", 2 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Email",
                value: "assistant1@example.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "Role" },
                values: new object[] { "assistant2@example.com", 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "Email",
                value: "parent1@example.com");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "Role" },
                values: new object[] { 5, "parent2@example.com", "", "", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "Role" },
                values: new object[] { "assistant1@example.com", "", "", "a109e36947ad56de1dca1cc49f0ef8ac9ad9a7b1aa0df41fb3c4cb73c1ff01ea", "", 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Email",
                value: "assistant2@example.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "Role" },
                values: new object[] { "parent1@example.com", 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "Email",
                value: "parent2@example.com");
        }
    }
}
