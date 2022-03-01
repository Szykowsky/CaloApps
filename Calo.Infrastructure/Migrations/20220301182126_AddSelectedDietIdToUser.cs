using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calo.Infrastructure.Migrations
{
    public partial class AddSelectedDietIdToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SelectedDietId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedDietId",
                table: "Users");
        }
    }
}
