using Entities.UserS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.MangaS
{
    public class MangaComentary
    {
        public int Id { get; set; }
        public Manga Manga { get; set; }
        public int MangaId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Comentary { get; set; }
        public DateTime DataComentary { get; set; }
    }
}
