using Entities.MangaS;
using Shared;

namespace DataAccessLayer.Interfaces.IMangaInterfaces
{
    public interface IMangaDAL : IMangaGet, IMangaPost,ICRUD<Manga>
    {
        
    }
}
