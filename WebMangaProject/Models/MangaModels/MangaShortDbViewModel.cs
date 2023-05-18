using System.ComponentModel.DataAnnotations;

namespace MvcPresentationLayer.Models.MangaModels
{
    public class MangaShortDbViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "Title")]
        public string CanonicalTitle { get; set; }
        [Display(Name = "Favorites Count")]
        public int? FavoritesCount { get; set; }

    }
}
