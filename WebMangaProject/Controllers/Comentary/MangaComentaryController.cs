using AutoMapper;
using Entities.MangaS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcPresentationLayer.Apis.MangaProjectApi.ItemComentary.MangaComentary;
using MvcPresentationLayer.Models.MangaModels;
using MvcPresentationLayer.Utilities;
using Shared.Responses;

namespace MvcPresentationLayer.Controllers.Comentary
{
    public class MangaComentaryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMangaProjectApiMangaComentary _mangaComentary;
        public MangaComentaryController(IMapper mapper, IMangaProjectApiMangaComentary mangaapiComentary)
        {
            this._mangaComentary = mangaapiComentary;
            this._mapper = mapper;
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> Insert(MangaItemModalViewModel fav)
        {
            fav.MangaComentary.MangaId = fav.Manga.Id;
            MangaComentary item = this._mapper.Map<MangaComentary>(fav.MangaComentary);

            item.UserId = UserService.GetId(HttpContext);
            item.DataComentary = DateTime.Now;

            Response Response = await _mangaComentary.Insert(item, null);
            if (!Response.HasSuccess)
            {
                return BadRequest(Response);
            }
            return RedirectToAction("MangaOnPage", "Manga", new { id = fav.Manga.Id });
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> Update(MangaComentary fav)
        {
            Response Response = await _mangaComentary.Update(fav,null);
            if (!Response.HasSuccess)
            {
                return BadRequest(Response);
            }
            return RedirectToAction("MangaOnPage", "Manga", new { id = fav.Id });
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> Get(int id)
        {
            DataResponse<MangaComentary> Response = await _mangaComentary.Get(null,id);
            if (!Response.HasSuccess)
            {
                return BadRequest(Response);
            }
            return RedirectToAction("MangaOnPage", "Manga", new { id = id });
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> Get(int skp,int take)
        {
            DataResponse<MangaComentary> Response = await _mangaComentary.Get(null,skp,take);
            if (!Response.HasSuccess)
            {
                return BadRequest(Response);
            }
            return RedirectToAction("Home", "Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Response Response = await _mangaComentary.Delete(id,null);
            if (!Response.HasSuccess)
            {
                return BadRequest(Response);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> GetByUser(int id)
        {
            Response Response = await _mangaComentary.GetByUser(id);
            if (!Response.HasSuccess)
            {
                return BadRequest(Response);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
