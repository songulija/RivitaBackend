using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RivitaBackend.Migrations
{
    public partial class createdDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_UserTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "UserTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transportations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportationNumber = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    WagonsCount = table.Column<int>(type: "int", nullable: false),
                    TransportationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CargoAcceptanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MovementStartDateInBelarus = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MovementEndDateInBelarus = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EtsngCargoCode = table.Column<int>(type: "int", nullable: false),
                    GngCargoCode = table.Column<int>(type: "int", nullable: false),
                    DepartureStationTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartureCountryTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationStationTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationCountryTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StationMovementBeginingBelarusTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StationMovementEndBelarusTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transportations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wagons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransportationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberOfWagon = table.Column<int>(type: "int", nullable: false),
                    TypeOfWagon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wagons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wagons_Transportations_TransportationId",
                        column: x => x.TransportationId,
                        principalTable: "Transportations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Rivita" },
                    { 2, "Linas Agro" }
                });

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "ADMINISTRATOR" },
                    { 2, "USER" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CompanyId", "Password", "PhoneNumber", "TypeId", "Username" },
                values: new object[] { new Guid("c9490c27-1b89-4e39-8f2e-99b48dcc709e"), 1, "$2a$11$N5UxGnynN2dACKX6/V8PvOXmoN.MAQUtJVyllt.yh4.8fWCyEfR0a", "+37061816214", 1, "jevgenijrivita" });

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_TransportationNumber",
                table: "Transportations",
                column: "TransportationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_UserId",
                table: "Transportations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId",
                table: "Users",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TypeId",
                table: "Users",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Wagons_TransportationId",
                table: "Wagons",
                column: "TransportationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wagons");

            migrationBuilder.DropTable(
                name: "Transportations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "UserTypes");
        }
    }
}
