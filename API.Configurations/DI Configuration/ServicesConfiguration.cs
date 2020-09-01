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
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IProductRepository, ProductRepository>();
            //Type
            services.AddScoped<IProductTypeManager, ProductTypeManager>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            //Brand
            services.AddScoped<IProductBrandManager, ProductBrandManager>();
            services.AddScoped<IProductBrandRepository, ProductBrandRepository>();
            //DB
            services.AddScoped<DbContext, StoreContext>();
        }
    }
}