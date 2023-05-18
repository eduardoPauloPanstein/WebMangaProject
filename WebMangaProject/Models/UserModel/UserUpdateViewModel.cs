namespace MvcPresentationLayer.Models.UserModel
{
    public class UserUpdateViewModel
    {
        public int Id { get; set; }
        public string? Nickname { get; set; }
        public string? About { get; set; }
        public string? AvatarImageFileLocation { get; set; }
        public string? CoverImageFileLocation { get; set; }
    }
}
