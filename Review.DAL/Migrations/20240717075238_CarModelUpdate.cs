using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Review.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CarModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "67cbfc52-2b98-47be-960c-8c8061c278e3");

            migrationBuilder.AddColumn<string>(
                name: "Generation",
                table: "Cars",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OIB", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "50563a2a-98d8-4f19-9a7b-416de985d2e8", 0, "a42c10d0-1beb-4915-95ad-2caa1b18d42c", "korisnik@test.com", true, false, null, "KORISNIK@TEST.COM", "KORISNIK@TEST.COM", "84858742558", "AQAAAAIAAYagAAAAEFNmhS5LzTlXqwvm1QGG2iinZeTTWXwbNanQ84frZ1XzfzeWqNr8H91T8FfejnnKeQ==", null, false, "", false, "korisnik@test.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "50563a2a-98d8-4f19-9a7b-416de985d2e8");

            migrationBuilder.DropColumn(
                name: "Generation",
                table: "Cars");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OIB", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "67cbfc52-2b98-47be-960c-8c8061c278e3", 0, "52f20fbd-4e36-4d53-a969-dc8e74de3035", "korisnik@test.com", true, false, null, "KORISNIK@TEST.COM", "KORISNIK@TEST.COM", "84858742558", "AQAAAAIAAYagAAAAEGNjqIMEl0jE45FtbUlv9yVousJqInEcSpmJzov5I6lwPpxl/lYPzxdMm8OkdO3rhQ==", null, false, "", false, "korisnik@test.com" });
        }
    }
}
