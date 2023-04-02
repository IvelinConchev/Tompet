using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tompet.Infrastructure.Migrations
{
    public partial class ServicesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Services");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Services",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
