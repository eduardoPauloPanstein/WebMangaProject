using BusinessLogicalLayer.Interfaces.IUserItemService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Entities.AnimeS;
using Entities.UserS;
namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MangaItemController : ControllerBase
    {
        private readonly IUserMangaItemService _MangaItem;

        public MangaItemController(IUserMangaItemService MangaItem)
        {
            this._MangaItem = MangaItem;
        }

        [HttpGet(template: "skip/{skip}/take/{take}"), AllowAnonymous]
        public async Task<IActionResult> GetAsync([FromRoute] int skip = 0, [FromRoute] int take = 25)
        {
            if (take >= 100)
            {
                return BadRequest("take < 100");
            }
            var responseUsers = await _MangaItem.Get(skip, take);
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers);
            }

            return Ok(responseUsers);
        }

        [HttpGet("ById/{id}"), AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseUsers = await _MangaItem.Get(id);
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers);
            }

            return Ok(responseUsers);
        }
        [HttpGet("ByUser/{id}"), AllowAnonymous]
        public async Task<IActionResult> GetByUserAsync(int id)
        {
            var responseUsers = await _MangaItem.GetByUser(id);
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers);
            }

            return Ok(responseUsers);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> PostAsync([FromBody] string value)
        {
            //if (IsAuthenticated())

            var manga = JsonConvert.DeserializeObject<UserMangaItem>(value);
            int score = ((int)manga.Score);
            var response = await _MangaItem.Insert(manga, score);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}"), AllowAnonymous]
        public async Task<IActionResult> PutAsync(int id, [FromBody] string value)
        {
            var user = JsonConvert.DeserializeObject<UserMangaItem>(value);

            var response = await _MangaItem.Update(user);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}"), AllowAnonymous]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _MangaItem.Delete(id);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
       
        [HttpGet("UserList/{id}"), AllowAnonymous]
        public async Task<IActionResult> GetByUserListAsync(int id)
        {
            var responseUsers = await _MangaItem.GetUserList(id);
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers);
            }

            return Ok(responseUsers);
        }
        [HttpGet("UserFavorites/{id}"), AllowAnonymous]
        public async Task<IActionResult> GetByUserFavoritesAsync(int id)
        {
            var responseUsers = await _MangaItem.GetUserFavorites(id);
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers);
            }

            return Ok(responseUsers);
        }
        [HttpGet("UserRecommendations/{id}"), AllowAnonymous]
        public async Task<IActionResult> GetByUserRecommendationsAsync(int id)
        {
            var responseUsers = await _MangaItem.GetUserRecommendations(id);
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers);
            }

            return Ok(responseUsers);
        }

    }
}
