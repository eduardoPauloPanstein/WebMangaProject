using Entities.AnimeS;
using Entities.MangaS;
using Shared;
using Shared.Responses;

namespace DataAccessLayer.Interfaces.IUserComentary
{
    public interface IMangaComentaryDAL : ICRUD<MangaComentary>
    {
        Task<DataResponse<MangaComentary>> GetByUser(int userid);
        Task<DataResponse<MangaComentary>> GetByManga(int MangaID);

    }
}
