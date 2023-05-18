using BusinessLogicalLayer.Interfaces.IAnimeInterfaces;
using DataAccessLayer.Interfaces.IAnimeInterfaces;
using DataAccessLayer.UnitOfWork;
using Entities.AnimeS;
using Entities.MangaS;
using Shared.Models.Anime;
using Shared.Responses;

namespace BusinessLogicalLayer.Implementations
{
    public class AnimeService : IAnimeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnimeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<Response> Insert(Anime Item)
        {
            //Validate
            Item.EnableEntity();
            await _unitOfWork.AnimeRepository.Insert(Item);
            return await _unitOfWork.Commit();
        }

        public async Task<Response> Update(Anime Item)
        {
            await _unitOfWork.AnimeRepository.Update(Item);
            return await _unitOfWork.Commit();
        }

        public async Task<Response> Delete(int id)
        {
            await _unitOfWork.AnimeRepository.Delete(id);
            return await _unitOfWork.Commit();
        }

        public async Task<SingleResponse<Anime>> Get(int id)
        {
            return await _unitOfWork.AnimeRepository.Get(id);
        }

        public async Task<DataResponse<Anime>> Get(int skip, int take)
        {
            return await _unitOfWork.AnimeRepository.Get(skip, take);
        }

        public async Task<DataResponse<Anime>> Get(string name)
        {
            return await _unitOfWork.AnimeRepository.Get(name);
        }

        public async Task<SingleResponse<Anime>> GetComplete(int ID)
        {
            return await _unitOfWork.AnimeRepository.GetComplete(ID);
        }

        public async Task<int> GetLastIndex()
        {
            return await _unitOfWork.AnimeRepository.GetLastIndex();
        }

        public async Task<int> GetLastIndexCategory()
        {
            return await _unitOfWork.AnimeRepository.GetLastIndexCategory();
        }
        public async Task<DataResponse<AnimeCatalog>> GetByFavorites(int skip, int take)
        {
            return await _unitOfWork.AnimeRepository.GetByFavorites(skip, take);
        }

        public async Task<DataResponse<AnimeCatalog>> GetByRating(int skip, int take)
        {
            return await _unitOfWork.AnimeRepository.GetByRating(skip, take);
        }

        public async Task<DataResponse<AnimeCatalog>> GetByUserCount(int skip, int take)
        {
            return await _unitOfWork.AnimeRepository.GetByUserCount(skip,take);
        }

        public async Task<DataResponse<Anime>> GetByCategory(int ID)
        {
            return await _unitOfWork.AnimeRepository.GetByCategory(ID);
        }

        public async Task<DataResponse<AnimeCatalog>> GetByPopularity(int skip, int take)
        {
            return await _unitOfWork.AnimeRepository.GetByPopularity(skip, take);
        }
    }
}
