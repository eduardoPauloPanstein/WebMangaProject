using DataAccessLayer.Interfaces.IUserItem;
using Entities.MangaS;
using Entities.UserS;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Responses;

namespace DataAccessLayer.Implementations.UserItemDAL
{
    public class UserMangaItemDAL : IUserMangaItemDAL
    {
        private readonly MangaProjectDbContext _db;
        public UserMangaItemDAL(MangaProjectDbContext db)
        {
            _db = db;
        }
        public async Task<Response> Delete(int id)
        {
            UserMangaItem? MangaDB = await _db.UserManga.FindAsync(id);
            if (MangaDB == null)
                return ResponseFactory.CreateInstance().CreateFailedResponseNotFoundId();
            try
            {
                _db.UserManga.Remove(MangaDB);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<SingleResponse<UserMangaItem>> Get(int id)
        {
            try
            {
                UserMangaItem? Select = _db.UserManga.FirstOrDefault(m => m.Id == id);
                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse<UserMangaItem>(Select);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponseNotFoundItem<UserMangaItem>(ex);
            }
        }

        public async Task<DataResponse<UserMangaItem>> Get(int skip, int take)
        {
            try
            {
                List<UserMangaItem> mangas = await _db.UserManga
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(mangas);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<UserMangaItem>(ex);
            }
        }

        public async Task<DataResponse<UserMangaItem>> GetByUser(int userid)
        {
            try
            {
                List<UserMangaItem> user = await _db.UserManga.Where(u => u.UserId == userid).ToListAsync();

                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(user);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<UserMangaItem>(ex);
            }
        }

        public async Task<DataResponse<Manga>> GetUserFavorites(int userid)
        {
            List<Manga> mangas = new();
            try
            {
                List<UserMangaItem> user = await _db.UserManga.Where(u => u.UserId == userid && u.Favorite == true).ToListAsync();

                foreach (UserMangaItem item in user)
                {
                    mangas.Add(_db.Mangas.FirstOrDefault(m => m.Id == item.MangaId));
                }


                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(mangas);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Manga>(ex);
            }
        }

        public async Task<DataResponse<Manga>> GetUserList(int userid)
        {
            List<Manga> mangas = new();
            try
            {
                List<UserMangaItem> user = await _db.UserManga.Where(u => u.UserId == userid).ToListAsync();

                foreach (UserMangaItem item in user)
                {
                    mangas.Add(_db.Mangas.FirstOrDefault(m => m.Id == item.MangaId));
                }
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(mangas);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Manga>(ex);
            }
        }

        public async Task<DataResponse<Manga>> GetUserRecommendations(int userid)
        {
            List<int> IdsUsuarios = new();
            List<UserMangaItem> Fav = new();
            List<Manga> Mangas = new();
            try
            {
                List<UserMangaItem> userFav = await _db.UserManga.Where(u => u.UserId == userid && u.Favorite == true).ToListAsync();
                foreach (UserMangaItem item in userFav)
                {
                    UserMangaItem user = _db.UserManga.FirstOrDefault(m => m.MangaId == item.MangaId && m.UserId != userid);
                    if (user == null)
                    {

                    }
                    else
                    {
                        if (IdsUsuarios.Contains(user.UserId))
                        {

                        }
                        else
                        {
                            IdsUsuarios.Add(user.UserId);
                        }
                    }
                }

                foreach (int item in IdsUsuarios)
                {
                    List<UserMangaItem> user = _db.UserManga.Where(u => u.UserId == item).ToList();
                    foreach (UserMangaItem i in user)
                    {
                        Fav.Add(i);
                    }
                }

                foreach (UserMangaItem item in Fav)
                {
                    Mangas.Add(_db.Mangas.FirstOrDefault(m => m.Id == item.MangaId));
                }

                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(Mangas);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Manga>(ex);
            }
        }
        public async Task<Response> Insert(UserMangaItem Item, int score)
        {
            UserMangaItem? Response = _db.UserManga.FirstOrDefault(m => m.UserId == Item.UserId && m.MangaId == Item.MangaId);
            if (Response != null)
            {
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }

            RatingFrequencies selec = _db.MangaRating.Find(Item.MangaId);
            switch (score)
            {
                case 1:
                    selec._1++;
                    break;
                case 2:
                    selec._2++;
                    break;
                case 3:
                    selec._3++;
                    break;
                case 4:
                    selec._4++;
                    break;
                case 5:
                    selec._5++;
                    break;
            }
            _db.MangaRating.Update(selec);
            try
            {
                _db.UserManga.Add(Item);
                User? user = await _db.Users.FindAsync(Item.UserId);
                user.FavoritesCount += 1;

                _db.Users.Update(user);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }

        }
        public async Task<Response> Update(UserMangaItem Item)
        {
            try
            {
                UserMangaItem? MangaDB = await _db.UserManga.FindAsync(Item.Id);
                if (MangaDB == null)
                    return ResponseFactory.CreateInstance().CreateFailedResponseNotFoundId();
                _db.UserManga.Update(Item);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

    }

}


