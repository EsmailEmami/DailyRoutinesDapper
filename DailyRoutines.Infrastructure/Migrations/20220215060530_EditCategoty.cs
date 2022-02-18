using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyRoutines.Infrastructure.Migrations
{
    public partial class EditCategoty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "UserCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "UserCategories");
        }
    }
}
