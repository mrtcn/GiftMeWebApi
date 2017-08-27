using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Gift.Data.Entities;
using Gift.Data.Models.VariousTypes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Gift.Data.Models
{
    public class ApplicationUserLogin : IdentityUserLogin<int> { }
    public class ApplicationUserClaim : IdentityUserClaim<int> { }
    public class ApplicationUserRole : IdentityUserRole<int> { }

    public class ApplicationRole : IdentityRole<int, ApplicationUserRole>, IRole<int>
    {
        public string Description { get; set; }

        public ApplicationRole() : base() { }
        public ApplicationRole(string name)
            : this()
        {
            this.Name = name;
        }

        public ApplicationRole(string name, string description)
            : this(name)
        {
            this.Description = description;
        }
    }


    public class ApplicationUser : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IUser<int>
    {
        public ApplicationUser() : base() {
            UserEvents = new HashSet<UserEvent>();
            FavoriteEvents = new HashSet<FavoriteEvent>();
        }
        public ApplicationUser(ApplicationUser user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            UserName = user.UserName;
            Gender = user.Gender;
            Birthdate = user.Birthdate;
            UserEvents = new HashSet<UserEvent>();
            FavoriteEvents = new HashSet<FavoriteEvent>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        public GenderType Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public virtual ICollection<Friend> Friends { get; set; }
        public virtual ICollection<UserEvent> UserEvents { get; set; }
        public virtual ICollection<FavoriteEvent> FavoriteEvents { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager, string defaultAuthenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

    public class ApplicationUserStore :
    UserStore<ApplicationUser, ApplicationRole, int,
    ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationUserStore(ApplicationDbContext context) : base(context)
        {
        }
    }


    public class ApplicationRoleStore
    : RoleStore<ApplicationRole, int, ApplicationUserRole>
    {
        public ApplicationRoleStore(ApplicationDbContext context) : base(context)
        {
        }
    }

}
