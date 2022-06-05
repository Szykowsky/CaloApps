using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calo.Infrastructure.Migrations
{
    public partial class AddDailyStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DailyStatusId",
                table: "Meals",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DailyStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    KcalConsumed = table.Column<int>(type: "int", nullable: false),
                    KcalRemaining = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyStatuses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meals_DailyStatusId",
                table: "Meals",
                column: "DailyStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyStatuses_UserId",
                table: "DailyStatuses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_DailyStatuses_DailyStatusId",
                table: "Meals",
                column: "DailyStatusId",
                principalTable: "DailyStatuses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_DailyStatuses_DailyStatusId",
                table: "Meals");

            migrationBuilder.DropTable(
                name: "DailyStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Meals_DailyStatusId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "DailyStatusId",
                table: "Meals");
        }
    }
}
