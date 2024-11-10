using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechnicoWebApi.Migrations
{
    /// <inheritdoc />
    public partial class M1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VAT = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OwnerType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConstructionYear = table.Column<int>(type: "int", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduledRepair = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RepairType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RepairStatus = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repairs_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OwnerPropertyItem",
                columns: table => new
                {
                    OwnersId = table.Column<int>(type: "int", nullable: false),
                    PropertiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerPropertyItem", x => new { x.OwnersId, x.PropertiesId });
                    table.ForeignKey(
                        name: "FK_OwnerPropertyItem_Owners_OwnersId",
                        column: x => x.OwnersId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OwnerPropertyItem_Properties_PropertiesId",
                        column: x => x.PropertiesId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "Address", "Email", "Name", "OwnerType", "Password", "PhoneNumber", "Surname", "VAT" },
                values: new object[,]
                {
                    { 1, "123 Elm St", "alice.smith@example.com", "Alice", 1, "A1!abcde", "123-456-7890", "Smith", "12345678910" },
                    { 2, "123 Main St, Anytown, USA", "john.doe@example.com", "Johnathan", 0, "Secure@123", "123-456-7892", "Doe", "12345678901" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OwnerPropertyItem_PropertiesId",
                table: "OwnerPropertyItem",
                column: "PropertiesId");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_Email",
                table: "Owners",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Owners_PhoneNumber",
                table: "Owners",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Owners_VAT",
                table: "Owners",
                column: "VAT",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_IdentificationNumber",
                table: "Properties",
                column: "IdentificationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_OwnerId",
                table: "Repairs",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwnerPropertyItem");

            migrationBuilder.DropTable(
                name: "Repairs");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
