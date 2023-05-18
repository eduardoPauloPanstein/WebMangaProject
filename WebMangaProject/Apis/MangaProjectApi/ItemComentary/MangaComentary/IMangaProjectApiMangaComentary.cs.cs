using Shared.Responses;

namespace MvcPresentationLayer.Apis.MangaProjectApi.ItemComentary.MangaComentary
{
    public interface IMangaProjectApiMangaComentary : IMangaProjectApiService<Entities.MangaS.MangaComentary>
    {
        Task<DataResponse<Entities.MangaS.MangaComentary>> GetByUser(int userid);
        Task<DataResponse<Entities.MangaS.MangaComentary>> GetByManga(int MangaID);


    }
}
