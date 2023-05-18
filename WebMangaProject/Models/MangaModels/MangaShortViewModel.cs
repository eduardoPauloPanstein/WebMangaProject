using Entities.MangaS;
using System.ComponentModel.DataAnnotations;

namespace MvcPresentationLayer.Models.MangaModels
{
    public class MangaShortViewModel
    {
        public int Id { get; set; }
        public string CanonicalTitle { get; set; }
        public string PosterImageLink { get; set; }
    }
}
