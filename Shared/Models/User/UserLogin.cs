namespace Shared.Models.User
{
    public class UserLogin
    {
        public UserLogin(string emailOrNickname, string password, bool keepLogged)
        {
            this.EmailOrNickname = emailOrNickname;
            this.Password = password;
            this.KeepLogged = keepLogged;
        }
        public UserLogin()
        {

        }

        public string EmailOrNickname { get; set; }
        public string Password { get; set; }
        public bool KeepLogged { get; set; }

    }
}
