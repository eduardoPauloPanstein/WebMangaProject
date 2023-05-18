using Shared.Responses;

namespace Shared
{
    public interface ICrudUserItem<T,W>
    {
        Task<Response> Insert(T Item,int Score);
        Task<SingleResponse<T>> Get(int id);
        Task<DataResponse<T>> Get(int skip, int take);
        Task<Response> Update(T Item);
        Task<Response> Delete(int id);
        Task<DataResponse<T>> GetByUser(int userid);

        Task<DataResponse<W>> GetUserList(int userid);
        Task<DataResponse<W>> GetUserFavorites(int userid);
        Task<DataResponse<W>> GetUserRecommendations(int userid);
    }
}
