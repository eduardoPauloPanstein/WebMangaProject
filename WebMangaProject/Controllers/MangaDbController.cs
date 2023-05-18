using AutoMapper;
using BusinessLogicalLayer.ApiConsumer.AnimeApi;
using BusinessLogicalLayer.ApiConsumer.CategoryApi;
using BusinessLogicalLayer.ApiConsumer.MangaApi;
using BusinessLogicalLayer.Interfaces.IMangaInterfaces;
using Entities.MangaS;
using Microsoft.AspNetCore.Mvc;
using MvcPresentationLayer.Apis.MangaProjectApi.Mangas;
using MvcPresentationLayer.Models.MangaModels;
using Shared.Responses;

namespace MvcPresentationLayer.Controllers
{
    public class MangaDbController : Controller
    {
        private readonly IMangaProjectApiMangaService _mangaApiService;
        private readonly IMapper _mapper;
        private readonly IApiConnect _apiService;
        private readonly ICategoryApiConnect _CateApi;
        private readonly IAnimeApiConnect _animeApi;
        public MangaDbController(IMangaProjectApiMangaService svc, IMapper mapper, IApiConnect connect, ICategoryApiConnect CateApi, IAnimeApiConnect animeApi)
        {
            this._animeApi = animeApi;
            this._CateApi = CateApi;
            this._mangaApiService = svc;
            this._mapper = mapper;
            this._apiService = connect;
        }
        public async Task<IActionResult> Index()
        {
            DataResponse<Manga> responseMangas = await _mangaApiService.Get(null, 01, 99);

            if (!responseMangas.HasSuccess)
            {
                ViewBag.Errors = responseMangas.Message;
                return View();
            }

            List<MangaShortDbViewModel> mangas =
                _mapper.Map<List<MangaShortDbViewModel>>(responseMangas.Data);

            return View(mangas);
        }
        public async Task<IActionResult> ConsumirApi()
        {
            await _CateApi.CovertiCatego();
            await _animeApi.ConsumeAnime();
            await _apiService.Consume();
            
            return RedirectToAction("Index");
        }

        // GET: MangaDbController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MangaDbController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MangaDbController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MangaDbController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MangaDbController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MangaDbController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MangaDbController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
    }
}
