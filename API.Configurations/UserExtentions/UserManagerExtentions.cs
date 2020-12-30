using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Configurations.UserExtentions
{
    public static class UserManagerExtentions
    {
        public static async Task<AppUser> FindByEmailWithAddressAsync (this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault (x => x.Type == ClaimTypes.Email)?.Value;
            return await input.Users.Include (x => x.Address).SingleOrDefaultAsync (x => x.Email == email);
        }
         public static async Task<AppUser> FindByEmailFromClaimPrincipale (this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault (x => x.Type == ClaimTypes.Email)?.Value;
            return await input.Users.SingleOrDefaultAsync (x => x.Email == email);
        }
    }
}