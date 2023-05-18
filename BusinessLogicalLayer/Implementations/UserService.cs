using BLL.Extensions;
using BusinessLogicalLayer.Interfaces.IUserInterfaces;
using BusinessLogicalLayer.Utilities;
using BusinessLogicalLayer.Validators.User;
using DataAccessLayer.UnitOfWork;
using Entities.Enums;
using Entities.UserS;
using Shared;
using Shared.Models.User;
using Shared.Responses;

namespace BusinessLogicalLayer.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void CreateAdmin()
        {
            string password = HashGenerator.ComputeSha256Hash("admin");

            User adm = new()
            {
                Nickname = "admin",
                Password = password,
                Email = "admin@gmail.com",
                Role = UserRoles.Admin,
                Active = true,
            };
            _unitOfWork.UserRepository.CreateAdmin(adm);
            _unitOfWork.Commit();
        }
        public async Task<Response> Delete(int id)
        {
            if (id < 0)
                return ResponseFactory.CreateInstance().CreateFailedResponse();

            var response = await _unitOfWork.UserRepository.Delete(id);
            if (!response.HasSuccess)
                return response;

            return await _unitOfWork.Commit();
        }
        public Task<Response> Insert(UserCreate userCreate)
        {
            if (!userCreate.PasswordHasBeenConfirmed)
                ResponseFactory.CreateInstance().CreateFailedResponse("passwords do not match");
            User user = new()
            {
                Nickname=userCreate.Nickname,
                Password=userCreate.Password,
                Email=userCreate.Email,
            };

            return Insert(user);
        }

        public async Task<Response> Insert(User user)
        {
            Response response = new UserInsertValidator().Validate(user).ConvertToResponse();
            if (!response.HasSuccess)
                return response;
            user.Password = HashGenerator.ComputeSha256Hash(user.Password);

            user.EnableEntity();
            response = await _unitOfWork.UserRepository.Insert(user);
            if (!response.HasSuccess)
                return response;

            return await _unitOfWork.Commit();

        }

        public async Task<SingleResponse<User>> Get(int id)
        {
            return await _unitOfWork.UserRepository.Get(id);
        }

        public async Task<DataResponse<User>> Get(int skip, int take)
        {
            return await _unitOfWork.UserRepository.Get(skip, take);
        }

        public async Task<Response> Update(UserProfileUpdate userProfile)
        {
            User userForValidate = new()
            {
                Id = userProfile.Id,
                Nickname = userProfile.Nickname,
                About = userProfile.About,
                AvatarImageFileLocation = userProfile.AvatarImageFileLocation,
                CoverImageFileLocation = userProfile.CoverImageFileLocation
            };

            Response response = new UserUpdateValidator().Validate(userForValidate).ConvertToResponse();
            if (!response.HasSuccess)
                return response;

            var responseGetUser = await _unitOfWork.UserRepository.Get(userProfile.Id);
            if (!responseGetUser.HasSuccess)
                return responseGetUser;

            User userDb = responseGetUser.Item;
            userDb.AccessEntity();

            userDb.Nickname = userProfile.Nickname;
            userDb.About = userProfile.About;

            if (userProfile.AvatarImageFileLocation != null)
                userDb.AvatarImageFileLocation = userProfile.AvatarImageFileLocation;
            if (userProfile.CoverImageFileLocation != null)
                userDb.CoverImageFileLocation = userProfile.CoverImageFileLocation;
            await _unitOfWork.UserRepository.Update(userDb);
            return await _unitOfWork.CommitForUser();
        }

        public async Task<SingleResponse<User>> Login(UserLogin user)
        {
            user.Password = HashGenerator.ComputeSha256Hash(user.Password);
            return await _unitOfWork.UserRepository.Login(user);
        }

        public async Task<Response> Update(User user)
        {
            Response response = new UserUpdateValidator().Validate(user).ConvertToResponse();
            if (!response.HasSuccess)
                return response;

            var responseGetUser = await _unitOfWork.UserRepository.Get(user.Id);
            user.AccessEntity();

            if (!responseGetUser.HasSuccess)
                return responseGetUser;

            User userDb = responseGetUser.Item;

            userDb.Nickname = user.Nickname;
            userDb.About = user.About;

            if (user.AvatarImageFileLocation != null)
                userDb.AvatarImageFileLocation = user.AvatarImageFileLocation;
            if (user.CoverImageFileLocation != null)
                userDb.CoverImageFileLocation = user.CoverImageFileLocation;
            await _unitOfWork.UserRepository.Update(user);
            return await _unitOfWork.CommitForUser();
        }
    }
}
