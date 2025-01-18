using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Regon = table.Column<string>(type: "TEXT", nullable: false),
                    Nip = table.Column<string>(type: "TEXT", nullable: false),
                    Address_City = table.Column<string>(type: "TEXT", nullable: true),
                    Address_Street = table.Column<string>(type: "TEXT", nullable: true),
                    Address_PostalCode = table.Column<string>(type: "TEXT", nullable: true),
                    Address_Region = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    Producent = table.Column<int>(type: "nvarchar(50)", nullable: false),
                    Produktdate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2024, 11, 21, 22, 28, 43, 719, DateTimeKind.Local).AddTicks(467)),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    OrganizationId = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 101)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "organizations",
                columns: new[] { "Id", "Nip", "Regon", "Title" },
                values: new object[] { 100, "83492100", "13777234", "Domyslna" });

            migrationBuilder.InsertData(
                table: "organizations",
                columns: new[] { "Id", "Nip", "Regon", "Title", "Address_City", "Address_PostalCode", "Address_Region", "Address_Street" },
                values: new object[] { 101, "83492384", "13424234", "Biedronka", "Kraków", "31-150", "małopolskie", "Św. Filipa 17" });

            migrationBuilder.InsertData(
                table: "organizations",
                columns: new[] { "Id", "Nip", "Regon", "Title", "Address_City", "Address_PostalCode", "Address_Region", "Address_Street" },
                values: new object[] { 102, "2498534", "0873439249", "Lidl", "Kraków", "31-150", "małopolskie", "Krowoderska 45/6" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "OrganizationId", "Price", "Producent", "Produktdate" },
                values: new object[] { 1, "Przykładowy opis dla produktu 1.", "Przykładowy Produkt 1", 101, 100, 0, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "OrganizationId", "Price", "Producent", "Produktdate" },
                values: new object[] { 2, "Przykładowy opis dla produktu 2.", "Przykładowy Produkt 2", 102, 200, 1, new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrganizationId",
                table: "Products",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "organizations");
        }
    }
}
