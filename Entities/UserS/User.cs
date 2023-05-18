using Entities.Enums;

namespace Entities.UserS
{
    public class User : Entity
    {

        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public string ConfirmPassword { get; set; }
        //public bool EmailConfirmed { get; set; }
        public DateTime LastLogin { get; set; }
        public UserRoles Role { get; set; }
        public string? About { get; set; }
        //public CivicAddress Address { get; set; }
        public int FavoritesCount { get; set; }
        public int ReviewsCount { get; set; }
        public string? AvatarImageFileLocation { get; set; }
        public string? CoverImageFileLocation { get; set; }
        public bool KeepLogged { get; set; }
        public ICollection<UserMangaItem> MangaList { get; set; }
        public ICollection<UserAnimeItem> AnimeList { get; set; }


        public override void EnableEntity()
        {
            base.EnableEntity();
            this.Role = UserRoles.User;
        }

        
    }
}
