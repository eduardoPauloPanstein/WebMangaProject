using MvcPresentationLayer.Models.MangaModels;

namespace MvcPresentationLayer.Models.AnimeModel
{
    public class AnimeItemModalViewModel
    {
        public UserFavoriteAnimeViewModel UserAnimeItem { get; set; }
        public AnimeOnpageViewModel Anime { get; set; }
        public AnimeComentaryViewModel AnimeComentary { get; set; }
        public List<AnimeComentaryViewModel> Comments { get; set; }
        public List<AnimeShortViewModel> Recommendations { get; set; }
    }
}
