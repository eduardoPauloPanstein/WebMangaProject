using Shared.Responses;

namespace Shared
{
    public interface ICRUD<T>
    {
        Task<Response> Insert(T Item);
        Task<SingleResponse<T>> Get(int id);
        Task<DataResponse<T>> Get(int skip, int take);
        Task<Response> Update(T Item);
        Task<Response> Delete(int id);
    }
}
