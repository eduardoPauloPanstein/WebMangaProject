using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Manga
{
    public class MangaCatalog
    {
        public int Id { get; set; }
        public string CanonicalTitle { get; set; }
        public string PosterImageLink { get; set; }

        public static Expression<Func<Entities.MangaS.Manga, MangaCatalog>> Projection => x => new MangaCatalog()
        {
            Id = x.Id,
            CanonicalTitle = x.CanonicalTitle,
            PosterImageLink = x.PosterImageLink
        };
    }
}
