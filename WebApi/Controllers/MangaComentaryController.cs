using BusinessLogicalLayer.Interfaces.IUserComentaryService;
using Microsoft.AspNetCore.Mvc;
using Entities.AnimeS;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Entities.MangaS;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MangaComentaryController : ControllerBase
    {
        private readonly IMangaComentary _MangaComentary;

        public MangaComentaryController(IMangaComentary mangaService)
        {
            this._MangaComentary = mangaService;
        }
        [HttpGet(template: "skip/{skip}/take/{take}"), AllowAnonymous]
        public async Task<IActionResult> GetAsync([FromRoute] int skip = 0, [FromRoute] int take = 25)
        {
            if (take >= 100)
            {
                return BadRequest("take < 100");
            }
            var responseUsers = await _MangaComentary.Get(skip, take);
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers);
            }

            return Ok(responseUsers);
        }

        [HttpGet("ById/{id}"), AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseUsers = await _MangaComentary.Get(id);
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers);
            }

            return Ok(responseUsers);
        }
        [HttpGet("ByUser/{id}"), AllowAnonymous]
        public async Task<IActionResult> GetByUserAsync(int id)
        {
            var responseUsers = await _MangaComentary.GetByUser(id);
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers);
            }

            return Ok(responseUsers);
        }
        [HttpGet("ByMAnga/{id}"), AllowAnonymous]
        public async Task<IActionResult> GetByMangaAsync(int id)
        {
            var responseUsers = await _MangaComentary.GetByManga(id);
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

            var manga = JsonConvert.DeserializeObject<MangaComentary>(value);

            var response = await _MangaComentary.Insert(manga);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}"), AllowAnonymous]
        public async Task<IActionResult> PutAsync(int id, [FromBody] string value)
        {
            var user = JsonConvert.DeserializeObject<MangaComentary>(value);

            var response = await _MangaComentary.Update(user);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}"), AllowAnonymous]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _MangaComentary.Delete(id);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
