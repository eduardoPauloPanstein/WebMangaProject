using DataAccessLayer.Interfaces.IUserComentary;
using Entities.AnimeS;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Responses;

namespace DataAccessLayer.Implementations.UserComentaryDAL
{
    public class AnimeComentaryDAL : IAnimeComentaryDAL
    {
        private readonly MangaProjectDbContext _db;
        public AnimeComentaryDAL(MangaProjectDbContext db)
        {
            _db = db;
        }
        public async Task<Response> Delete(int id)
        {
            try
            {
                AnimeComentary? MangaDB = await _db.AnimeComentaries.FindAsync(id);
                if (MangaDB == null)
                    return ResponseFactory.CreateInstance().CreateFailedResponseNotFoundId();
                _db.AnimeComentaries.Remove(MangaDB);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<SingleResponse<AnimeComentary>> Get(int id)
        {
            try
            {
                AnimeComentary? Select = _db.AnimeComentaries.FirstOrDefault(m => m.Id == id);
                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse<AnimeComentary>(Select);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponseNotFoundItem<AnimeComentary>(ex);
            }
        }

        public async Task<DataResponse<AnimeComentary>> Get(int skip, int take)
        {
            try
            {
                List<AnimeComentary> mangas = await _db.AnimeComentaries
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(mangas);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<AnimeComentary>(ex);
            }
        }

        public async Task<DataResponse<AnimeComentary>> GetByAnime(int animeID)
        {
            try
            {
                List<AnimeComentary> user = await _db.AnimeComentaries.Where(u => u.AnimeId == animeID).ToListAsync();

                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(user);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<AnimeComentary>(ex);
            }
        }

        public async Task<DataResponse<AnimeComentary>> GetByUser(int userid)
        {
            try
            {
                List<AnimeComentary> user = await _db.AnimeComentaries.Where(u => u.UserId == userid).ToListAsync();

                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(user);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<AnimeComentary>(ex);
            }
        }

        public async Task<Response> Insert(AnimeComentary Item)
        {
            try
            {
                _db.AnimeComentaries.Add(Item);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<Response> Update(AnimeComentary Item)
        {
            try
            {
                AnimeComentary? MangaDB = await _db.AnimeComentaries.FindAsync(Item.Id);
                if (MangaDB == null)
                    return ResponseFactory.CreateInstance().CreateFailedResponseNotFoundId();
                _db.AnimeComentaries.Update(Item);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }
    }
}
