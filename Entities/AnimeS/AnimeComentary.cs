using Entities.UserS;

namespace Entities.AnimeS
{
    public class AnimeComentary
    {
        public int Id { get; set; }

        public Anime Anime { get; set; }
        public int AnimeId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public string Comentary { get; set; }
        public DateTime DataComentary { get; set; }
    }
}
