using System.ComponentModel.DataAnnotations;

namespace Entities.Enums
{
    public enum UserAnimeStatus
    {
        [Display(Name = "Watching.")]
        Watching,

        [Display(Name = "Plan to watch.")]
        PlanToWatch,

        [Display(Name = "Completed.")]
        Completed,

        [Display(Name = "Rewatching.")]
        ReWatching,

        [Display(Name = "Paused.")]
        Paused,

        [Display(Name = "Dropped.")]
        Dropped
    }
}
