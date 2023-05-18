using System.ComponentModel.DataAnnotations;

namespace MvcPresentationLayer.Models.UserModel
{
    public class UserInsertViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter nickname please", AllowEmptyStrings = false)]
        public string Nickname { get; set; }
        [Required(ErrorMessage = "Enter email please", AllowEmptyStrings = false)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter password please", AllowEmptyStrings = false)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm your password please", AllowEmptyStrings = false)]
        public string ConfirmPassword { get; set; }
    }
}
