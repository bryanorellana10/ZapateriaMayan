using CapaBaseDeDatos.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Security
{
    public class ApplicationSignInManager : SignInManager<User, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            :base(userManager, authenticationManager)
        {

        }

        public override Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool rememberMe, bool shouldLockout)
        {
            var user = UserManager.FindByNameAsync(userName).Result;

            if ((!user.IsInactive && user.IsInactive) || user.IsInactive)
            {
                return Task.FromResult<SignInStatus>(SignInStatus.LockedOut);
            }

            return base.PasswordSignInAsync(userName, password, rememberMe, shouldLockout);
        }

    }
}
