using AutoMapper;
using Entities.UserS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcPresentationLayer.Apis.MangaProjectApi.UserItem.UserMangaItem;
using MvcPresentationLayer.Models.MangaModels;
using MvcPresentationLayer.Utilities;
using Shared.Responses;
namespace MvcPresentationLayer.Controllers.UserItem
{
    public class UserItemMangaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMangaProjectApiMangaItem _MangaApiItem;
        public UserItemMangaController(IMapper mapper, IMangaProjectApiMangaItem Mangaitem)
        {
            this._MangaApiItem = Mangaitem;
            this._mapper = mapper;
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> Insert(MangaItemModalViewModel fav)
        {
            fav.UserMangaItem.MangaId = fav.Manga.Id;
            UserMangaItem item = this._mapper.Map<UserMangaItem>(fav.UserMangaItem);

            item.UserId = UserService.GetId(HttpContext);
            item.Id = 0;

            Response Response = await _MangaApiItem.Insert(item, null);
            if (!Response.HasSuccess)
            {
                return BadRequest(Response);
            }
            return RedirectToAction("MangaOnPage", "Manga", new { id = fav.Manga.Id });
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> Update(UserMangaItem fav)
        {
            Response Response = await _MangaApiItem.Update(fav, null);
            if (!Response.HasSuccess)
            {
                return BadRequest(Response);
            }
            return RedirectToAction("AnimeOnPage", "Anime", new { id = fav.Id });
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> Get(int id)
        {
            SingleResponse<UserMangaItem> Response = await _MangaApiItem.Get(id, null);
            if (!Response.HasSuccess)
            {
                return BadRequest(Response);
            }
            return RedirectToAction("AnimeOnPage", "Anime", new { id = id });
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> Get(int skp, int take)
        {
            DataResponse<UserMangaItem> Response = await _MangaApiItem.Get(skp, take,null);
            if (!Response.HasSuccess)
            {
                return BadRequest(Response);
            }
            return RedirectToAction("Home", "Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Response Response = await _MangaApiItem.Delete(id, null);
            if (!Response.HasSuccess)
            {
                return BadRequest(Response);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> GetByUser(int id)
        {
            Response Response = await _MangaApiItem.GetByUser(id,null);
            if (!Response.HasSuccess)
            {
                return BadRequest(Response);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
