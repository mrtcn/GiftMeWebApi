﻿using System.ComponentModel.DataAnnotations;
using Gift.Data.Models;
using Gift.Data.Models.VariousTypes;

namespace Gift.Api.ViewModel
{
    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }

    public class AddLoginBindingModel
    {   [Required]
        [Display(Name = "Access Token")]
        public string AccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class UserBindingModel : IUserBindingModel
    {
        public UserBindingModel(){}

        public UserBindingModel(ApplicationUser user)
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
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public GenderType Gender { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
    }

    public interface IUserBindingModel
    {
        int Id { get; set; }                
        string UserName { get; set; }                
        string FirstName { get; set; }
        string LastName { get; set; }
        string FullName { get; set; }
        GenderType Gender { get; set; }
        string Email { get; set; }
        string ImagePath { get; set; }
    }

    public class ParsedExternalAccessToken
    {
        public string user_id { get; set; }
        public string app_id { get; set; }
    }

    public class ExternalUserBindingModel
    {
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }

        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        [Display(Name = "ExternalAccessToken")]
        public string ExternalAccessToken { get; set; }
    }

    public class SetPasswordBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}