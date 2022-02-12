using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaloApps.Migrations
{
    public partial class AddDietTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DayKcal = table.Column<int>(type: "int", nullable: false),
                    Carbohydrates = table.Column<int>(type: "int", nullable: true),
                    Fiber = table.Column<int>(type: "int", nullable: true),
                    Protein = table.Column<int>(type: "int", nullable: true),
                    Fats = table.Column<int>(type: "int", nullable: true),
                    Vitamins = table.Column<int>(type: "int", nullable: true),
                    Minerals = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diet_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diet_UserId",
                table: "Diet",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diet");
        }
    }
}
