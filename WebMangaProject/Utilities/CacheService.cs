using Microsoft.Extensions.Caching.Distributed;
using MvcPresentationLayer.Apis.MangaProjectApi.Animes;
using MvcPresentationLayer.Apis.MangaProjectApi.Mangas;
using Newtonsoft.Json;
using Shared;
using Shared.Models.Anime;
using Shared.Models.Manga;
using Shared.Responses;

namespace MvcPresentationLayer.Utilities
{
    public class CacheService : ICacheService
    {
        private readonly IMangaProjectApiAnimeService _animeApiService;
        private readonly IMangaProjectApiMangaService _mangaApiService;
        private readonly IDistributedCache _distributedCache;

        public CacheService(IMangaProjectApiAnimeService animeService, IMangaProjectApiMangaService mangaService, IDistributedCache cache)
        {
            _animeApiService = animeService;
            _mangaApiService = mangaService;
            _distributedCache = cache;
        }

        //animes
        public async Task<DataResponse<AnimeCatalog>> GetTop7AnimesCatalogByFavorites()
        {
            var json = await _distributedCache.GetStringAsync(LocationConstants.CacheKey.Anime.GetTop7AnimesCatalogByFavorites);
            if (json != null)
            {
                var animeCatalog = JsonConvert.DeserializeObject<List<AnimeCatalog>>(json);
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(animeCatalog);
            }
            else
            {
                DataResponse<AnimeCatalog> response = await _animeApiService.GetByFavorites(0, 7);
                if (response.HasSuccess)
                {
                    json = JsonConvert.SerializeObject(response.Data);
                    await _distributedCache.SetStringAsync(LocationConstants.CacheKey.Anime.GetTop7AnimesCatalogByFavorites, json);
                }
                return response;
            }
        }
        public async Task<DataResponse<AnimeCatalog>> GetTop7AnimesCatalogByUserCount()
        {
            var json = await _distributedCache.GetStringAsync(LocationConstants.CacheKey.Anime.GetTop7AnimesCatalogByUserCount);
            if (json != null)
            {
                var animeCatalog = JsonConvert.DeserializeObject<List<AnimeCatalog>>(json);
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(animeCatalog);
            }
            else
            {
                DataResponse<AnimeCatalog> response = await _animeApiService.GetByUserCount(0, 7);
                if (response.HasSuccess)
                {
                    json = JsonConvert.SerializeObject(response.Data);
                    await _distributedCache.SetStringAsync(LocationConstants.CacheKey.Anime.GetTop7AnimesCatalogByUserCount, json);
                }
                return response;
            }
        }
        public async Task<DataResponse<AnimeCatalog>> GetTop7AnimesCatalogByRating()
        {
            var json = await _distributedCache.GetStringAsync(LocationConstants.CacheKey.Anime.GetTop7AnimesCatalogByRating);
            if (json != null)
            {
                var animeCatalog = JsonConvert.DeserializeObject<List<AnimeCatalog>>(json);
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(animeCatalog);
            }
            else
            {
                DataResponse<AnimeCatalog> response = await _animeApiService.GetByRating(0, 7);
                if (response.HasSuccess)
                {
                    json = JsonConvert.SerializeObject(response.Data);
                    await _distributedCache.SetStringAsync(LocationConstants.CacheKey.Anime.GetTop7AnimesCatalogByRating, json);
                }
                return response;
            }
        }

        //mangas
        public async Task<DataResponse<MangaCatalog>> GetTop7MangasCatalogByFavorites()
        {
            var json = await _distributedCache.GetStringAsync(LocationConstants.CacheKey.Manga.GetTop7MangasCatalogByFavorites);
            if (json != null)
            {
                var mangasCatalog = JsonConvert.DeserializeObject<List<MangaCatalog>>(json);
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(mangasCatalog);
            }
            else
            {
                DataResponse<MangaCatalog> response = await _mangaApiService.GetByFavorites(0, 7);
                if (response.HasSuccess)
                {
                    json = JsonConvert.SerializeObject(response.Data);
                    await _distributedCache.SetStringAsync(LocationConstants.CacheKey.Manga.GetTop7MangasCatalogByFavorites, json);
                }
                return response;
            }
        }
        public async Task<DataResponse<MangaCatalog>> GetTop7MangasCatalogByUserCount()
        {
            var json = await _distributedCache.GetStringAsync(LocationConstants.CacheKey.Manga.GetTop7MangasCatalogByUserCount);
            if (json != null)
            {
                var mangasCatalog = JsonConvert.DeserializeObject<List<MangaCatalog>>(json);
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(mangasCatalog);
            }
            else
            {
                DataResponse<MangaCatalog> response = await _mangaApiService.GetByUserCount(0, 7);
                if (response.HasSuccess)
                {
                    json = JsonConvert.SerializeObject(response.Data);
                    await _distributedCache.SetStringAsync(LocationConstants.CacheKey.Manga.GetTop7MangasCatalogByUserCount, json);
                }
                return response;
            }
        }
        public async Task<DataResponse<MangaCatalog>> GetTop7MangasCatalogByRating()
        {
            var json = await _distributedCache.GetStringAsync(LocationConstants.CacheKey.Manga.GetTop7MangasCatalogByRating);
            if (json != null)
            {
                var mangasCatalog = JsonConvert.DeserializeObject<List<MangaCatalog>>(json);
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(mangasCatalog);
            }
            else
            {
                DataResponse<MangaCatalog> response = await _mangaApiService.GetByRating(0, 7);
                if (response.HasSuccess)
                {
                    json = JsonConvert.SerializeObject(response.Data);
                    await _distributedCache.SetStringAsync(LocationConstants.CacheKey.Manga.GetTop7MangasCatalogByRating, json);
                }
                return response;
            }
        }
    }

    public interface ICacheService
    {
        Task<DataResponse<AnimeCatalog>> GetTop7AnimesCatalogByFavorites();
        Task<DataResponse<AnimeCatalog>> GetTop7AnimesCatalogByUserCount();
        Task<DataResponse<AnimeCatalog>> GetTop7AnimesCatalogByRating();

        Task<DataResponse<MangaCatalog>> GetTop7MangasCatalogByFavorites();
        Task<DataResponse<MangaCatalog>> GetTop7MangasCatalogByUserCount();
        Task<DataResponse<MangaCatalog>> GetTop7MangasCatalogByRating();
    }
}
