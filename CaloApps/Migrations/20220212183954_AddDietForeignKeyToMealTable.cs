using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaloApps.Migrations
{
    public partial class AddDietForeignKeyToMealTable : Migration
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

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Diet_DietId",
                table: "Meals",
                column: "DietId",
                principalTable: "Diet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Diet_DietId",
                table: "Meals");

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
