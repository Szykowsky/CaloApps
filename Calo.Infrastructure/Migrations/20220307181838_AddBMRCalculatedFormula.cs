using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calo.Infrastructure.Migrations
{
    public partial class AddBMRCalculatedFormula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Formula",
                table: "MetabolicRate",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Formula",
                table: "MetabolicRate");
        }
    }
}
