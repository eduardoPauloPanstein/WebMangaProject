using Entities.Enums;

namespace Entities.MangaS
{
    public class Manga : Entity
    {
        public string? CanonicalTitle { get; set; }
        public MangaTitles? Titles { get; set; }
        public string Synopsis { get; set; }
        public string? AverageRating { get; set; }
        public RatingFrequencies? RatingFrequencies { get; set; }
        public int? RatingRank { get; set; }
        public int? PopularityRank { get; set; }
        public int? UserCount { get; set; }
        public int? FavoritesCount { get; set; }
        public int? VolumeCount { get; set; }
        public string? Serialization { get; set; } 
        public string PosterImageLink { get; set; }
        public string? CoverImageLink { get; set; }
        public string? Subtype { get; set; }
        public int? ChapterCount { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public MangaStatus? Status { get; set; } 
        public ICollection<Category> Genres { get; set; }
        public ICollection<MangaComentary> Comentaries { get; set; }

    }
}
