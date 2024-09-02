using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Review.DAL.Migrations
{
    /// <inheritdoc />
    public partial class BrandModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Countries_CountryID",
                table: "Brands");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ad03bf3-6b71-4d42-aa84-637e08aaa46b");

            migrationBuilder.AlterColumn<int>(
                name: "CountryID",
                table: "Brands",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OIB", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e0c10371-a585-4d33-b383-f60aeb7cb83e", 0, "45e43bdc-308f-42b5-8173-9a96115d68fc", "korisnik@test.com", true, false, null, "KORISNIK@TEST.COM", "KORISNIK@TEST.COM", "84858742558", "AQAAAAIAAYagAAAAEKFuTn2mkUHysJjfkisaJJNj13vZTnEJIuxYbPpyeeaeo23NIqIYIvsldl2CUjQyfw==", null, false, "", false, "korisnik@test.com" });

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Countries_CountryID",
                table: "Brands",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Countries_CountryID",
                table: "Brands");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e0c10371-a585-4d33-b383-f60aeb7cb83e");

            migrationBuilder.AlterColumn<int>(
                name: "CountryID",
                table: "Brands",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OIB", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2ad03bf3-6b71-4d42-aa84-637e08aaa46b", 0, "a609adca-1d63-40c5-a7cf-cdae89b94aca", "korisnik@test.com", true, false, null, "KORISNIK@TEST.COM", "KORISNIK@TEST.COM", "84858742558", "AQAAAAIAAYagAAAAEIlcgE7vMcCyhWiOGqXsEogNrUIT4aK1wCnPDMzt9ybymkjnFH1/5f+SxPLEtEplTA==", null, false, "", false, "korisnik@test.com" });

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Countries_CountryID",
                table: "Brands",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
