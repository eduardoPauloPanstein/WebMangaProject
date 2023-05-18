using AutoMapper;
using BusinessLogicalLayer.Interfaces.IUserComentaryService;
using Entities.AnimeS;
using Entities.MangaS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcPresentationLayer.Apis.MangaProjectApi.ItemComentary.AnimeComentary;
using MvcPresentationLayer.Models.AnimeModel;
using MvcPresentationLayer.Utilities;
using Shared.Responses;

namespace MvcPresentationLayer.Controllers.Comentary
{
    public class AnimeComentaryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMangaProjectApiAnimeComentary _AnimeComentary;
        public AnimeComentaryController(IMapper mapper, IMangaProjectApiAnimeComentary AnimeComentary)
        {
            this._AnimeComentary = AnimeComentary;
            this._mapper = mapper;
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> Insert(AnimeItemModalViewModel fav)
        {
            fav.AnimeComentary.AnimeId = fav.Anime.Id;
            AnimeComentary item = this._mapper.Map<AnimeComentary>(fav.AnimeComentary);

            item.UserId = UserService.GetId(HttpContext);
            item.DataComentary = DateTime.Now;

            Response Response = await _AnimeComentary.Insert(item, null);
            if (!Response.HasSuccess)
            {
                return BadRequest(Response);
            }
            return RedirectToAction("AnimeOnPage", "Anime", new { id = fav.Anime.Id });
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> Update(AnimeComentary fav)
        {
            Response Response = await _AnimeComentary.Update(fav,null);
            if (!Response.HasSuccess)
            {
                return BadRequest(Response);
            }
            return RedirectToAction("AnimeOnPage", "Anime", new { id = fav.Id });
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> Get(int id)
        {
            SingleResponse<AnimeComentary> Response = await _AnimeComentary.Get(id,null);
            if (!Response.HasSuccess)
            {
                return BadRequest(Response);
            }
            return RedirectToAction("AnimeOnPage", "Anime", new { id = id });
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> Get(int skp, int take)
        {
            DataResponse<AnimeComentary> Response = await _AnimeComentary.Get(null,skp, take);
            if (!Response.HasSuccess)
            {
                return BadRequest(Response);
            }
            return RedirectToAction("Home", "Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Response Response = await _AnimeComentary.Delete(id,null);
            if (!Response.HasSuccess)
            {
                return BadRequest(Response);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> GetByUser(int id)
        {
            Response Response = await _AnimeComentary.GetByUser(id);
            if (!Response.HasSuccess)
            {
                return BadRequest(Response);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
