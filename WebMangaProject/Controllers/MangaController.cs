using AutoMapper;
using BusinessLogicalLayer.Implementations.UserComentaryService;
using BusinessLogicalLayer.Interfaces.IUserItemService;
using Entities.MangaS;
using Entities.UserS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcPresentationLayer.Apis.MangaProjectApi;
using MvcPresentationLayer.Apis.MangaProjectApi.ItemComentary.MangaComentary;
using MvcPresentationLayer.Apis.MangaProjectApi.Mangas;
using MvcPresentationLayer.Models.MangaModels;
using MvcPresentationLayer.Utilities;
using Shared.Models.Manga;
using Shared.Responses;


namespace MvcPresentationLayer.Controllers
{
    public class MangaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMangaProjectApiMangaService _mangaApiService;
        private readonly IMangaProjectApiUserService _userApiService;
        private readonly IUserMangaItemService _userMangaItem;
        private readonly IMangaProjectApiMangaComentary _mangaComentary;

        public MangaController(IMapper mapper, IMangaProjectApiMangaService mangaApiService, IMangaProjectApiUserService userService, IUserMangaItemService userMangaItem, IMangaProjectApiMangaComentary mangaComentary)
        {
            this._userMangaItem = userMangaItem;
            this._userApiService = userService;
            this._mapper = mapper;
            this._mangaApiService = mangaApiService;
            this._mangaComentary = mangaComentary;
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            DataResponse<MangaCatalog> responseFavorites = await _mangaApiService.GetByFavorites(0, 7);
            DataResponse<MangaCatalog> responseCount = await _mangaApiService.GetByUserCount(0, 7);
            DataResponse<MangaCatalog> responserating = await _mangaApiService.GetByRating(0, 7);

            if (!responseFavorites.HasSuccess || !responseCount.HasSuccess || !responserating.HasSuccess)
            {
                return BadRequest(responseFavorites);
            }

            List<MangaShortViewModel> favo =
                _mapper.Map<List<MangaShortViewModel>>(responseFavorites.Data);

            List<MangaShortViewModel> Count =
                _mapper.Map<List<MangaShortViewModel>>(responseCount.Data);

            List<MangaShortViewModel> rating = _mapper.Map<List<MangaShortViewModel>>(responserating.Data);

            MangasForHomeViewModel MangasForHomeViewModel = new()
            {
                MangasFavorites = favo,
                MangasByCount = Count,
                MangasByRating = rating
            };
            return View(MangasForHomeViewModel);
        }

        #region All
        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> All(string by)
        {
            DataResponse<MangaCatalog> response;

            switch (by)
            {
                case "ByFavorites":
                    response = await _mangaApiService.GetByFavorites(0, 99);
                    break;
                case "ByRating":
                    response = await _mangaApiService.GetByRating(0, 99);
                    break;
                case "ByUserCount":
                    response = await _mangaApiService.GetByUserCount(0, 99);
                    break;
                case "ByPopularity":
                    response = await _mangaApiService.GetByPopularity(0, 99);
                    break;
                default:
                    response = new("Whereby??", false, null, null);
                    break;
            }


            if (!response.HasSuccess)
            {
                return BadRequest(response.Message);
            }

            List<MangaShortViewModel> mangasView =
                _mapper.Map<List<MangaShortViewModel>>(response.Data);

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

        [HttpGet]
        public async Task<IActionResult> MangaOnPage(int id)
        {
            SingleResponse<Manga> responseManga = await _mangaApiService.GetComplete(id);
            if (responseManga.NotFound)
            {
                return NotFound();
            }
            MangaOnPageViewModel manga = _mapper.Map<MangaOnPageViewModel>(responseManga.Item);


            SingleResponse<User> responseUser = new();
            if (User.Identity.IsAuthenticated)
            {
                responseUser = await _userApiService.Get(UserService.GetId(HttpContext), UserService.GetToken(HttpContext));
            }
            UserFavoriteMangaViewModel userMangaItem = new();
            bool hasItem = false;

            if (responseUser.HasSuccess && responseUser.Item.MangaList != null)
            {
                foreach (var item in responseUser.Item.MangaList)
                {
                    if (item.MangaId == id)
                    {
                        userMangaItem = _mapper.Map<UserFavoriteMangaViewModel>(item);
                        hasItem = true;
                    }
                }
            }

            DataResponse<Manga> responseSugg = new();
            if (User.Identity.IsAuthenticated)
            {
                responseSugg = await _userMangaItem.GetUserRecommendations(responseUser.Item.Id);
            }
            List<MangaShortViewModel> mangaSugg = _mapper.Map<List<MangaShortViewModel>>(responseSugg.Data);

            DataResponse<MangaComentary> responseComentary = await _mangaComentary.GetByManga(manga.Id);

            List<MangaComentaryViewModel> comments = _mapper.Map<List<MangaComentaryViewModel>>(responseComentary.Data);

            if (!hasItem)
            {
                userMangaItem.StartDate = DateTime.Now.Date;
                userMangaItem.FinishDate = DateTime.Now.Date;
            }

            MangaItemModalViewModel mangaItemModalViewModel = new()
            {
                UserMangaItem = userMangaItem,
                Manga = manga,
                Recommendations = mangaSugg,
                Comments = comments
            };

            return View(mangaItemModalViewModel);
        }


        public async Task<IActionResult> GetSuggestionList(string title)
        {
            DataResponse<Manga> response = await _mangaApiService.Get(title);
            return Json(new { resultado = response.Data });
        }
    }
}
