namespace MvcPresentationLayer.Models.MangaModels
{
    public class MangasForHomeViewModel
    {
        public List<MangaShortViewModel> MangasFavorites { get; set; }
        public List<MangaShortViewModel> MangasByCount { get; set; }
        public List<MangaShortViewModel> MangasByRating { get; set; }

    }
}
