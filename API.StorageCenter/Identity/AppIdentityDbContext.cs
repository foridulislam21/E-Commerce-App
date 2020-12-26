using API.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.StorageCenter.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext (DbContextOptions<AppIdentityDbContext> options) : base (options) { }

        protected override void OnModelCreating (ModelBuilder builder)
        {
            base.OnModelCreating (builder);
        }
    }
}