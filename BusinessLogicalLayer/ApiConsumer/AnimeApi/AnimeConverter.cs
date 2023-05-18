using Entities.AnimeS;
using Entities.Enums;

namespace BusinessLogicalLayer.ApiConsumer.NovaPasta
{
    public class AnimeConverter
    {
        public static Anime ConvertDTOToAnime(RootANI mangaRootDTO)
        {
            var item = mangaRootDTO.data;
            AnimeSTitles titles = new()
            {
                En_jp = item.attributes.titles.en_jp,
                Ja_jp = item.attributes.titles.ja_jp,
            };
            AnimeRatingFrequencies Rating = new();
            Rating.Id = Convert.ToInt32(item.id);

            MangaStatus status;
            bool hasStatusParse = Enum.TryParse(item.attributes.status, out status);
            //_ = Enum.TryParse(item.attributes.status, out MangaStatus status);

            Anime anime = new()
            {
                Id = Convert.ToInt32(item.id),
                name = item.attributes.slug,
                description = item.attributes.description,
                synopsis = item.attributes.synopsis,
                AnimeTitles = titles,
                canonicalTitle = item.attributes.canonicalTitle,
                CreatedAt = item.attributes.createdAt,
                UpdatedAt = item.attributes.updatedAt,
                averageRating = item.attributes.averageRating,
                AnimeRatingFrequencies = Rating,
                userCount = item.attributes.userCount,
                favoritesCount = item.attributes.favoritesCount,
                popularityRank = item.attributes.popularityRank,
                startDate = item.attributes.startDate,
                endDate = item.attributes.endDate,
                ratingRank = item.attributes.ratingRank,
                ageRating = item.attributes.ageRating,

                ageRatingGuide = item.attributes.ageRatingGuide,
                subtype = item.attributes.subtype,
                AnimePosterImage = item.attributes.posterImage?.original,
                AnimeCoverImage = item.attributes.coverImage?.original,
                episodeCount = item.attributes.episodeCount,
                episodeLength = item.attributes.episodeLength,
                totalLength = item.attributes.totalLength,
                youtubeVideoId = item.attributes.youtubeVideoId,
                showType = item.attributes.showType,
                status = status,


            };

            return anime;
        }
    }
}
