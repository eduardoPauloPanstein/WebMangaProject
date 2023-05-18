using DataAccessLayer.Interfaces.IUserItem;
using Entities.AnimeS;
using Entities.UserS;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Responses;

namespace DataAccessLayer.Implementations.UserItemDAL
{
    public class UserAnimeItemDAL : IUserAnimeItemDAL
    {
        private readonly MangaProjectDbContext _db;
        public UserAnimeItemDAL(MangaProjectDbContext db)
        {
            _db = db;
        }
        public async Task<Response> Delete(int id)
        {

            try
            {
                UserAnimeItem? MangaDB = await _db.UserAnime.FindAsync(id);
                if (MangaDB == null)
                    return ResponseFactory.CreateInstance().CreateFailedResponseNotFoundId();
                _db.UserAnime.Remove(MangaDB);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<SingleResponse<UserAnimeItem>> Get(int id)
        {
            try
            {
                UserAnimeItem? Select = _db.UserAnime.FirstOrDefault(m => m.Id == id);
                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse<UserAnimeItem>(Select);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponseNotFoundItem<UserAnimeItem>(ex);
            }
        }

        public async Task<DataResponse<UserAnimeItem>> Get(int skip, int take)
        {
            try
            {
                List<UserAnimeItem> mangas = await _db.UserAnime
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(mangas);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<UserAnimeItem>(ex);
            }
        }

        public async Task<DataResponse<UserAnimeItem>> GetByUser(int userid)
        {
            try
            {
                List<UserAnimeItem> user = await _db.UserAnime.Where(u => u.UserId == userid).ToListAsync();

                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(user);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<UserAnimeItem>(ex);
            }
        }

        public async Task<DataResponse<Anime>> GetUserFavorites(int userid)
        {
            List<Anime> anime = new();
            try
            {
                List<UserAnimeItem> user = await _db.UserAnime.Where(u => u.UserId == userid && u.Favorite == true).ToListAsync();

                foreach (UserAnimeItem item in user)
                {
                    anime.Add(_db.Animes.FirstOrDefault(m => m.Id == item.AnimeId));
                }


                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(anime);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Anime>(ex);
            }
        }

        public async Task<DataResponse<Anime>> GetUserList(int userid)
        {
            List<Anime> anime = new();
            try
            {
                List<UserAnimeItem> user = await _db.UserAnime.Where(u => u.UserId == userid).ToListAsync();

                foreach (UserAnimeItem item in user)
                {
                    anime.Add(_db.Animes.FirstOrDefault(m => m.Id == item.AnimeId));
                }
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(anime);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Anime>(ex);
            }
        }

        public async Task<DataResponse<Anime>> GetUserRecommendations(int userid)
        {
            List<int> IdsUsuarios = new();
            List<UserAnimeItem> Fav = new();
            List<Anime> Anime = new();
            try
            {
                List<UserAnimeItem> userFav = await _db.UserAnime.Where(u => u.UserId == userid && u.Favorite == true).ToListAsync();
                foreach (UserAnimeItem item in userFav)
                {
                    UserAnimeItem user = _db.UserAnime.FirstOrDefault(m => m.AnimeId == item.AnimeId && m.UserId != userid);
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
                    List<UserAnimeItem> user = _db.UserAnime.Where(u => u.UserId == item).ToList();
                    foreach (UserAnimeItem i in user)
                    {
                        Fav.Add(i);
                    }
                }

                foreach (UserAnimeItem item in Fav)
                {
                    Anime.Add(_db.Animes.FirstOrDefault(m => m.Id == item.AnimeId));
                }

                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(Anime);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Anime>(ex);
            }
        }


        public async Task<Response> Insert(UserAnimeItem Item, int score)
        {
            UserAnimeItem? Response = _db.UserAnime.FirstOrDefault(m => m.UserId == Item.UserId && m.AnimeId == Item.AnimeId);
            if (Response != null)
            {
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }

            AnimeRatingFrequencies selec = _db.AnimeRating.Find(Item.AnimeId);
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
            _db.AnimeRating.Update(selec);
            try
            {
                _db.UserAnime.Add(Item);
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
        public async Task<Response> Update(UserAnimeItem Item)
        {
            try
            {
                UserAnimeItem? MangaDB = await _db.UserAnime.FindAsync(Item.Id);
                if (MangaDB == null)
                    return ResponseFactory.CreateInstance().CreateFailedResponseNotFoundId();
                _db.UserAnime.Update(Item);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

    }
}



