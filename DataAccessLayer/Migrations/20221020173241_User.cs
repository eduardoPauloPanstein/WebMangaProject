using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "UserMangaItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "UserAnimeItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserMangaItem_UserId1",
                table: "UserMangaItem",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimeItem_UserId1",
                table: "UserAnimeItem",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnimeItem_Users_UserId1",
                table: "UserAnimeItem",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMangaItem_Users_UserId1",
                table: "UserMangaItem",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnimeItem_Users_UserId1",
                table: "UserAnimeItem");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMangaItem_Users_UserId1",
                table: "UserMangaItem");

            migrationBuilder.DropIndex(
                name: "IX_UserMangaItem_UserId1",
                table: "UserMangaItem");

            migrationBuilder.DropIndex(
                name: "IX_UserAnimeItem_UserId1",
                table: "UserAnimeItem");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserMangaItem");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserAnimeItem");
        }
    }
}
