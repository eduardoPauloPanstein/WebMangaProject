using Entities.UserS;
using Shared;
using Shared.Models.User;
using Shared.Responses;

namespace DataAccessLayer.Interfaces.IUSerInterfaces
{
    public interface IUserDAL :IUserGet , IUserPost ,ICRUD<User>
    {
        Task<SingleResponse<User>> Login(UserLogin user);
        void CreateAdmin(User adm);
    }
}
