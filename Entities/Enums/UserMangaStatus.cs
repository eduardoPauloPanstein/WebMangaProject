using System.ComponentModel.DataAnnotations;

namespace Entities.Enums
{
    public enum UserMangaStatus
    {
        [Display(Name = "Reading.")]
        Reading,

        [Display(Name = "Plan to read.")]
        PlanToRead,

        [Display(Name = "Completed.")]
        Completed,

        [Display(Name = "Rereading.")]
        Rereading, 

        [Display(Name = "Paused.")]
        Paused,

        [Display(Name = "Dropped.")]
        Dropped
    }
}
