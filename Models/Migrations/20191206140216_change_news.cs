using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodNews.DB.Migrations
{
    public partial class change_news : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "MoodNews",
                table: "News",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoodNews",
                table: "News");
        }
    }
}
