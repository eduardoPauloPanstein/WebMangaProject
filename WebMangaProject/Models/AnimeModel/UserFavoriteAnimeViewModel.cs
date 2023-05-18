using Entities.AnimeS;
using Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MvcPresentationLayer.Models.AnimeModel
{
    public class UserFavoriteAnimeViewModel
    {
        public int Id { get; set; }
        public Anime Anime { get; set; }
        public int AnimeId { get; set; }
        public int UserId { get; set; }
        public Entities.UserS.User User { get; set; }
        public UserAnimeStatus Status { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Finish Date")]
        public DateTime FinishDate { get; set; }
        public MangaScore? Score { get; set; }
        [DisplayName("Total Rewatches")]
        public int? TotalRewatches{ get; set; }
        public int? Episode { get; set; }
        [DisplayName("Private Notes")]
        public string? PrivateNotes { get; set; }
        [DisplayName("Public Notes")]
        public string? PublicNotes { get; set; }
        public bool Private { get; set; }
        public bool Favorite { get; set; }
    }
}
