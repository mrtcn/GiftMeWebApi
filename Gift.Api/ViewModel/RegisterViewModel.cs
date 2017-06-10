using System.ComponentModel.DataAnnotations;
using System.Web;
using Gift.Data.Models;
using Gift.Data.Models.VariousTypes;

namespace Gift.Api.ViewModel {
    public class RegisterViewModel : IUserBindingModel
    {
        public RegisterViewModel()
        {
            
        }

        public RegisterViewModel(ApplicationUser user)
        {
            Id = user.Id;
            UserName = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            FullName = user.FullName;
            Gender = user.Gender;
            Email = user.Email;
            ImagePath = user.ImagePath;
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "FullName")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public GenderType Gender { get; set; }

        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        
        public string ImagePath { get; set; }
    }

    public class ExternalRegisterViewModel : IUserBindingModel
    {
        public ExternalRegisterViewModel() {}

        public ExternalRegisterViewModel(ApplicationUser user, string loginProvider, string externalAccessToken, string providerKey)
        {
            Id = user.Id;
            UserName = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            FullName = user.FullName;
            Gender = user.Gender;
            Email = user.Email;
            ImagePath = user.ImagePath;
            ExternalAccessToken = externalAccessToken;
            LoginProvider = loginProvider;
            ProviderKey = providerKey;
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public GenderType Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }        
        public string ExternalAccessToken { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
    }
}