using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Review.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ImagesUpload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarReviews_Cars_CarID",
                table: "CarReviews");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "35f73733-a0f7-4c05-86c6-99d7a628e15b");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "CarReviews");

            migrationBuilder.DropColumn(
                name: "ImageMimeType",
                table: "CarReviews");

            migrationBuilder.AlterColumn<int>(
                name: "CarID",
                table: "CarReviews",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageData = table.Column<byte[]>(type: "bytea", nullable: true),
                    ImageMimeType = table.Column<string>(type: "text", nullable: true),
                    CarReviewId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Image_CarReviews_CarReviewId",
                        column: x => x.CarReviewId,
                        principalTable: "CarReviews",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OIB", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "812af401-dbaa-4a06-9b14-ac2461c33394", 0, "d42f96be-be81-412e-8e25-18e0558ac8b5", "korisnik@test.com", true, false, null, "KORISNIK@TEST.COM", "KORISNIK@TEST.COM", "84858742558", "AQAAAAIAAYagAAAAEE2L/mRui9tcWZJtV+DB5mli8CD5/GgYMHQ70N7PMHnljU9bK39pGkiDeGimsnry2w==", null, false, "", false, "korisnik@test.com" });

            migrationBuilder.CreateIndex(
                name: "IX_Image_CarReviewId",
                table: "Image",
                column: "CarReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarReviews_Cars_CarID",
                table: "CarReviews",
                column: "CarID",
                principalTable: "Cars",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarReviews_Cars_CarID",
                table: "CarReviews");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "812af401-dbaa-4a06-9b14-ac2461c33394");

            migrationBuilder.AlterColumn<int>(
                name: "CarID",
                table: "CarReviews",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "CarReviews",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageMimeType",
                table: "CarReviews",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OIB", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "35f73733-a0f7-4c05-86c6-99d7a628e15b", 0, "c6a23903-97ef-4b7a-86e5-ec4faedec52a", "korisnik@test.com", true, false, null, "KORISNIK@TEST.COM", "KORISNIK@TEST.COM", "84858742558", "AQAAAAIAAYagAAAAEEobtaWm77iSiC9h8qnTs+BrrY1pcVIqNj1Y2xMDI1hWYcFyVDxrXShWyUFGW7cmTw==", null, false, "", false, "korisnik@test.com" });

            migrationBuilder.AddForeignKey(
                name: "FK_CarReviews_Cars_CarID",
                table: "CarReviews",
                column: "CarID",
                principalTable: "Cars",
                principalColumn: "ID");
        }
    }
}
