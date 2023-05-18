using AutoMapper;
using Entities.AnimeS;
using Entities.Enums;
using Entities.MangaS;
using Entities.UserS;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcPresentationLayer.Apis.MangaProjectApi;
using MvcPresentationLayer.Apis.MangaProjectApi.UserItem.UserAnimeItem;
using MvcPresentationLayer.Apis.MangaProjectApi.UserItem.UserMangaItem;
using MvcPresentationLayer.Models.AnimeModel;
using MvcPresentationLayer.Models.MangaModels;
using MvcPresentationLayer.Models.User;
using MvcPresentationLayer.Models.UserModel;
using MvcPresentationLayer.Utilities;
using Shared;
using Shared.Models.User;
using Shared.Responses;
using System.Security.Claims;

namespace MvcPresentationLayer.Controllers
{

    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMangaProjectApiUserService _userApiService;
        private readonly IMangaProjectApiAnimeItem _userItemApiAnimeService;
        private readonly IMangaProjectApiMangaItem _userItemApiMangaService;

        private string _filePath;

        public UserController(IMapper mapper, IWebHostEnvironment env, IMangaProjectApiUserService userApiService, IMangaProjectApiAnimeItem userItemApiAnimeService, IMangaProjectApiMangaItem userItemApiMangaService)
        {
            this._filePath = env.WebRootPath;
            this._mapper = mapper;
            this._userApiService = userApiService;
            this._userItemApiMangaService = userItemApiMangaService;
            this._userItemApiAnimeService = userItemApiAnimeService;
        }

        [HttpGet, Authorize(Policy = "Admin")]
        public async Task<IActionResult> Index()
        {
            DataResponse<User> responseUsers = await _userApiService.Get(UserService.GetToken(HttpContext));
            if (!responseUsers.HasSuccess)
            {
                return BadRequest(responseUsers.Exception);
            }
            List<UserSelectViewModel> users =
                _mapper.Map<List<UserSelectViewModel>>(responseUsers.Data);
            return View(users);
        }


        #region Avatar
        public async Task<Response> SaveAvatarFileAsync(IFormFile file, UserProfileUpdate user)
        {
            var response = ImageFileValidator(file);
            if (!response.HasSuccess)
                return response;

            //var name = Guid.NewGuid().ToString() + file.FileName;
            string folder = "\\avatars";
            string filePath = _filePath + folder;
            string extension = Path.GetExtension(file.FileName);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath); //folder
            }

            string name = $"{user.Nickname}Avatar{extension}";

            DeleteAvatarImage(folder, name);
            using (var stream = System.IO.File.Create(filePath + "\\" + name))
            {
                await file.CopyToAsync(stream);
            }

            user.AvatarImageFileLocation = name;

            return ResponseFactory.CreateInstance().CreateSuccessResponse();
        }

        public void DeleteAvatarImage(string folder, string fileName)
        {
            string filePath = $"{_filePath}{folder}\\{fileName}";
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
        #endregion

        #region Cover
        public async Task<Response> SaveCoverFileAsync(IFormFile file, UserProfileUpdate user)
        {
            var response = ImageFileValidator(file);
            if (!response.HasSuccess)
                return response;

            //var name = Guid.NewGuid().ToString() + file.FileName;
            string folder = "\\covers";
            string filePath = _filePath + folder;
            string extension = Path.GetExtension(file.FileName);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath); //folder
            }

            string name = $"{user.Nickname}Cover{extension}";

            DeleteCoverImage(folder, name);
            using (var stream = System.IO.File.Create(filePath + "\\" + name))
            {
                await file.CopyToAsync(stream);
            }

            user.CoverImageFileLocation = name;

            return ResponseFactory.CreateInstance().CreateSuccessResponse();
        }


        public void DeleteCoverImage(string folder, string fileName)
        {
            string filePath = $"{_filePath}{folder}\\{fileName}";
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
        #endregion

        #region Utilities

        public bool IsAuthenticated()
        {
            return HttpContext.User.Identity.IsAuthenticated;
        }
        public RedirectToActionResult RedirectIfIsAuthenticated()
        {
            return RedirectToAction("Index", "Home");
        }

        public bool IsAmMyself(int? id)
        {
            int MyId = UserService.GetId(HttpContext);
            return MyId == id;
        }
        public RedirectToActionResult RedirectIfImNotMe()
        {
            return RedirectToAction("Index", "Home");
        }

        public bool IsAdmin()
        {
            return UserService.IsRole(UserRoles.Admin.ToString(), HttpContext);
        }

        private async Task<bool> UserExists(int id)
        {
            var userSelectResponse = await _userApiService.Get(id, UserService.GetToken(HttpContext));
            return userSelectResponse.HasSuccess;
        }

        public Response ImageFileValidator(IFormFile file)
        {
            switch (file.ContentType)
            {
                case "image/jpeg": return ResponseFactory.CreateInstance().CreateSuccessResponse();
                case "image/bmp": return ResponseFactory.CreateInstance().CreateSuccessResponse();
                case "image/gif": return ResponseFactory.CreateInstance().CreateSuccessResponse();
                case "image/png": return ResponseFactory.CreateInstance().CreateSuccessResponse();

                default:
                    return ResponseFactory.CreateInstance().CreateFailedResponse(null);
            }
        }

        [Authorize]
        public IActionResult TesteAuth() => Ok(User.Claims.Select(x => new { Type = x.Type, Value = x.Value }));

        #endregion

        #region Login

        [HttpGet, AllowAnonymous]
        public IActionResult Login()
        {
            if (IsAuthenticated())
                return RedirectIfIsAuthenticated();

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginPageViewModel userLoginView)
        {
            UserLogin login = new()
            {
                EmailOrNickname = userLoginView.UserLogin.EmailOrNickname,
                Password = userLoginView.UserLogin.Password
            };
            var response = await _userApiService.Login(login);

            if (response.HasSuccess)
            {
                _ = AuthenticationAsync(response.Item, response.Token);
                return RedirectToAction("Index", "Home");

            }

            ViewBag.Errors = response.Message;
            return View();
        }


        public async Task AuthenticationAsync(User user, string token)
        {
            ClaimsIdentity identity = new("CookieSerieAuth");

            identity.AddClaim(new Claim(ClaimTypes.Name, user.Nickname));
            identity.AddClaim(new Claim(ClaimTypes.PrimarySid, user.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()));
            identity.AddClaim(new Claim("Token", token));

            ClaimsPrincipal principal = new(identity);

            Thread.CurrentPrincipal = principal;

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddHours(8),
                IssuedUtc = DateTime.UtcNow
            };

            await HttpContext.SignInAsync("CookieSerieAuth", principal, authProperties);
        }

        public async Task LogoutAuthenticationAsync()
        {
            await HttpContext.SignOutAsync("CookieSerieAuth");
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Logout()
        {
            await LogoutAuthenticationAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion

        [HttpGet, Authorize]
        public async Task<IActionResult> Profile()
        {
            var ctx = HttpContext;
            int id = UserService.GetId(ctx);

            if (!IsAmMyself(id))
                return RedirectIfImNotMe();

            SingleResponse<User> response = await _userApiService.Get(id, UserService.GetToken(ctx));
            //var responseMangaFavorites = await _userItemApiMangaService.GetUserFavorites(id, null);
            //var responseAnimeFavorites = await _userItemApiAnimeService.GetUserFavorites(id, UserService.GetToken(ctx));

            var user = _mapper.Map<UserProfileViewModel>(response.Item);
            //var animeFavorite = _mapper.Map<List<AnimeShortViewModel>>(responseAnimeFavorites.Data);
            //var mangaFavorite = _mapper.Map<List<MangaShortViewModel>>(responseMangaFavorites.Data);

            UserProfileItemViewModel userProfileViewModel = new()
            {
               User = user
                //FavoriteAnimeList = animeFavorite,
                //FavoriteMangaList = mangaFavorite
            };

            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }

            return View(userProfileViewModel);
        }


        [HttpGet, AllowAnonymous]
        public IActionResult Create()
        {
            if (IsAuthenticated())
                return RedirectIfIsAuthenticated();

            return View();
        }
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Create(UserLoginPageViewModel userCreate)
        {
            //User user = _mapper.Map<User>(viewModel);

            UserCreate create = new()
            {
                Nickname = userCreate.UserInsert.Nickname,
                Email = userCreate.UserInsert.Email,
                Password = userCreate.UserInsert.Password,
                ConfirmPassword = userCreate.UserInsert.ConfirmPassword
            };
            var response = await _userApiService.Insert(create, UserService.GetToken(HttpContext));

            if (response.HasSuccess)
                return RedirectToAction("Index");

            ViewBag.Errors = response.Message;
            return View();
        }


        [HttpGet, Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            if (!IsAmMyself(id))
                return RedirectIfImNotMe();

            var userSelectResponse = await _userApiService.Get(id, UserService.GetToken(HttpContext));

            if (!userSelectResponse.HasSuccess)
            {
                return NotFound();
            }
            User user = userSelectResponse.Item;
            UserUpdateViewModel userUpdate = _mapper.Map<UserUpdateViewModel>(user);

            return View(userUpdate);
        }
        
        [HttpPost, Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nickname,About,AvatarImage,CoverImage")] UserUpdateViewModel userUpdate, IFormFile fileA, IFormFile fileC)
        {
            Response response;

            if (id != userUpdate.Id)
            {
                return NotFound();
            }

            UserProfileUpdate user = _mapper.Map<UserProfileUpdate>(userUpdate);
            
            if (fileA != null)
            {
                await SaveAvatarFileAsync(fileA, user);
            }
            if (fileC != null)
            {
                await SaveCoverFileAsync(fileC, user);
            }
            response = await _userApiService.Update(user, UserService.GetToken(HttpContext));

            if (!response.HasSuccess)
            {
                ViewBag.Errors = response.Message;
                return View(userUpdate);
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpPost, Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            //only admin or the logged in user
            if (!IsAdmin())
            {
                if (!IsAmMyself(id))
                    return RedirectIfImNotMe();
            }


            if (!await UserExists(id))
                return Problem("User is not exist.");


            await _userApiService.Delete(id, UserService.GetToken(HttpContext));
            //Delete avatar

            return RedirectToAction(nameof(Index));
        }

    }
}
