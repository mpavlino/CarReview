using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Review.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CarReviewsFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "74e26509-f9ce-4f22-827d-b127c5e46d3b");

            migrationBuilder.CreateTable(
                name: "CarReviews",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReviewerId = table.Column<int>(type: "integer", nullable: false),
                    CarID = table.Column<int>(type: "integer", nullable: true),
                    ImageData = table.Column<byte[]>(type: "bytea", nullable: true),
                    ImageMimeType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarReviews", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CarReviews_Cars_CarID",
                        column: x => x.CarID,
                        principalTable: "Cars",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CarReviews_Reviewers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Reviewers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OIB", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "35f73733-a0f7-4c05-86c6-99d7a628e15b", 0, "c6a23903-97ef-4b7a-86e5-ec4faedec52a", "korisnik@test.com", true, false, null, "KORISNIK@TEST.COM", "KORISNIK@TEST.COM", "84858742558", "AQAAAAIAAYagAAAAEEobtaWm77iSiC9h8qnTs+BrrY1pcVIqNj1Y2xMDI1hWYcFyVDxrXShWyUFGW7cmTw==", null, false, "", false, "korisnik@test.com" });

            migrationBuilder.CreateIndex(
                name: "IX_CarReviews_CarID",
                table: "CarReviews",
                column: "CarID");

            migrationBuilder.CreateIndex(
                name: "IX_CarReviews_ReviewerId",
                table: "CarReviews",
                column: "ReviewerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarReviews");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "35f73733-a0f7-4c05-86c6-99d7a628e15b");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OIB", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "74e26509-f9ce-4f22-827d-b127c5e46d3b", 0, "ad32cd93-b4fd-4464-a6ca-a824e0b37c43", "korisnik@test.com", true, false, null, "KORISNIK@TEST.COM", "KORISNIK@TEST.COM", "84858742558", "AQAAAAIAAYagAAAAEEG8eT40Qc9bBsgwbEbmHZExAiulwNZxp6bb1u4w6ktRJapn9hps7NQGsAt49Mv1Nw==", null, false, "", false, "korisnik@test.com" });
        }
    }
}
