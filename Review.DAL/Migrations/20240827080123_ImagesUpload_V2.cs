using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Review.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ImagesUpload_V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_CarReviews_CarReviewId",
                table: "Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                table: "Image");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "812af401-dbaa-4a06-9b14-ac2461c33394");

            migrationBuilder.RenameTable(
                name: "Image",
                newName: "Images");

            migrationBuilder.RenameIndex(
                name: "IX_Image_CarReviewId",
                table: "Images",
                newName: "IX_Images_CarReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "ID");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OIB", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2ad03bf3-6b71-4d42-aa84-637e08aaa46b", 0, "a609adca-1d63-40c5-a7cf-cdae89b94aca", "korisnik@test.com", true, false, null, "KORISNIK@TEST.COM", "KORISNIK@TEST.COM", "84858742558", "AQAAAAIAAYagAAAAEIlcgE7vMcCyhWiOGqXsEogNrUIT4aK1wCnPDMzt9ybymkjnFH1/5f+SxPLEtEplTA==", null, false, "", false, "korisnik@test.com" });

            migrationBuilder.AddForeignKey(
                name: "FK_Images_CarReviews_CarReviewId",
                table: "Images",
                column: "CarReviewId",
                principalTable: "CarReviews",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_CarReviews_CarReviewId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ad03bf3-6b71-4d42-aa84-637e08aaa46b");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "Image");

            migrationBuilder.RenameIndex(
                name: "IX_Images_CarReviewId",
                table: "Image",
                newName: "IX_Image_CarReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                table: "Image",
                column: "ID");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OIB", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "812af401-dbaa-4a06-9b14-ac2461c33394", 0, "d42f96be-be81-412e-8e25-18e0558ac8b5", "korisnik@test.com", true, false, null, "KORISNIK@TEST.COM", "KORISNIK@TEST.COM", "84858742558", "AQAAAAIAAYagAAAAEE2L/mRui9tcWZJtV+DB5mli8CD5/GgYMHQ70N7PMHnljU9bK39pGkiDeGimsnry2w==", null, false, "", false, "korisnik@test.com" });

            migrationBuilder.AddForeignKey(
                name: "FK_Image_CarReviews_CarReviewId",
                table: "Image",
                column: "CarReviewId",
                principalTable: "CarReviews",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
