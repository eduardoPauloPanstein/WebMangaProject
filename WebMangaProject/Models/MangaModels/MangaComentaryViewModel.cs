using Entities.MangaS;

namespace MvcPresentationLayer.Models.MangaModels
{
    public class MangaComentaryViewModel
    {
        public int Id { get; set; }
        public Manga Manga { get; set; }
        public int MangaId { get; set; }
        public int UserId { get; set; }
        public Entities.UserS.User User { get; set; }
        public string Comentary { get; set; }
        public DateTime DataComentary { get; set; }
    }
}
