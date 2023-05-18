using Shared.Responses;

namespace MvcPresentationLayer.Apis.MangaProjectApi
{
    public interface IMangaProjectUserItemApi<T,W>
    {
        Task<Response> Insert(T Item, string? token);
        Task<SingleResponse<T>> Get(int id, string? token);
        Task<DataResponse<T>> Get(int skip, int take, string token);
        Task<Response> Update(T Item, string? token);
        Task<Response> Delete(int id, string? token);
        Task<DataResponse<T>> GetByUser(int userid, string? token);

        Task<DataResponse<W>> GetUserList(int userid, string? token);
        Task<DataResponse<W>> GetUserFavorites(int userid, string? token);
        Task<DataResponse<W>> GetUserRecommendations(int userid, string? token);
    }
}
