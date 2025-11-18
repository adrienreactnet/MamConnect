using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MamConnect.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddChildHeadshot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HeadshotUrl",
                table: "Children",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 1,
                column: "HeadshotUrl",
                value: "https://api.dicebear.com/7.x/thumbs/svg?seed=Liam");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 2,
                column: "HeadshotUrl",
                value: "https://api.dicebear.com/7.x/thumbs/svg?seed=Emma");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 3,
                column: "HeadshotUrl",
                value: "https://api.dicebear.com/7.x/thumbs/svg?seed=Noah");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 4,
                column: "HeadshotUrl",
                value: "https://api.dicebear.com/7.x/thumbs/svg?seed=Olivia");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 5,
                column: "HeadshotUrl",
                value: "https://api.dicebear.com/7.x/thumbs/svg?seed=Ava");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 6,
                column: "HeadshotUrl",
                value: "https://api.dicebear.com/7.x/thumbs/svg?seed=Ethan");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 7,
                column: "HeadshotUrl",
                value: "https://api.dicebear.com/7.x/thumbs/svg?seed=Mia");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 8,
                column: "HeadshotUrl",
                value: "https://api.dicebear.com/7.x/thumbs/svg?seed=Lucas");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 9,
                column: "HeadshotUrl",
                value: "https://api.dicebear.com/7.x/thumbs/svg?seed=Sophia");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 10,
                column: "HeadshotUrl",
                value: "https://api.dicebear.com/7.x/thumbs/svg?seed=Mason");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 11,
                column: "HeadshotUrl",
                value: "https://api.dicebear.com/7.x/thumbs/svg?seed=Isabella");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 12,
                column: "HeadshotUrl",
                value: "https://api.dicebear.com/7.x/thumbs/svg?seed=Harper");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 13,
                column: "HeadshotUrl",
                value: "https://api.dicebear.com/7.x/thumbs/svg?seed=James");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 14,
                column: "HeadshotUrl",
                value: "https://api.dicebear.com/7.x/thumbs/svg?seed=Amelia");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 15,
                column: "HeadshotUrl",
                value: "https://api.dicebear.com/7.x/thumbs/svg?seed=Benjamin");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 16,
                column: "HeadshotUrl",
                value: "https://api.dicebear.com/7.x/thumbs/svg?seed=Charlotte");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 17,
                column: "HeadshotUrl",
                value: "https://api.dicebear.com/7.x/thumbs/svg?seed=Elijah");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 18,
                column: "HeadshotUrl",
                value: "https://api.dicebear.com/7.x/thumbs/svg?seed=Evelyn");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 19,
                column: "HeadshotUrl",
                value: "https://api.dicebear.com/7.x/thumbs/svg?seed=Henry");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeadshotUrl",
                table: "Children");
        }
    }
}
