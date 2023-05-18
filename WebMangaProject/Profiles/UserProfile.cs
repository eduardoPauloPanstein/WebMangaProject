using AutoMapper;
using Entities.UserS;
using MvcPresentationLayer.Models.AnimeModel;
using MvcPresentationLayer.Models.MangaModels;
using MvcPresentationLayer.Models.User;
using MvcPresentationLayer.Models.UserModel;
using Shared.Models.User;

namespace MvcPresentationLayer.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserSelectViewModel, User>();
            CreateMap<User, UserSelectViewModel>();

            CreateMap<UserInsertViewModel, User>();
            CreateMap<User, UserInsertViewModel>();


            CreateMap<UserUpdateViewModel, User>();
            CreateMap<User, UserUpdateViewModel>();

            CreateMap<UserLoginViewModel, User>();
            CreateMap<User, UserLoginViewModel>();

            CreateMap<UserFavoriteMangaViewModel, User>();
            CreateMap<User, UserFavoriteMangaViewModel>();

            CreateMap<UserFavoriteAnimeViewModel, User>();
            CreateMap<User, UserFavoriteAnimeViewModel>();

            CreateMap<UserProfileViewModel, User>();
			CreateMap<User, UserProfileViewModel>();

			CreateMap<UserFavoriteMangaViewModel, UserMangaItem>();
            CreateMap<UserMangaItem, UserFavoriteMangaViewModel>();

            CreateMap<UserFavoriteAnimeViewModel, UserAnimeItem>();
            CreateMap<UserAnimeItem, UserFavoriteAnimeViewModel>();

            CreateMap<User, UserProfileUpdate>();
            CreateMap<UserProfileUpdate, User>();

            CreateMap<UserProfileUpdate, UserUpdateViewModel>();
            CreateMap<UserUpdateViewModel, UserProfileUpdate>();

        }
    }
}
