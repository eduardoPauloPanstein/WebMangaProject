using System.Linq.Expressions;

namespace Shared.Models.Anime
{
    //https://benjii.me/2018/01/expression-projection-magic-entity-framework-core/
    public class AnimeCatalog
    {
        public int Id { get; set; }
        public string canonicalTitle { get; set; }
        public string? AnimePosterImage { get; set; }

        public static Expression<Func<Entities.AnimeS.Anime, AnimeCatalog>> Projection => x => new AnimeCatalog()
        {
            Id = x.Id,
            canonicalTitle = x.canonicalTitle,
            AnimePosterImage = x.AnimePosterImage
        };
    }

}
