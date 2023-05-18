using DataAccessLayer.Interfaces.IUserComentary;
using Entities.AnimeS;
using Entities.MangaS;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Responses;

namespace DataAccessLayer.Implementations.UserComentaryDAL
{
    public class MangaComentaryDAL : IMangaComentaryDAL
    {
        private readonly MangaProjectDbContext _db;
        public MangaComentaryDAL(MangaProjectDbContext db)
        {
            _db = db;
        }
        public async Task<Response> Delete(int id)
        {
            try
            {
                MangaComentary? MangaDB = await _db.MangaComentaries.FindAsync(id);
                if (MangaDB == null)
                    return ResponseFactory.CreateInstance().CreateFailedResponseNotFoundId();
                _db.MangaComentaries.Remove(MangaDB);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<SingleResponse<MangaComentary>> Get(int id)
        {
            try
            {
                MangaComentary? Select = _db.MangaComentaries.FirstOrDefault(m => m.Id == id);
                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse<MangaComentary>(Select);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponseNotFoundItem<MangaComentary>(ex);
            }
        }

        public async Task<DataResponse<MangaComentary>> Get(int skip, int take)
        {
            try
            {
                List<MangaComentary> mangas = await _db.MangaComentaries
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(mangas);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<MangaComentary>(ex);
            }
        }

        public async Task<DataResponse<MangaComentary>> GetByManga(int MangaID)
        {
            try
            {
                List<MangaComentary> user = await _db.MangaComentaries.Where(u => u.MangaId == MangaID).Include(u => u.User).ToListAsync();

                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(user);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<MangaComentary>(ex);
            }
        }

        public async Task<DataResponse<MangaComentary>> GetByUser(int userid)
        {
            try
            {
                List<MangaComentary> user = await _db.MangaComentaries.Where(u => u.UserId == userid).ToListAsync();

                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(user);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<MangaComentary>(ex);
            }
        }

        public async Task<Response> Insert(MangaComentary Item)
        {
            try
            {
                _db.MangaComentaries.Add(Item);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<Response> Update(MangaComentary Item)
        {
            try
            {
                MangaComentary? MangaDB = await _db.MangaComentaries.FindAsync(Item.Id);
                if (MangaDB == null)
                    return ResponseFactory.CreateInstance().CreateFailedResponseNotFoundId();
                _db.MangaComentaries.Update(Item);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }
    }
}
