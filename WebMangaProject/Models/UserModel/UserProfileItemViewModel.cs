using Entities.UserS;
using MvcPresentationLayer.Models.AnimeModel;
using MvcPresentationLayer.Models.MangaModels;

namespace MvcPresentationLayer.Models.UserModel
{
    public class UserProfileItemViewModel
    {

        public UserProfileViewModel User { get; set; }
        public List<MangaShortViewModel> FavoriteMangaList { get; set; }
        public List<AnimeShortViewModel> FavoriteAnimeList { get; set; }

    }
}
