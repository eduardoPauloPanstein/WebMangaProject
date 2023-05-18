using Entities.Enums;
using Entities.UserS;

namespace MvcPresentationLayer.Models.UserModel
{
    public class UserProfileViewModel
    {
		public int Id { get; set; }

		public string Nickname { get; set; }

		public string Email { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.Now;

		public DateTime LastLogin { get; set; }

		public string? About { get; set; }

		public int FavoritesCount { get; set; }

		public int ReviewsCount { get; set; }

		public string? AvatarImageFileLocation { get; set; }

		public string? CoverImageFileLocation { get; set; }

		public ICollection<UserMangaItem> MangaList { get; set; }
        public ICollection<UserAnimeItem> AnimeList { get; set; }
	}
}
