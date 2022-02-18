using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyRoutines.Infrastructure.Migrations
{
    public partial class EditAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatePersianMonth",
                table: "Actions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatePersianYear",
                table: "Actions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatePersianMonth",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "CreatePersianYear",
                table: "Actions");
        }
    }
}
