using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tompet.Infrastructure.Migrations
{
    public partial class IsPublic_TechniqueD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Techniques",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Techniques");
        }
    }
}
