using System.Linq;
using System.Threading.Tasks;
using API.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace API.StorageCenter.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager){
            if (!userManager.Users.Any())
            {
                var user = new AppUser{
                    DisplayName = "Forid",
                    Email = "foridulislam21@gmail.com",
                    UserName = "foridulislam21@gmail.com",
                    Address = new Address{
                        FirstName = "Forid",
                        LastName = "Islam",
                        Street = "13 Rupnagor Abashik, Dhaka-1216.",
                        City = "Dhaka",
                        State = "Dhaka",
                        ZipCode = "1216"
                    }
                };
                await userManager.CreateAsync(user, "*Moria_1590");
            }
        }
    }
}