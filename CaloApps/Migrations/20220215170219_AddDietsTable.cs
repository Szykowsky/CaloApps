using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaloApps.Migrations
{
    public partial class AddDietsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Users_UserId",
                table: "Meals");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Meals",
                newName: "DietId");

            migrationBuilder.RenameIndex(
                name: "IX_Meals_UserId",
                table: "Meals",
                newName: "IX_Meals_DietId");

            migrationBuilder.CreateTable(
                name: "Diets",
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
                    table.PrimaryKey("PK_Diets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diets_UserId",
                table: "Diets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Diets_DietId",
                table: "Meals",
                column: "DietId",
                principalTable: "Diets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Diets_DietId",
                table: "Meals");

            migrationBuilder.DropTable(
                name: "Diets");

            migrationBuilder.RenameColumn(
                name: "DietId",
                table: "Meals",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Meals_DietId",
                table: "Meals",
                newName: "IX_Meals_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Users_UserId",
                table: "Meals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
