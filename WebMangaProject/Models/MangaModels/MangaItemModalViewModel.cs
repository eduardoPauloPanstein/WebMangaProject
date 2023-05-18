namespace MvcPresentationLayer.Models.MangaModels
{
    public class MangaItemModalViewModel
    {
        public UserFavoriteMangaViewModel UserMangaItem { get; set; }
        public MangaOnPageViewModel Manga { get; set; }
        public MangaComentaryViewModel MangaComentary { get; set; }
        public List<MangaComentaryViewModel> Comments { get; set; }
        public List<MangaShortViewModel> Recommendations { get; set; }
    }
}
