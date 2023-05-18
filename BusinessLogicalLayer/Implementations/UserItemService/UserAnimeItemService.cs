using BusinessLogicalLayer.Interfaces.IUserItemService;
using DataAccessLayer.UnitOfWork;
using Entities.AnimeS;
using Entities.UserS;
using Shared.Responses;

namespace BusinessLogicalLayer.Implementations.UserItemService
{
    public class UserAnimeItemService : IUserAnimeItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserAnimeItemService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<Response> Delete(int id)
        {
            await _unitOfWork.UserAnimeItemRepository.Delete(id);
            return await _unitOfWork.Commit();
        }
        public async Task<SingleResponse<UserAnimeItem>> Get(int id)
        {
            return await _unitOfWork.UserAnimeItemRepository.Get(id);
        }

        public async Task<DataResponse<UserAnimeItem>> Get(int skip, int take)
        {
            return await _unitOfWork.UserAnimeItemRepository.Get(skip, take);
        }
        public async Task<DataResponse<UserAnimeItem>> GetByUser(int userid)
        {
            return await _unitOfWork.UserAnimeItemRepository.GetByUser(userid);

        }
        public async Task<DataResponse<Anime>> GetUserFavorites(int userid)
        {
            return await _unitOfWork.UserAnimeItemRepository.GetUserFavorites(userid);

        }
        public async Task<DataResponse<Anime>> GetUserList(int userid)
        {
            return await _unitOfWork.UserAnimeItemRepository.GetUserList(userid);
        }
        public async Task<DataResponse<Anime>> GetUserRecommendations(int userid)
        {
            return await _unitOfWork.UserAnimeItemRepository.GetUserRecommendations(userid);
        }
     
        public async Task<Response> Insert(UserAnimeItem Item, int Score)
        {
            await _unitOfWork.UserAnimeItemRepository.Insert(Item, Score);
            return await _unitOfWork.Commit();
        }
        public async Task<Response> Update(UserAnimeItem Item)
        {
            await _unitOfWork.UserAnimeItemRepository.Update(Item);
            return await _unitOfWork.Commit();
        }
    }
}
