using System.ComponentModel.DataAnnotations;

namespace MvcPresentationLayer.Models.User
{
    public class UserSelectViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        public string Nickname { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        [Display(Name ="Created At")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Last Login")]
        public DateTime LastLogin { get; set; }

        [Display(Name = "Favorites Count")]
        public int FavoritesCount { get; set; }

        [Display(Name = "Reviews Count")]
        public int ReviewsCount { get; set; }
    }
}
