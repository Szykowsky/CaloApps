using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calo.Infrastructure.Migrations
{
    public partial class AddUserAdditionalDataTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "SelectedDietId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "AdditionalDataId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SettingId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserAdditionals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Growth = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAdditionals", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AdditionalDataId",
                table: "Users",
                column: "AdditionalDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserAdditionals_AdditionalDataId",
                table: "Users",
                column: "AdditionalDataId",
                principalTable: "UserAdditionals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserAdditionals_AdditionalDataId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserAdditionals");

            migrationBuilder.DropIndex(
                name: "IX_Users_AdditionalDataId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AdditionalDataId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SettingId",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "SelectedDietId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
