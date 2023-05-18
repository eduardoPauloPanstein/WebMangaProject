using AutoMapper;
using BusinessLogicalLayer.Interfaces.IUserItemService;
using Entities.AnimeS;
using Entities.MangaS;
using Entities.UserS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcPresentationLayer.Apis.MangaProjectApi;
using MvcPresentationLayer.Apis.MangaProjectApi.Animes;
using MvcPresentationLayer.Apis.MangaProjectApi.ItemComentary.AnimeComentary;
using MvcPresentationLayer.Models.AnimeModel;
using MvcPresentationLayer.Models.MangaModels;
using MvcPresentationLayer.Utilities;
using Shared.Models.Anime;
using Shared.Responses;

namespace MvcPresentationLayer.Controllers
{
    public class AnimeController : Controller
    {
        private readonly IMangaProjectApiAnimeService _animeApiService;
        private readonly IMapper _mapper;
        private readonly IMangaProjectApiUserService _userApiService;
        private readonly IUserAnimeItemService _userAnimeItem;
        private readonly IMangaProjectApiAnimeComentary _animeComentary;
        private readonly ICacheService _cacheService;

        public AnimeController(IMapper mapper, IMangaProjectApiUserService userApiService, IMangaProjectApiAnimeService animeApiService, IUserAnimeItemService userAnimeItem, IMangaProjectApiAnimeComentary animeComentary, ICacheService cacheService)
        {
            this._animeApiService = animeApiService;
            this._userApiService = userApiService;
            this._mapper = mapper;
            this._userAnimeItem = userAnimeItem;
            this._animeComentary = animeComentary;

            this._cacheService = cacheService;
        }

        [HttpGet]
        public async Task<IActionResult> AnimeOnPage(int id)
        {
            SingleResponse<Anime> responseAnime = await _animeApiService.GetComplete(id);
            if (responseAnime.NotFound)
            {
                return NotFound();
            }
            AnimeOnpageViewModel anime = _mapper.Map<AnimeOnpageViewModel>(responseAnime.Item);

            SingleResponse<User> responseUser = new();
            if (User.Identity.IsAuthenticated)
            {
                responseUser = await _userApiService.Get(UserService.GetId(HttpContext), UserService.GetToken(HttpContext));
            }

            UserFavoriteAnimeViewModel userAnimeItem = new();
            bool hasItem = false;

            if (responseUser.HasSuccess && responseUser.Item.AnimeList != null)
            {
                foreach (var item in responseUser.Item.AnimeList)
                {
                    if (item.AnimeId == id)
                    {
                        userAnimeItem = _mapper.Map<UserFavoriteAnimeViewModel>(item);
                        hasItem = true;
                    }
                }
            }

            DataResponse<Anime> responseSugg = new();
            if (User.Identity.IsAuthenticated)
            {
                responseSugg = await _userAnimeItem.GetUserRecommendations(responseUser.Item.Id);
            }
            List<AnimeShortViewModel> animeSugg = _mapper.Map<List<AnimeShortViewModel>>(responseSugg.Data);

            DataResponse<AnimeComentary> responseComentary = await _animeComentary.GetByAnime(anime.Id);

            List<AnimeComentaryViewModel> comments = _mapper.Map<List<AnimeComentaryViewModel>>(responseComentary.Data);

            if (!hasItem)
            {
                userAnimeItem.StartDate = DateTime.Now.Date;
                userAnimeItem.FinishDate = DateTime.Now.Date;
            }

            AnimeItemModalViewModel animeItemModalViewModel = new()
            {
                UserAnimeItem = userAnimeItem,
                Anime = anime,
                Recommendations = animeSugg,
                Comments = comments
            };
            return View(animeItemModalViewModel);
        }

        #region All
        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> All(string by)
        {
            DataResponse<AnimeCatalog> response;

            switch (by)
            {
                case "ByFavorites":
                    response = await _animeApiService.GetByFavorites(0, 99);
                    break;
                case "ByRating":
                    response = await _animeApiService.GetByRating(0, 99);
                    break;
                case "ByUserCount":
                    response = await _animeApiService.GetByUserCount(0, 99);
                    break;
                case "ByPopularity":
                    response = await _animeApiService.GetByPopularity(0, 99);
                    break;
                default:
                    response = new("Whereby??", false, null, null);
                    break;
            }


            if (!response.HasSuccess)
            {
                return BadRequest(response.Message);
            }

            List<AnimeShortViewModel> mangasView =
                _mapper.Map<List<AnimeShortViewModel>>(response.Data);

            return View(mangasView);
        }


        [HttpGet, AllowAnonymous]
        public IActionResult AllByFavorites() => RedirectToAction("All", new { by = "ByFavorites" });
        [HttpGet, AllowAnonymous]
        public IActionResult AllByPopularity() => RedirectToAction("All", new { by = "ByPopularity" });

        [HttpGet, AllowAnonymous]
        public IActionResult AllByRating() => RedirectToAction("All", new { by = "ByRating" });

        [HttpGet, AllowAnonymous]
        public IActionResult AllByUserCount() => RedirectToAction("All", new { by = "ByUserCount" });
        #endregion

        public async Task<IActionResult> Index()
        {
            DataResponse<AnimeCatalog> responseAnimesFavorites = await _cacheService.GetTop7AnimesCatalogByFavorites();
            DataResponse<AnimeCatalog> responseAnimesByCount = await _cacheService.GetTop7AnimesCatalogByUserCount();
            DataResponse<AnimeCatalog> responseAnimesByRating = await _cacheService.GetTop7AnimesCatalogByRating();

            if (!responseAnimesFavorites.HasSuccess || !responseAnimesByCount.HasSuccess || !responseAnimesByRating.HasSuccess)
            {
                return BadRequest(responseAnimesFavorites);
            }

            List<AnimeShortViewModel> animesFavorites =
                _mapper.Map<List<AnimeShortViewModel>>(responseAnimesFavorites.Data);

            List<AnimeShortViewModel> animesByCount =
                _mapper.Map<List<AnimeShortViewModel>>(responseAnimesByCount.Data);

            List<AnimeShortViewModel> animeesbyrating = _mapper.Map<List<AnimeShortViewModel>>(responseAnimesByRating.Data);

            AnimesForHomeViewModel animesForHomeViewModel = new()
            {
                AnimesFavorites = animesFavorites,
                AnimesByCount = animesByCount,
                AnimesByRating = animeesbyrating
            };

            return View(animesForHomeViewModel);
        }


    }
}
