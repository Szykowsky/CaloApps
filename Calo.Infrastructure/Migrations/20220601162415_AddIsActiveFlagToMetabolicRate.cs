using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calo.Infrastructure.Migrations
{
    public partial class AddIsActiveFlagToMetabolicRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "MetabolicRate",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "MetabolicRate");
        }
    }
}
