using API.Abstractions.BLL;
using API.Abstractions.Repository;
using API.BLL;
using API.Repositories;
using API.StorageCenter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API.Configurations.DI_Configuration
{
    public class ServicesConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            //product
            services.AddTransient<IProductManager, ProductManager>();
            services.AddTransient<IProductRepository, ProductRepository>();
            //DB
            services.AddScoped<DbContext, StoreContext>();
        }
    }
}