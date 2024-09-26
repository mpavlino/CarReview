using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Review.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NewModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6a434cd0-08bd-4d7f-8f94-bfe073dd1fd9");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Models",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Cars",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OIB", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "901c2b8f-2d1b-40c7-b567-141f708f3185", 0, "ca91373c-dd8f-427b-9bcc-d1642df398b8", "korisnik@test.com", true, false, null, "KORISNIK@TEST.COM", "KORISNIK@TEST.COM", "84858742558", "AQAAAAIAAYagAAAAEO1ujIbIo8QTxlXeIxssQJSZBFaciYxTMbKjNHjfJxM7vzdX08Yhu+fIbJHHq37kLQ==", null, false, "", false, "korisnik@test.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "901c2b8f-2d1b-40c7-b567-141f708f3185");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Models");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Cars",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OIB", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6a434cd0-08bd-4d7f-8f94-bfe073dd1fd9", 0, "8502e527-9ed0-4de6-bee7-d6dae0e69e6b", "korisnik@test.com", true, false, null, "KORISNIK@TEST.COM", "KORISNIK@TEST.COM", "84858742558", "AQAAAAIAAYagAAAAEGbyLs5DC0rwOq1w9KN1duAiW+V+rzTxvdpFszdgQIw126qzQYq9hoatje9fUD04cQ==", null, false, "", false, "korisnik@test.com" });
        }
    }
}
