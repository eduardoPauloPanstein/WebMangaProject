using Shared.Models.User;
using Shared.Responses;

namespace BusinessLogicalLayer.Interfaces.IUserInterfaces
{
    public interface IUserPost
    {
        Task<Response> Insert(UserCreate userCreate);
        Task<Response> Update(UserProfileUpdate userProfileUpdate);
    }
}
