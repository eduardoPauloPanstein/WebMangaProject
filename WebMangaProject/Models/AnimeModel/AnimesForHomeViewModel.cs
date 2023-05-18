namespace MvcPresentationLayer.Models.AnimeModel
{
    public class AnimesForHomeViewModel
    {
        public List<AnimeShortViewModel> AnimesFavorites { get; set; }
        public List<AnimeShortViewModel> AnimesByCount { get; set; }
        public List<AnimeShortViewModel> AnimesByRating { get; set; }
    }
}
