using Entities;
using Entities.AnimeS;
using Entities.Enums;

namespace MvcPresentationLayer.Models.AnimeModel
{
    public class AnimeOnpageViewModel
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Name { get; set; }
        public string? Synopsis { get; set; }
        public string? Description { get; set; }
        public AnimeSTitles? AnimeTitles { get; set; }
        public string? CanonicalTitle { get; set; }
        public string? AverageRating { get; set; }
        public AnimeRatingFrequencies? AnimeRatingFrequencies { get; set; }
        public int? UserCount { get; set; }
        public int? FavoritesCount { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int? PopularityRank { get; set; }
        public int? RatingRank { get; set; }
        public string? AgeRating { get; set; }
        public string? AgeRatingGuide { get; set; }
        public string? Subtype { get; set; }
        public MangaStatus? Status { get; set; }
        public string? AnimePosterImage { get; set; }
        public string? AnimeCoverImage { get; set; }
        public int? EpisodeCount { get; set; }
        public int? EpisodeLength { get; set; }
        public int? TotalLength { get; set; }
        public string? YoutubeVideoId { get; set; }
        public string? ShowType { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<AnimeComentary> Comentaries { get; set; }
    }
}
