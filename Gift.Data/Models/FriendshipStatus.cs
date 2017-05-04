using System.ComponentModel.DataAnnotations;

namespace Gift.Data.Models {
    public enum FriendshipStatus {
        [Display(Name = "Waiting Approval")] Waiting = 0,
        [Display(Name = "Accepted")] Accepted = 1,
        [Display(Name = "Declined")] Declined = 2        
    }
}