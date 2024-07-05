using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Review.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CarImageUpload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c72bc749-8ed2-437d-bac5-2099a7b1428f");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Cars",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageMimeType",
                table: "Cars",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OIB",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OIB", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "67cbfc52-2b98-47be-960c-8c8061c278e3", 0, "52f20fbd-4e36-4d53-a969-dc8e74de3035", "korisnik@test.com", true, false, null, "KORISNIK@TEST.COM", "KORISNIK@TEST.COM", "84858742558", "AQAAAAIAAYagAAAAEGNjqIMEl0jE45FtbUlv9yVousJqInEcSpmJzov5I6lwPpxl/lYPzxdMm8OkdO3rhQ==", null, false, "", false, "korisnik@test.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "67cbfc52-2b98-47be-960c-8c8061c278e3");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ImageMimeType",
                table: "Cars");

            migrationBuilder.AlterColumn<string>(
                name: "OIB",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OIB", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c72bc749-8ed2-437d-bac5-2099a7b1428f", 0, "b90651eb-b5ad-416c-bf43-f896db0fc599", "korisnik@test.com", true, false, null, "KORISNIK@TEST.COM", "KORISNIK@TEST.COM", "84858742558", "AQAAAAIAAYagAAAAEFairTdGPojK550C/Gdwrh3lgYT6qD7bXXLXDgFcb3KavXasbUE2+2cj84XW/UlUmg==", null, false, "", false, "korisnik@test.com" });
        }
    }
}
