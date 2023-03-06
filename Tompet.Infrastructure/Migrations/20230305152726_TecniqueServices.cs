using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tompet.Infrastructure.Data.Migrations
{
    public partial class TecniqueServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Techniques_Services_ServiceId",
                table: "Techniques");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceId",
                table: "Techniques",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Techniques",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Techniques_Services_ServiceId",
                table: "Techniques",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Techniques_Services_ServiceId",
                table: "Techniques");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceId",
                table: "Techniques",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Techniques",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Techniques_Services_ServiceId",
                table: "Techniques",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }
    }
}
