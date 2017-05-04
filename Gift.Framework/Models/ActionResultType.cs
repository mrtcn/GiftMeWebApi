using System.ComponentModel.DataAnnotations;

namespace Gift.Framework.Models {
    public enum ActionResultType {
        [Display(Name = "İşleminiz başarıyla gerçekleşti!")]
        Success,
        [Display(Name = "Beklenmedik bir hata oluştu!")]
        Failure
    }
}