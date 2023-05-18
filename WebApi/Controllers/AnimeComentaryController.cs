using BusinessLogicalLayer.Interfaces.IUserComentaryService;
using Microsoft.AspNetCore.Mvc;
using Entities.AnimeS;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimeComentaryController : ControllerBase
    {
        private readonly IAnimeComentary _AnimeComentary;

        public AnimeComentaryController(IAnimeComentary animeService)
        {
            this._AnimeComentary = animeService;
        }
        [HttpGet(template: "skip/{skip}/take/{take}"), AllowAnonymous]
        public async Task<IActionResult> GetAsync([FromRoute] int skip = 0, [FromRoute] int take = 25)
        {
            if (take >= 100)
            {
                return BadRequest("take < 100");
            }
            var responseUsers = await _AnimeComentary.Get(skip, take);
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers);
            }

            return Ok(responseUsers);
        }

        [HttpGet("ById/{id}"), AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseUsers = await _AnimeComentary.Get(id);
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers);
            }

            return Ok(responseUsers);
        }
        [HttpGet("ByUser/{id}"), AllowAnonymous]
        public async Task<IActionResult> GetByUserAsync(int id)
        {
            var responseUsers = await _AnimeComentary.GetByUser(id);
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers);
            }

            return Ok(responseUsers);
        }

        [HttpGet("ByAnime/{id}"), AllowAnonymous]
        public async Task<IActionResult> GetByAnimeAsync(int id)
        {
            var responseUsers = await _AnimeComentary.GetByAnime(id);
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

            var manga = JsonConvert.DeserializeObject<AnimeComentary>(value);

            var response = await _AnimeComentary.Insert(manga);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}"), AllowAnonymous]
        public async Task<IActionResult> PutAsync(int id, [FromBody] string value)
        {
            var user = JsonConvert.DeserializeObject<AnimeComentary>(value);

            var response = await _AnimeComentary.Update(user);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}"), AllowAnonymous]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _AnimeComentary.Delete(id);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


    }
}
