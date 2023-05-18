using Entities.MangaS;
using Shared;
using Shared.Responses;

namespace BusinessLogicalLayer.Interfaces.IUserComentaryService
{
    public interface IMangaComentary :ICRUD<MangaComentary>
    {
        Task<DataResponse<MangaComentary>> GetByUser(int userid);
        Task<DataResponse<MangaComentary>> GetByManga(int MangaID);

    }
}
