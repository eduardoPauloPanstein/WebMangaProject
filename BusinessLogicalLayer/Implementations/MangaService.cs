using BusinessLogicalLayer.Interfaces.IMangaInterfaces;
using DataAccessLayer.UnitOfWork;
using Entities;
using Entities.MangaS;
using Shared.Models.Manga;
using Shared.Responses;

namespace BusinessLogicalLayer.Implementations
{
    public class MangaService : IMangaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MangaService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<Response> Insert(Manga manga)
        {
            //Response response = new MangaInsertValidator().Validate(manga).ConvertToResponse();
            //if (!response.HasSuccess)
            //    return response;
            manga.EnableEntity();
            await _unitOfWork.MangaRepository.Insert(manga);
            return await _unitOfWork.Commit();
        }

        public async Task<Response> Update(Manga manga)
        { 
            await _unitOfWork.MangaRepository.Update(manga);
            return await _unitOfWork.Commit();
        }

        public async Task<Response> Delete(int id)
        {
            await _unitOfWork.MangaRepository.Delete(id);
            return await _unitOfWork.Commit();
        }

        public async Task<SingleResponse<Manga>> Get(int id)
        {
            return await _unitOfWork.MangaRepository.Get(id);
        }

        public async Task<DataResponse<Manga>> Get(int skip, int take)
        {
            //politica de cache!
            return await _unitOfWork.MangaRepository.Get(skip,take);
        }

        public async Task<DataResponse<Manga>> Get(string name)
        {
            return await _unitOfWork.MangaRepository.Get(name);
        }

        public async Task<Response> InsertCategory(Category id)
        {
            await _unitOfWork.MangaRepository.InsertCategory(id);
            return await _unitOfWork.Commit();
        }
        public async Task<int> GetLastIndexCategory()
        {
            return await _unitOfWork.MangaRepository.GetLastIndexCategory();
        }

        public async Task<int> GetLastIndex()
        {
            return await _unitOfWork.MangaRepository.GetLastIndex();
        }

        public async Task<SingleResponse<Manga>> GetComplete(int ID)
        {
            return await _unitOfWork.MangaRepository.GetComplete(ID);
        }
        public async Task<DataResponse<MangaCatalog>> GetByFavorites(int skip, int take)
        {
            return await _unitOfWork.MangaRepository.GetByFavorites(skip, take);
        }

        public async Task<DataResponse<MangaCatalog>> GetByUserCount(int skip, int take)
        {
            return await _unitOfWork.MangaRepository.GetByUserCount(skip, take);
        }

        public async Task<DataResponse<MangaCatalog>> GetByRating(int skip, int take)
        {
            return await _unitOfWork.MangaRepository.GetByRating(skip, take);
        }

        public async Task<DataResponse<Manga>> GetByCategory(int ID)
        {
            return await _unitOfWork.MangaRepository.GetByCategory(ID);
        }

        public async Task<DataResponse<MangaCatalog>> GetByPopularity(int skip, int take)
        {
            return await _unitOfWork.MangaRepository.GetByPopularity(skip, take);
        }
    }
}
