using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;
using System.Diagnostics;
using WebMangaProject.Models;
using MvcPresentationLayer.Models.AnimeModel;
using MvcPresentationLayer.Models.MangaModels;
using MvcPresentationLayer.Models.HomePage;
using Shared.Models.Manga;
using MvcPresentationLayer.Utilities;

namespace MvcPresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public HomeController(IMapper mapper, ICacheService cacheService)
        {
            this._mapper = mapper;
            this._cacheService = cacheService;
        }

        public async Task<IActionResult> Index()
        {
            var responseAnimesFavorites = await _cacheService.GetTop7AnimesCatalogByFavorites();
            var responseAnimesByCount = await _cacheService.GetTop7AnimesCatalogByUserCount();
            var responseAnimesByRating = await _cacheService.GetTop7AnimesCatalogByRating();

            if (!responseAnimesFavorites.HasSuccess || !responseAnimesByCount.HasSuccess || !responseAnimesByRating.HasSuccess)
            {
                return BadRequest(responseAnimesFavorites);
            }

            List<AnimeShortViewModel> animesFavorites =
                _mapper.Map<List<AnimeShortViewModel>>(responseAnimesFavorites.Data);

            List<AnimeShortViewModel> animesByCount =
                _mapper.Map<List<AnimeShortViewModel>>(responseAnimesByCount.Data);

            List<AnimeShortViewModel> animesByRating = _mapper.Map<List<AnimeShortViewModel>>(responseAnimesByRating.Data);

            DataResponse<MangaCatalog> responseMangaFavorites = await _cacheService.GetTop7MangasCatalogByFavorites();
            DataResponse<MangaCatalog> responseMangaCount = await _cacheService.GetTop7MangasCatalogByUserCount();
            DataResponse<MangaCatalog> responseMangaRating = await _cacheService.GetTop7MangasCatalogByRating();

            if (!responseMangaFavorites.HasSuccess || !responseMangaCount.HasSuccess || !responseMangaRating.HasSuccess)
            {
                return BadRequest(responseMangaFavorites);
            }

            List<MangaShortViewModel> MangaFavorite =
                _mapper.Map<List<MangaShortViewModel>>(responseMangaFavorites.Data);

            List<MangaShortViewModel> MangaCount =
                _mapper.Map<List<MangaShortViewModel>>(responseMangaCount.Data);

            List<MangaShortViewModel> MangaRating = _mapper.Map<List<MangaShortViewModel>>(responseMangaRating.Data);

            HomePageViewModel homePageViewModel = new()
            {
                MangasFavorites = MangaFavorite,
                MangasByCount = MangaCount,
                MangasByRating = MangaRating,
                AnimesFavorites = animesFavorites,
                AnimesByCount = animesByCount,
                AnimesByRating = animesByRating,
            };
            return View(homePageViewModel);
        }
        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}