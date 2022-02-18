using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyRoutines.Infrastructure.Migrations
{
    public partial class editActionDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatePersianDay",
                table: "Actions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatePersianDay",
                table: "Actions");
        }
    }
}
