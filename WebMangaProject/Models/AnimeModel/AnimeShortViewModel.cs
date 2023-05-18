using Entities.AnimeS;
using System.ComponentModel.DataAnnotations;

namespace MvcPresentationLayer.Models.AnimeModel
{
    public class AnimeShortViewModel
    {
        public int Id { get; set; }
        public string canonicalTitle { get; set; }
        public string? AnimePosterImage { get; set; }
    }
}
