using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class MangaAnimeComentari : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chapter",
                table: "UserAnimeItem");

            migrationBuilder.RenameColumn(
                name: "Volume",
                table: "UserAnimeItem",
                newName: "TotalRewachtes");

            migrationBuilder.RenameColumn(
                name: "TotalRereads",
                table: "UserAnimeItem",
                newName: "Episode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalRewachtes",
                table: "UserAnimeItem",
                newName: "Volume");

            migrationBuilder.RenameColumn(
                name: "Episode",
                table: "UserAnimeItem",
                newName: "TotalRereads");

            migrationBuilder.AddColumn<int>(
                name: "Chapter",
                table: "UserAnimeItem",
                type: "int",
                nullable: true);
        }
    }
}
