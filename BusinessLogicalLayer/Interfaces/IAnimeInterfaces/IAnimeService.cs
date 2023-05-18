using Entities.AnimeS;
using Shared;

namespace BusinessLogicalLayer.Interfaces.IAnimeInterfaces
{
    public interface IAnimeService :ICRUD<Anime>,IAnimePost,IAnimeGet
    {
    }
}
