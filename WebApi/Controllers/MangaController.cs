using BusinessLogicalLayer.Interfaces.IMangaInterfaces;
using Entities.MangaS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MangaController : ControllerBase
    {
        private readonly IMangaService _mangaService;

        public MangaController(IMangaService mangaService)
        {
            this._mangaService = mangaService;
        }

        [HttpGet(template: "skip/{skip}/take/{take}"), AllowAnonymous]
        public async Task<IActionResult> GetAsync([FromRoute] int skip = 0, [FromRoute] int take = 25)
        {
            if (take >= 100)
            {
                return BadRequest("take < 100");
            }
            var responseUsers = await _mangaService.Get(skip, take);
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
            var responseUsers = await _mangaService.GetByFavorites(skip, take);
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
            var responseUsers = await _mangaService.GetByRating(skip, take);
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers);
            }

            return Ok(responseUsers);
        }

        [HttpGet(template: "ByUserCount/skip/{skip}/take/{take}"), AllowAnonymous]
        public async Task<IActionResult> GetByUserCountAsync([FromRoute] int skip = 0, [FromRoute] int take = 25)
        {
            if (take >= 100)
            {
                return BadRequest("take < 100");
            }
            var responseUsers = await _mangaService.GetByUserCount(skip, take);
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers);
            }

            return Ok(responseUsers);
        }

        [HttpGet("ByName/{title}"), AllowAnonymous]
        public async Task<IActionResult> GetByNameAsync([FromRoute] string title)
        {
            var response = await _mangaService.Get(title);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("ById/{id}"), AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseUsers = await _mangaService.Get(id);
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers);
            }

            return Ok(responseUsers);
        }
        [HttpGet("MangaOnPage/{id}"), AllowAnonymous]
        public async Task<IActionResult> GetByOnPage(int id)
        {
            var responseUsers = await _mangaService.GetComplete(id);
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

            var manga = JsonConvert.DeserializeObject<Manga>(value);

            var response = await _mangaService.Insert(manga);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}"), Authorize(Policy = "Admin")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] string value)
        {
            var user = JsonConvert.DeserializeObject<Manga>(value);

            var response = await _mangaService.Update(user);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}"), Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _mangaService.Delete(id);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
