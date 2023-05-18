using Entities;
using Entities.MangaS;
using Shared.Responses;

namespace DataAccessLayer.Interfaces.IMangaInterfaces
{
    public interface IMangaPost
    {
        Task<Response> InsertCategory(Category id);
    }
}
