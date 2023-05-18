using BusinessLogicalLayer.Interfaces.IAnimeInterfaces;
using Entities.AnimeS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimeController : ControllerBase
    {
        private readonly IAnimeService _animeService;

        public AnimeController(IAnimeService animeService)
        {
            this._animeService = animeService;
        }

        [HttpGet(template: "skip/{skip}/take/{take}"), AllowAnonymous]
        public async Task<IActionResult> GetAsync([FromRoute] int skip = 0, [FromRoute] int take = 25)
        {
            if (take >= 100)
            {
                return BadRequest("take < 100");
            }
            var responseUsers = await _animeService.Get(skip, take);
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers);
            }

            return Ok(responseUsers);
        }

        [HttpGet(template: "ByFavorites/skip/{skip}/take/{take}"), AllowAnonymous]
        public async Task<IActionResult> GeByFavoritestAsync([FromRoute] int skip = 0, [FromRoute] int take = 25)
        {
            if (take >= 100)
            {
                return BadRequest("take < 100");
            }
            var responseUsers = await _animeService.GetByFavorites(skip, take);
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers);
            }

            return Ok(responseUsers);
        }
        [HttpGet(template: "ByRating/skip/{skip}/take/{take}"), AllowAnonymous]
        public async Task<IActionResult> GetByRating([FromRoute] int skip = 0, [FromRoute] int take = 25)
        {
            if (take >= 100)
            {
                return BadRequest("take < 100");
            }
            var response = await _animeService.GetByRating(skip, take);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet(template: "ByUserCount/skip/{skip}/take/{take}"), AllowAnonymous]
        public async Task<IActionResult> GetByUserCountAsync([FromRoute] int skip = 0, [FromRoute] int take = 25)
        {
            if (take >= 100)
            {
                return BadRequest("take < 100");
            }
            var responseUsers = await _animeService.GetByUserCount(skip, take);
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers);
            }

            return Ok(responseUsers);
        }
        [HttpGet(template: "ByPopularity/skip/{skip}/take/{take}"), AllowAnonymous]
        public async Task<IActionResult> GetByPopularityAsync([FromRoute] int skip = 0, [FromRoute] int take = 25)
        {
            if (take >= 100)
            {
                return BadRequest("take < 100");
            }
            var responseUsers = await _animeService.GetByPopularity(skip, take);
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers);
            }
            return Ok(responseUsers);
        }

        [HttpGet("ByName/{title}"), AllowAnonymous]
        public async Task<IActionResult> GetByNameAsync([FromRoute] string title)
        {
            var response = await _animeService.Get(title);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("ById/{id}"), AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseUsers = await _animeService.Get(id);
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers);
            }

            return Ok(responseUsers);
        }
        [HttpGet("AnimeOnPage/{id}"), AllowAnonymous]
        public async Task<IActionResult> GetByOnPageAsync(int id)
        {
            var responseUsers = await _animeService.GetComplete(id);
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers);
            }

            return Ok(responseUsers);
        }


        [HttpPost, Authorize(Policy = "Admin")]
        public async Task<IActionResult> PostAsync([FromBody] string value)
        {
            //if (IsAuthenticated())

            var manga = JsonConvert.DeserializeObject<Anime>(value);

            var response = await _animeService.Insert(manga);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}"), Authorize(Policy = "Admin")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] string value)
        {
            var user = JsonConvert.DeserializeObject<Anime>(value);

            var response = await _animeService.Update(user);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}"), Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _animeService.Delete(id);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
