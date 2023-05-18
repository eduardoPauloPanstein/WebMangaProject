using Entities.UserS;
using Shared;
using Shared.Models.User;
using Shared.Responses;

namespace BusinessLogicalLayer.Interfaces.IUserInterfaces
{
    public interface IUserService :IUserGet,IUserPost,ICRUD<User>
    {
        Task<SingleResponse<User>> Login(UserLogin user);
        void CreateAdmin();
    }
}
