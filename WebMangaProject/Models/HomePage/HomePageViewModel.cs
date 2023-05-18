using MvcPresentationLayer.Models.AnimeModel;
using MvcPresentationLayer.Models.MangaModels;

namespace MvcPresentationLayer.Models.HomePage
{
    public class HomePageViewModel
    {
        public List<MangaShortViewModel> MangasFavorites { get; set; }
        public List<MangaShortViewModel> MangasByCount { get; set; }
        public List<MangaShortViewModel> MangasByRating { get; set; }

        public List<AnimeShortViewModel> AnimesFavorites { get; set; }
        public List<AnimeShortViewModel> AnimesByCount { get; set; }
        public List<AnimeShortViewModel> AnimesByRating { get; set; }
    }
}
