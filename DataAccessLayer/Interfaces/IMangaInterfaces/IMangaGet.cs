using Entities.MangaS;
using Shared.Interfaces;
using Shared.Models.Manga;
using Shared.Responses;

namespace DataAccessLayer.Interfaces.IMangaInterfaces
{
    public interface IMangaGet : IUsualGetInterfaces<MangaCatalog,Manga>
    {
    }
}
