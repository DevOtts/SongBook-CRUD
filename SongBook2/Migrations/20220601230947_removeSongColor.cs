using Microsoft.EntityFrameworkCore.Migrations;

namespace SongBook2.Migrations
{
    public partial class removeSongColor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Article");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Article",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
