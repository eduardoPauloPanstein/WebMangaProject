using BusinessLogicalLayer.Interfaces.IUserInterfaces;
using Entities.UserS;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared;
using Shared.Models.User;
using Shared.Responses;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILog _log; 

        public UserController(IUserService userService, ILog log)
        {
            this._userService = userService;
            this._log = log;
            _userService.CreateAdmin();
        }

        /// <summary>
        /// Take users
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet(template:"skip/{skip}/take/{take}"), Authorize]
        public async Task<IActionResult> GetAsync([FromRoute] int skip = 0, [FromRoute] int take = 25)
        {
            if (take >= 100)
            {
                _log.Warn("Bad request, try to take more than 100 datas");
                return BadRequest("take < 100");
            }
            DataResponse<User> responseUsers = await _userService.Get(skip, take);
            if (!responseUsers.HasSuccess)
            {
                _log.Error(responseUsers.Message);
                return BadRequest(responseUsers);
            }

            return Ok(responseUsers);
        }

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> GetAsync(int id)
        {
            var responseUsers = await _userService.Get(id);
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers);
            }

            return Ok(responseUsers);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> PostAsync(UserCreate userCreate)
        {
            //if (IsAuthenticated())

            //var user = JsonConvert.DeserializeObject<User>(value);

            var response = await _userService.Insert(userCreate);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UserProfileUpdate user) 
        {
            if (!UserService.IsAdmin(HttpContext))
            {
                if (!UserService.IsAmMyself(HttpContext, id))
                    return BadRequest();
            }

            //var user = JsonConvert.DeserializeObject<User>(value);

            var response = await _userService.Update(user);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            //only admin or the logged in user
            if (!UserService.IsAdmin(HttpContext))
            {
                if (!UserService.IsAmMyself(HttpContext, id))
                    return BadRequest();
            }

            var response = await _userService.Delete(id);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpPost("Authenticate"), AllowAnonymous]
        public async Task<IActionResult> AuthenticateAsync(UserLogin user)
        {
            //mvc need a SingleResponseWToken return 
            _log.Debug("Trying to authenticate.");

            //if (UserService.IsAuthenticated(HttpContext))
            //{
            //    _log.Warn("User is already authenticated.");
            //    return BadRequest("User is authenticated.");
            //}

            var response = await _userService.Login(user);

            if (!response.HasSuccess)
            {
                _log.Warn("Login failed.");

                if (response.IsInfrastructureError)
                    return BadRequest(response.Exception);
                //return BadRequest(ResponseFactory.CreateInstance().CreateFailedSingleResponseWToken<User>(response.Message));
                else
                    return Unauthorized();
            }

            _log.Debug("Generate token.");
            var token = TokenService.GenerateToken(response.Item);
            _log.Info($"User {response.Item.Id} has been authenticated.");
            return Ok(ResponseFactory.CreateInstance().CreateSuccessSingleResponseWToken(token, response.Item));
        }

    }
}
