
namespace Shared.Models.User
{
    public class UserCreate
    {
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        //public bool EmailConfirmed { get; set; }

        public bool PasswordHasBeenConfirmed { get { return Password == ConfirmPassword; } }

    }
}
