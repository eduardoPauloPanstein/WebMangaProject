using Entities.AnimeS;
using Shared;
using Shared.Responses;

namespace BusinessLogicalLayer.Interfaces.IUserComentaryService
{
    public interface IAnimeComentary : ICRUD<AnimeComentary>
    {
        Task<DataResponse<AnimeComentary>> GetByUser(int userid);
        Task<DataResponse<AnimeComentary>> GetByAnime(int animeID);

    }
}
