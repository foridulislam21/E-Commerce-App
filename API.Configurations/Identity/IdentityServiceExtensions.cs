using API.Models.Identity;
using API.StorageCenter.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace API.Configurations.Identity
{
    public class IdentityServiceExtensions
    {
        public static void AddIdentityService (IServiceCollection services)
        {
            var builder = services.AddIdentityCore<AppUser> ();
            builder = new IdentityBuilder (builder.UserType, builder.Services);
            builder.AddEntityFrameworkStores<AppIdentityDbContext> ();
            builder.AddSignInManager<SignInManager<AppUser>> ();
            services.AddAuthentication();
        }
    }
}