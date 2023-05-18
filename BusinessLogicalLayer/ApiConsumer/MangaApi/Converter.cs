using Entities.Enums;
using Entities.MangaS;

namespace BusinessLogicalLayer.ApiConsumer.MangaApi
{
    public class ConverterToCategory
    {
        public static Manga ConvertDTOToManga(Root mangaRootDTO)
        {
            var item = mangaRootDTO.data;
            MangaTitles titles = new()
            {
                En = item.attributes.titles.en,
                En_jp = item.attributes.titles.en_jp,
                Ja_jp = item.attributes.titles.ja_jp,

            };

            MangaStatus status;
            bool hasStatusParse = Enum.TryParse(item.attributes.status, out status);
            //_ = Enum.TryParse(item.attributes.status, out MangaStatus status);

            Entities.MangaS.RatingFrequencies Rating = new();
            Rating.Id = Convert.ToInt32(item.id);

            Manga manga = new()
            {
                Id = Convert.ToInt32(item.id),
                Synopsis = item.attributes.synopsis,
                Titles = titles,
                CanonicalTitle = item.attributes.canonicalTitle,
                AverageRating = item.attributes.averageRating,
                RatingFrequencies = Rating,
                RatingRank = item.attributes.ratingRank,
                PopularityRank = item.attributes.popularityRank,
                UserCount = item.attributes.userCount,
                FavoritesCount = item.attributes.favoritesCount,
                StartDate = item.attributes.startDate,
                EndDate = item.attributes.endDate,
                Status = status,
                VolumeCount = item.attributes.volumeCount,
                Serialization = item.attributes.serialization,
                PosterImageLink = item.attributes.posterImage?.original,
                CoverImageLink = item.attributes.coverImage?.original,
                Subtype = item.attributes.subtype,
                ChapterCount = item.attributes.chapterCount
            };

            return manga;
        }
    }
}
