using System.ComponentModel.DataAnnotations;

namespace Gift.Data.Models {
    public enum PermissionStatus {
        [Display(Name = "Only Friends")] Friends = 0,
        [Display(Name = "Everyone")] Everyone = 1,
        [Display(Name = "Specific Friends")] Specific = 2        
    }
}