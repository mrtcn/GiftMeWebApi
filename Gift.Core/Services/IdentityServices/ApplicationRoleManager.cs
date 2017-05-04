using Gift.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Gift.Core.Services.IdentityServices
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole, int>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, int> store)
            : base(store)
        {
        }

        public static ApplicationRoleManager Create(
            IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var manager = new ApplicationRoleManager(
                new ApplicationRoleStore(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames 

            return manager;
        }
    }
}
