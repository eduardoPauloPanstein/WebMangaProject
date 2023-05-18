using System.ComponentModel.DataAnnotations;

namespace MvcPresentationLayer.Models.UserModel
{
    public class UserLoginViewModel 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Email or nickname please", AllowEmptyStrings = false)]
        [Display(Name = "Email or nickname")]
        public string EmailOrNickname { get; set; }

        [Required(ErrorMessage = "Enter password please", AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
