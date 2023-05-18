using Entities.Enums;
using Entities.MangaS;
using Entities.UserS;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcPresentationLayer.Models.MangaModels
{
    public class UserFavoriteMangaViewModel
    {
        public int Id { get; set; }

        public Manga Manga { get; set; }
        public int MangaId { get; set; }

        public int UserId { get; set; }
        public Entities.UserS.User User { get; set; }

        public UserMangaStatus Status { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; } 
        [DataType(DataType.Date)]
        [DisplayName("Finish Date")]
        public DateTime FinishDate { get; set; } 
        public MangaScore Score { get; set; }
        [DisplayName("Total Rereads")]
        public int? TotalRereads { get; set; }
        public int? Chapter { get; set; }
        public int? Volume { get; set; }
        [DisplayName("Private Notes")]
        public string? PrivateNotes { get; set; }
        [DisplayName("Public Notes")]
        public string? PublicNotes { get; set; }
        public bool Private { get; set; }
        public bool Favorite { get; set; }
    }
}
    