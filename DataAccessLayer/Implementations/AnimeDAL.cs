using DataAccessLayer.Interfaces.IAnimeInterfaces;
using Entities;
using Entities.AnimeS;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Models.Anime;
using Shared.Responses;


namespace DataAccessLayer.Implementations
{
    public class AnimeDAL : IAnimeDAL
    {
        private readonly MangaProjectDbContext _db;
        public AnimeDAL(MangaProjectDbContext db)
        {
            this._db = db;
        }
        public async Task<Response> Insert(Anime Anime)
        {
            List<Category> Cate = new();
            try
            {
                foreach (var item in Anime.Categories)
                {
                    Cate.Add(await _db.Categories.FindAsync(item.ID));
                }
                Anime.Categories = Cate;
                _db.Animes.Add(Anime);
                await _db.SaveChangesAsync();
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<Response> Update(Anime Item)
        {
            Anime? AnimeDB = await _db.Animes.FindAsync(Item.Id);
            if (AnimeDB == null)
                return ResponseFactory.CreateInstance().CreateFailedResponseNotFoundId();
            try
            {
                _db.Animes.Update(Item);
                await _db.SaveChangesAsync();
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<SingleResponse<Anime>> Get(int id)
        {
            try
            {
                Anime? Select = await _db.Animes.FindAsync(id);
                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse<Anime>(Select);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponseNotFoundItem<Anime>(ex);
            }
        }

        public async Task<Response> Delete(int id)
        {
            Anime? AnimeDB = await _db.Animes.FindAsync(id);
            if (AnimeDB == null)
                return ResponseFactory.CreateInstance().CreateFailedResponseNotFoundId();
            try
            {
                _db.Animes.Remove(AnimeDB);
                await _db.SaveChangesAsync();
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<DataResponse<Anime>> Get(int skip, int take)
        {
            try
            {
                List<Anime> anime = await _db.Animes
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(anime);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Anime>(ex);
            }
        }

        public async Task<DataResponse<Anime>> Get(string name)
        {
            try
            {
                List<Anime> anime = await _db.Animes.Where(M => M.name.Contains(name)).ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData<Anime>(anime);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Anime>(ex);
            }
        }

        public async Task<SingleResponse<Anime>> GetComplete(int ID)
        {
            try
            {
                Anime? Select = _db.Animes.Include(c => c.Categories).Include(t => t.AnimeTitles).Include(c => c.Comentaries).ThenInclude(u => u.User).Include(r => r.AnimeRatingFrequencies).FirstOrDefault(m => m.Id == ID);
                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse<Anime>(Select);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Anime>(ex);
            }
        }

        public async Task<int> GetLastIndex()
        {
            try
            {
                Anime? a = _db.Animes.OrderBy(c => c.Id).LastOrDefault();
                return a.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> GetLastIndexCategory()
        {
            try
            {
                Category? a = _db.Categories.OrderBy(c => c.ID).LastOrDefault();
                return a.ID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<DataResponse<AnimeCatalog>> GetByFavorites(int skip, int take)
        {
            try
            {

                List<AnimeCatalog> animes = await _db.Animes
                    .OrderByDescending(m => m.favoritesCount)
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .Select(AnimeCatalog.Projection)
                    .ToListAsync();

                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(animes);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<AnimeCatalog>(ex);
            }
        }
        public async Task<DataResponse<AnimeCatalog>> GetByRating(int skip, int take)
        {
            try
            {
                List<AnimeCatalog> animes = await _db.Animes
                    .OrderBy(m => m.ratingRank)
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .Select(AnimeCatalog.Projection)
                    .ToListAsync();

                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(animes);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<AnimeCatalog>(ex);
            }
        }

        public async Task<DataResponse<AnimeCatalog>> GetByUserCount(int skip, int take)
        {
            try
            {
                List<AnimeCatalog> Animes = await _db.Animes
                    .AsNoTracking()
                    .OrderByDescending(m => m.userCount)
                    .Skip(skip)
                    .Take(take)
                    .Select(AnimeCatalog.Projection)
                    .ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(Animes);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<AnimeCatalog>(ex);
            }
        }

        public async Task<DataResponse<Anime>> GetByCategory(int ID)
        {
            try
            {
                Category? Select = _db.Categories.Include(c => c.AnimesID).FirstOrDefault(m => m.ID == ID);
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(Select.AnimesID.ToList());
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Anime>(ex);
            }
        }

        public async Task<DataResponse<AnimeCatalog>> GetByPopularity(int skip, int take)
        {
            try
            {
                List<AnimeCatalog> Animes = await _db.Animes
                    .OrderBy(m => m.popularityRank)
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .Select(AnimeCatalog.Projection)
                    .ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(Animes);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<AnimeCatalog>(ex);
            }
        }
    }
}
