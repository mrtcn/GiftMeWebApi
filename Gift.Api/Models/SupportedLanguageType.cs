using System.ComponentModel.DataAnnotations;

namespace Gift.Api.Models
{
    public enum SupportedLanguageType {
        [Display(Name = "tr-TR")]
        Turkish = 1,
        [Display(Name = "en-US")]
        English = 2,
    }
}