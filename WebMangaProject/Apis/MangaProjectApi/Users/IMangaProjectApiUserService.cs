using Entities.MangaS;
using Entities.UserS;
using Shared;
using Shared.Models.User;
using Shared.Responses;

namespace MvcPresentationLayer.Apis.MangaProjectApi
{
    public interface IMangaProjectApiUserService : IMangaProjectApiService<User>
    {
        Task<SingleResponseWToken<User>> Login(UserLogin user);
        Task<Response> Insert(UserCreate item, string token);
        Task<Response> Update(UserProfileUpdate userProfileUpdate, string token);

    }
}
