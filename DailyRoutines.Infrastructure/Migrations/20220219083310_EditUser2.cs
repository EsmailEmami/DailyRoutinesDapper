using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyRoutines.Infrastructure.Migrations
{
    public partial class EditUser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBlock",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlock",
                table: "Users");
        }
    }
}
