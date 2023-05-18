using DataAccessLayer.Interfaces.IMangaInterfaces;
using Entities;
using Entities.MangaS;
using Entities.UserS;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Models.Manga;
using Shared.Responses;

namespace DataAccessLayer.Implementations
{
    public class MangaDAL : IMangaDAL
    {
        private readonly MangaProjectDbContext _db;
        public MangaDAL(MangaProjectDbContext db)
        {
            this._db = db;
        }

        public async Task<Response> Insert(Manga manga)
        {
            List<Category> Cate = new();
            try
            {
                foreach (var item in manga.Genres)
                {
                   Cate.Add(await _db.Categories.FindAsync(item.ID));
                }
                manga.Genres = Cate;
                _db.Mangas.Add(manga);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<Response> Update(Manga Item)
        {
            Manga? MangaDB = await _db.Mangas.FindAsync(Item.Id);
            if (MangaDB == null)
                return ResponseFactory.CreateInstance().CreateFailedResponseNotFoundId();
            try
            {
                _db.Mangas.Update(Item);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<Response> Delete(int id)
        {
            Manga? MangaDB = await _db.Mangas.FindAsync(id);
            if (MangaDB == null)
                return ResponseFactory.CreateInstance().CreateFailedResponseNotFoundId();
            try
            {
                _db.Mangas.Remove(MangaDB);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }
        public async Task<SingleResponse<Manga>> Get(int id)
        {
            try
            {
                Manga Select = _db.Mangas.AsNoTracking().FirstOrDefault(m => m.Id == id);
                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse<Manga>(Select);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponseNotFoundItem<Manga>(ex);
            }
        }

        public async Task<DataResponse<Manga>> Get(int skip, int take)
        {
            try
            {
                List<Manga> mangas = await _db.Mangas
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(mangas);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Manga>(ex);
            }
        }
        public async Task<DataResponse<MangaCatalog>> GetByUserCount(int skip, int take)
        {
            try
            {
                List<MangaCatalog> mangas = await _db.Mangas
                    .OrderByDescending(m => m.UserCount)
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .Select(MangaCatalog.Projection)
                    .ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(mangas);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<MangaCatalog>(ex);
            }
        }

        public async Task<DataResponse<MangaCatalog>> GetByFavorites(int skip, int take)
        {
            try
            {
                List<MangaCatalog> mangas = await _db.Mangas
                    .OrderByDescending(m => m.FavoritesCount)
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .Select(MangaCatalog.Projection)
                    .ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(mangas);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<MangaCatalog>(ex);
            }
        }

        public async Task<DataResponse<Manga>> Get(string name)
        {
            try
            {
                List<Manga> mangas = await _db.Mangas.AsNoTracking().Where(M => M.CanonicalTitle.Contains(name)).ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData<Manga>(mangas);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Manga>(ex);
            }
        }


        public async Task<Response> InsertCategory(Category id)
        {
            try
            {
                _db.Categories.Add(id);
                //await _db.SaveChangesAsync();
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
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

        public async Task<DataResponse<MangaCatalog>> GetByRating(int skip, int take)
        {
            try
            {
                List<MangaCatalog> mangas = await _db.Mangas
                    .OrderByDescending(m => m.RatingRank)
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .Select(MangaCatalog.Projection)
                    .ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(mangas);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<MangaCatalog>(ex);
            }
        }
        public async Task<int> GetLastIndex()
        {
            try
            {
                Manga? a = _db.Mangas.OrderBy(c => c.Id).LastOrDefault();
                return a.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<SingleResponse<Manga>> GetComplete(int ID)
        {
            try
            {
                Manga? Select = _db.Mangas.Include(c => c.Genres).Include(c=> c.Comentaries).ThenInclude(u => u.User).Include(t => t.Titles).Include(r => r.RatingFrequencies).FirstOrDefault(m => m.Id == ID);
                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse<Manga>(Select);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Manga>(ex);
            }
        }
        public async Task<DataResponse<Manga>> GetByCategory(int ID)
        {
            try
            {
                Category? Select = _db.Categories.Include(c => c.MangasID).FirstOrDefault(m => m.ID == ID);
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(Select.MangasID.ToList());
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Manga>(ex);
            }
        }
        public async Task<DataResponse<MangaCatalog>> GetByPopularity(int skip, int take)
        {
            try
            {
                List<MangaCatalog> mangas = await _db.Mangas
                    .OrderBy(m => m.PopularityRank)
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .Select(MangaCatalog.Projection)
                    .ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(mangas);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<MangaCatalog>(ex);
            }
        }
    }
}
