using BusinessLogicalLayer.Interfaces.IUserComentaryService;
using DataAccessLayer.Interfaces.IUserComentary;
using DataAccessLayer.UnitOfWork;
using Entities.AnimeS;
using Shared.Responses;

namespace BusinessLogicalLayer.Implementations.UserComentaryService
{
    public class AnimeComentaryService : IAnimeComentary
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnimeComentaryService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<Response> Delete(int id)
        {
            await _unitOfWork.AnimeComentaryRepository.Delete(id);
            return await _unitOfWork.Commit();
        }

        public async Task<SingleResponse<AnimeComentary>> Get(int id)
        {
            return await _unitOfWork.AnimeComentaryRepository.Get(id);
        }

        public async Task<DataResponse<AnimeComentary>> Get(int skip, int take)
        {
            return await _unitOfWork.AnimeComentaryRepository.Get(skip,take);
        }

        public async Task<DataResponse<AnimeComentary>> GetByAnime(int animeID)
        {
            return await _unitOfWork.AnimeComentaryRepository.GetByAnime(animeID);
        }

        public async Task<DataResponse<AnimeComentary>> GetByUser(int userid)
        {
            return await _unitOfWork.AnimeComentaryRepository.GetByUser(userid);
        }

        public async Task<Response> Insert(AnimeComentary animeComentary)
        {
            await _unitOfWork.AnimeComentaryRepository.Insert(animeComentary);
            return await _unitOfWork.Commit();
        }

        public async Task<Response> Update(AnimeComentary animeComentary)
        {
            await _unitOfWork.AnimeComentaryRepository.Update(animeComentary);
            return await _unitOfWork.Commit();
        }
    }
}
