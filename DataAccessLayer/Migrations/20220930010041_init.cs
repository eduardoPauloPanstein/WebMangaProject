using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimeRatingFrequencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    _1 = table.Column<int>(type: "int", nullable: true),
                    _2 = table.Column<int>(type: "int", nullable: true),
                    _3 = table.Column<int>(type: "int", nullable: true),
                    _4 = table.Column<int>(type: "int", nullable: true),
                    _5 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeRatingFrequencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnimeSTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    En_jp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ja_jp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeSTitles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MangasRatingFrequencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    _1 = table.Column<int>(type: "int", nullable: true),
                    _2 = table.Column<int>(type: "int", nullable: true),
                    _3 = table.Column<int>(type: "int", nullable: true),
                    _4 = table.Column<int>(type: "int", nullable: true),
                    _5 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangasRatingFrequencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MangaTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    En = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    En_jp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ja_jp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaTitles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nickname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FavoritesCount = table.Column<int>(type: "int", nullable: false),
                    ReviewsCount = table.Column<int>(type: "int", nullable: false),
                    AvatarImageFileLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverImageFileLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeepLogged = table.Column<bool>(type: "bit", nullable: false),
                    AccessCount = table.Column<int>(type: "int", nullable: false),
                    AccessUserId = table.Column<int>(type: "int", nullable: false),
                    LastAccess = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Anime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    synopsis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnimeTitlesId = table.Column<int>(type: "int", nullable: true),
                    canonicalTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    averageRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnimeRatingFrequenciesId = table.Column<int>(type: "int", nullable: true),
                    userCount = table.Column<int>(type: "int", nullable: true),
                    favoritesCount = table.Column<int>(type: "int", nullable: true),
                    popularityRank = table.Column<int>(type: "int", nullable: true),
                    startDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    endDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ratingRank = table.Column<int>(type: "int", nullable: true),
                    ageRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ageRatingGuide = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subtype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    AnimePosterImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnimeCoverImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    episodeCount = table.Column<int>(type: "int", nullable: true),
                    episodeLength = table.Column<int>(type: "int", nullable: true),
                    totalLength = table.Column<int>(type: "int", nullable: true),
                    youtubeVideoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    showType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccessCount = table.Column<int>(type: "int", nullable: false),
                    AccessUserId = table.Column<int>(type: "int", nullable: false),
                    LastAccess = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Anime_AnimeRatingFrequencies_AnimeRatingFrequenciesId",
                        column: x => x.AnimeRatingFrequenciesId,
                        principalTable: "AnimeRatingFrequencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Anime_AnimeSTitles_AnimeTitlesId",
                        column: x => x.AnimeTitlesId,
                        principalTable: "AnimeSTitles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Mangas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Synopsis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitlesId = table.Column<int>(type: "int", nullable: true),
                    CanonicalTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AverageRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RatingFrequenciesId = table.Column<int>(type: "int", nullable: true),
                    RatingRank = table.Column<int>(type: "int", nullable: true),
                    PopularityRank = table.Column<int>(type: "int", nullable: true),
                    UserCount = table.Column<int>(type: "int", nullable: true),
                    FavoritesCount = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    VolumeCount = table.Column<int>(type: "int", nullable: true),
                    Serialization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PosterImageLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverImageLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subtype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChapterCount = table.Column<int>(type: "int", nullable: true),
                    AccessCount = table.Column<int>(type: "int", nullable: false),
                    AccessUserId = table.Column<int>(type: "int", nullable: false),
                    LastAccess = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mangas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mangas_MangasRatingFrequencies_RatingFrequenciesId",
                        column: x => x.RatingFrequenciesId,
                        principalTable: "MangasRatingFrequencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Mangas_MangaTitles_TitlesId",
                        column: x => x.TitlesId,
                        principalTable: "MangaTitles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AnimeCategory",
                columns: table => new
                {
                    AnimesIDId = table.Column<int>(type: "int", nullable: false),
                    CategoriesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeCategory", x => new { x.AnimesIDId, x.CategoriesID });
                    table.ForeignKey(
                        name: "FK_AnimeCategory_Anime_AnimesIDId",
                        column: x => x.AnimesIDId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeCategory_Category_CategoriesID",
                        column: x => x.CategoriesID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeComentary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Comentary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataComentary = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeComentary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimeComentary_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeComentary_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAnimeItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: true),
                    TotalRereads = table.Column<int>(type: "int", nullable: true),
                    Chapter = table.Column<int>(type: "int", nullable: true),
                    Volume = table.Column<int>(type: "int", nullable: true),
                    PrivateNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Private = table.Column<bool>(type: "bit", nullable: false),
                    Favorite = table.Column<bool>(type: "bit", nullable: false),
                    AccessCount = table.Column<int>(type: "int", nullable: false),
                    AccessUserId = table.Column<int>(type: "int", nullable: false),
                    LastAccess = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnimeItem", x => x.Id);
                    table.ForeignKey(
                        name: "fk_Animeuser",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAnimeItem_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryManga",
                columns: table => new
                {
                    GenresID = table.Column<int>(type: "int", nullable: false),
                    MangasIDId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryManga", x => new { x.GenresID, x.MangasIDId });
                    table.ForeignKey(
                        name: "FK_CategoryManga_Category_GenresID",
                        column: x => x.GenresID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryManga_Mangas_MangasIDId",
                        column: x => x.MangasIDId,
                        principalTable: "Mangas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MangaComentary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MangaId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Comentary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataComentary = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaComentary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MangaComentary_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MangaComentary_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMangaItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MangaId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: true),
                    TotalRereads = table.Column<int>(type: "int", nullable: true),
                    Chapter = table.Column<int>(type: "int", nullable: true),
                    Volume = table.Column<int>(type: "int", nullable: true),
                    PrivateNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Private = table.Column<bool>(type: "bit", nullable: false),
                    Favorite = table.Column<bool>(type: "bit", nullable: false),
                    AccessCount = table.Column<int>(type: "int", nullable: false),
                    AccessUserId = table.Column<int>(type: "int", nullable: false),
                    LastAccess = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMangaItem", x => x.Id);
                    table.ForeignKey(
                        name: "fk_mangauser",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMangaItem_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anime_AnimeRatingFrequenciesId",
                table: "Anime",
                column: "AnimeRatingFrequenciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Anime_AnimeTitlesId",
                table: "Anime",
                column: "AnimeTitlesId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeCategory_CategoriesID",
                table: "AnimeCategory",
                column: "CategoriesID");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeComentary_AnimeId",
                table: "AnimeComentary",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeComentary_UserId",
                table: "AnimeComentary",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryManga_MangasIDId",
                table: "CategoryManga",
                column: "MangasIDId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaComentary_MangaId",
                table: "MangaComentary",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaComentary_UserId",
                table: "MangaComentary",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_RatingFrequenciesId",
                table: "Mangas",
                column: "RatingFrequenciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_TitlesId",
                table: "Mangas",
                column: "TitlesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimeItem_AnimeId",
                table: "UserAnimeItem",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimeItem_UserId",
                table: "UserAnimeItem",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMangaItem_MangaId",
                table: "UserMangaItem",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMangaItem_UserId",
                table: "UserMangaItem",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeCategory");

            migrationBuilder.DropTable(
                name: "AnimeComentary");

            migrationBuilder.DropTable(
                name: "CategoryManga");

            migrationBuilder.DropTable(
                name: "MangaComentary");

            migrationBuilder.DropTable(
                name: "UserAnimeItem");

            migrationBuilder.DropTable(
                name: "UserMangaItem");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Anime");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Mangas");

            migrationBuilder.DropTable(
                name: "AnimeRatingFrequencies");

            migrationBuilder.DropTable(
                name: "AnimeSTitles");

            migrationBuilder.DropTable(
                name: "MangasRatingFrequencies");

            migrationBuilder.DropTable(
                name: "MangaTitles");
        }
    }
}
