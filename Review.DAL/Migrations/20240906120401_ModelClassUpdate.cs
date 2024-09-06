using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Review.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ModelClassUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3197430a-5297-49ba-89af-1d31656d90e8");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "ModelID",
                table: "Cars",
                type: "integer",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OIB", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "076ffb2b-05c7-4f70-b939-6c22f60d98da", 0, "69b072bb-d3b0-48b9-9136-12faf1d3db00", "korisnik@test.com", true, false, null, "KORISNIK@TEST.COM", "KORISNIK@TEST.COM", "84858742558", "AQAAAAIAAYagAAAAEB6id4w7neKqPak7xXPl5ER7j6HwvCor+FbG0xVo60Vx9Tf7mTRZcvHv24Gd+wFNNw==", null, false, "", false, "korisnik@test.com" });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ModelID",
                table: "Cars",
                column: "ModelID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Models_ModelID",
                table: "Cars",
                column: "ModelID",
                principalTable: "Models",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Models_ModelID",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ModelID",
                table: "Cars");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "076ffb2b-05c7-4f70-b939-6c22f60d98da");

            migrationBuilder.DropColumn(
                name: "ModelID",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Cars",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OIB", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3197430a-5297-49ba-89af-1d31656d90e8", 0, "3dc1c4ff-a2c1-4714-96d1-d121bb0e8573", "korisnik@test.com", true, false, null, "KORISNIK@TEST.COM", "KORISNIK@TEST.COM", "84858742558", "AQAAAAIAAYagAAAAEKu0KutdHqKhG5IN2haFdeXPP8LKOm7AB93P0dKMBLGKY6syMGL5YnxwV+3e6C3vfQ==", null, false, "", false, "korisnik@test.com" });
        }
    }
}
