using Entities.AnimeS;

namespace MvcPresentationLayer.Models.AnimeModel
{
    public class AnimeComentaryViewModel
    {
        public int Id { get; set; }

        public Anime Anime { get; set; }
        public int AnimeId { get; set; }

        public int UserId { get; set; }
        public Entities.UserS.User User { get; set; }
        public string Comentary { get; set; }
        public DateTime DataComentary { get; set; }
    }
}
