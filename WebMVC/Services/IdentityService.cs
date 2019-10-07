using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class IdentityService : IIdentityService<ApplicationUser>
    {
        public ApplicationUser Get(IPrincipal principal)
        {
            if (principal is ClaimsPrincipal claims)
            {
                var user = new ApplicationUser()
                {
                    Email = claims.Claims.FirstOrDefault(x => x.Type == "preferred_username")?.Value ?? "",
                    Id = claims.Claims.FirstOrDefault(x => x.Type == "name")?.Value ?? "",
                };

                return user;

            }

            throw new ArgumentException(message: "The principal must be a ClaimsPrincipal", paramName: nameof(principal));
        }
    }
}
