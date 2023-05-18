using Entities.AnimeS;
using Shared;
using Shared.Responses;

namespace DataAccessLayer.Interfaces.IUserComentary
{
    public interface IAnimeComentaryDAL : ICRUD<AnimeComentary>
    {
        Task<DataResponse<AnimeComentary>> GetByUser(int userid);
        Task<DataResponse<AnimeComentary>> GetByAnime(int animeID);


    }
}
