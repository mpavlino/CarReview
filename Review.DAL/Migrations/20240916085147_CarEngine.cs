using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Review.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CarEngine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "076ffb2b-05c7-4f70-b939-6c22f60d98da");

            migrationBuilder.DropColumn(
                name: "Acceleration",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Engine",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "EngineDisplacement",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "EnginePower",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "TopSpeed",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Torque",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "ModelYear",
                table: "Cars",
                newName: "ModelYearFrom");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModelYearTo",
                table: "Cars",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Engines",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Cylinders = table.Column<string>(type: "text", nullable: true),
                    Displacement = table.Column<string>(type: "text", nullable: true),
                    Power = table.Column<string>(type: "text", nullable: true),
                    Torque = table.Column<string>(type: "text", nullable: true),
                    FuelSystem = table.Column<string>(type: "text", nullable: true),
                    FuelType = table.Column<string>(type: "text", nullable: true),
                    FuelCapacity = table.Column<decimal>(type: "numeric", nullable: true),
                    TopSpeed = table.Column<int>(type: "integer", nullable: true),
                    Acceleration = table.Column<decimal>(type: "numeric", nullable: true),
                    DriveType = table.Column<string>(type: "text", nullable: true),
                    Gearbox = table.Column<string>(type: "text", nullable: true),
                    FrontBrakes = table.Column<string>(type: "text", nullable: true),
                    RearBrakes = table.Column<string>(type: "text", nullable: true),
                    TireSize = table.Column<string>(type: "text", nullable: true),
                    Length = table.Column<string>(type: "text", nullable: true),
                    Width = table.Column<string>(type: "text", nullable: true),
                    Height = table.Column<string>(type: "text", nullable: true),
                    FrontRearTrack = table.Column<string>(type: "text", nullable: true),
                    Wheelbase = table.Column<string>(type: "text", nullable: true),
                    GroundClearance = table.Column<string>(type: "text", nullable: true),
                    CargoVolume = table.Column<string>(type: "text", nullable: true),
                    UnladenWeight = table.Column<string>(type: "text", nullable: true),
                    GrossWeightLimit = table.Column<string>(type: "text", nullable: true),
                    FuelEconomyCity = table.Column<string>(type: "text", nullable: true),
                    FuelEconomyHighway = table.Column<string>(type: "text", nullable: true),
                    FuelEconomyCombined = table.Column<string>(type: "text", nullable: true),
                    CO2Emissions = table.Column<string>(type: "text", nullable: true),
                    CarID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engines", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Engines_Cars_CarID",
                        column: x => x.CarID,
                        principalTable: "Cars",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OIB", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6a434cd0-08bd-4d7f-8f94-bfe073dd1fd9", 0, "8502e527-9ed0-4de6-bee7-d6dae0e69e6b", "korisnik@test.com", true, false, null, "KORISNIK@TEST.COM", "KORISNIK@TEST.COM", "84858742558", "AQAAAAIAAYagAAAAEGbyLs5DC0rwOq1w9KN1duAiW+V+rzTxvdpFszdgQIw126qzQYq9hoatje9fUD04cQ==", null, false, "", false, "korisnik@test.com" });

            migrationBuilder.CreateIndex(
                name: "IX_Engines_CarID",
                table: "Engines",
                column: "CarID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Engines");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6a434cd0-08bd-4d7f-8f94-bfe073dd1fd9");

            migrationBuilder.DropColumn(
                name: "ModelYearTo",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "ModelYearFrom",
                table: "Cars",
                newName: "ModelYear");

            migrationBuilder.AddColumn<decimal>(
                name: "Acceleration",
                table: "Cars",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Engine",
                table: "Cars",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EngineDisplacement",
                table: "Cars",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnginePower",
                table: "Cars",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TopSpeed",
                table: "Cars",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Torque",
                table: "Cars",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OIB", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "076ffb2b-05c7-4f70-b939-6c22f60d98da", 0, "69b072bb-d3b0-48b9-9136-12faf1d3db00", "korisnik@test.com", true, false, null, "KORISNIK@TEST.COM", "KORISNIK@TEST.COM", "84858742558", "AQAAAAIAAYagAAAAEB6id4w7neKqPak7xXPl5ER7j6HwvCor+FbG0xVo60Vx9Tf7mTRZcvHv24Gd+wFNNw==", null, false, "", false, "korisnik@test.com" });
        }
    }
}
