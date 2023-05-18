using BusinessLogicalLayer.Interfaces.IUserComentaryService;
using DataAccessLayer.Interfaces.IUserComentary;
using DataAccessLayer.UnitOfWork;
using Entities.MangaS;
using Shared.Responses;

namespace BusinessLogicalLayer.Implementations.UserComentaryService
{
    public class MangaComentaryService : IMangaComentary
    {
        private readonly IUnitOfWork _unitOfWork;

        public MangaComentaryService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<Response> Delete(int id)
        {
            await _unitOfWork.MangaComentaryRepository.Delete(id);
            return await _unitOfWork.Commit();
        }

        public async Task<SingleResponse<MangaComentary>> Get(int id)
        {
            return await _unitOfWork.MangaComentaryRepository.Get(id);
        }

        public async Task<DataResponse<MangaComentary>> Get(int skip, int take)
        {
            return await _unitOfWork.MangaComentaryRepository.Get(skip,take);
        }

        public async Task<DataResponse<MangaComentary>> GetByManga(int MangaID)
        {
            return await _unitOfWork.MangaComentaryRepository.GetByManga(MangaID);
        }

        public async Task<DataResponse<MangaComentary>> GetByUser(int userid)
        {
            return await _unitOfWork.MangaComentaryRepository.GetByUser(userid);
        }

        public async Task<Response> Insert(MangaComentary mangaComentary)
        {
            await _unitOfWork.MangaComentaryRepository.Insert(mangaComentary);
            return await _unitOfWork.Commit();
        }

        public async Task<Response> Update(MangaComentary mangaComentary)
        {
            await _unitOfWork.MangaComentaryRepository.Update(mangaComentary);
            return await _unitOfWork.Commit();
        }
    }
}
