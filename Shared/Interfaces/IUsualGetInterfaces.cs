using Shared.Responses;

namespace Shared.Interfaces
{
    public interface IUsualGetInterfaces<T,W>
    {
        Task<DataResponse<T>> GetByUserCount(int skip, int take);
        Task<DataResponse<T>> GetByFavorites(int skip, int take);
        Task<DataResponse<T>> GetByRating(int skip, int take);
        Task<DataResponse<T>> GetByPopularity(int skip, int take);
        Task<DataResponse<W>> GetByCategory(int ID);
        Task<DataResponse<W>> Get(string name);
        Task<SingleResponse<W>> GetComplete(int ID);
        Task<int> GetLastIndexCategory();
        Task<int> GetLastIndex();
    }
}
