using BusinessLogicalLayer.Interfaces.IUserItemService;
using DataAccessLayer.UnitOfWork;
using Entities.MangaS;
using Entities.UserS;
using Shared.Responses;

namespace BusinessLogicalLayer.Implementations.UserItemService
{
    public class UserMangaItemService : IUserMangaItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserMangaItemService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<Response> Delete(int id)
        {
            await _unitOfWork.UserMangaItemRepository.Delete(id);
            return await _unitOfWork.Commit();
        }

        public async Task<SingleResponse<UserMangaItem>> Get(int id)
        {
            return await _unitOfWork.UserMangaItemRepository.Get(id);
        }

        public async Task<DataResponse<UserMangaItem>> Get(int skip, int take)
        {
            return await _unitOfWork.UserMangaItemRepository.Get(skip, take);
        }

        public async Task<DataResponse<UserMangaItem>> GetByUser(int userid)
        {
            return await _unitOfWork.UserMangaItemRepository.GetByUser(userid);
        }

        public async Task<DataResponse<Manga>> GetUserFavorites(int userid)
        {
            return await _unitOfWork.UserMangaItemRepository.GetUserFavorites(userid);
        }

        public async Task<DataResponse<Manga>> GetUserList(int userid)
        {
            return await _unitOfWork.UserMangaItemRepository.GetUserList(userid);
        }

        public async Task<DataResponse<Manga>> GetUserRecommendations(int userid)
        {
            return await _unitOfWork.UserMangaItemRepository.GetUserRecommendations(userid);
        }

        public async Task<Response> Insert(UserMangaItem Item,int score)
        {
            await _unitOfWork.UserMangaItemRepository.Insert(Item,score);
            return await _unitOfWork.Commit();
        }

        public async Task<Response> Update(UserMangaItem Item)
        {
            await _unitOfWork.UserMangaItemRepository.Update(Item);
            return await _unitOfWork.Commit();
        }
    }
}
