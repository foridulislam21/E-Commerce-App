using System.Linq;
using API.Abstractions.BLL;
using API.Abstractions.BLL.Core;
using API.Abstractions.Interfaces;
using API.Abstractions.Repository;
using API.Abstractions.TokenService;
using API.BLL;
using API.Configurations.Error;
using API.Configurations.Service;
using API.Repositories;
using API.Repositories.Services;
using API.StorageCenter.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API.Configurations.DI_Configuration
{
    public class ServicesConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            //Data Related
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
            //Basket
            services.AddScoped<IBasketManager, BasketRepository>();
            //order
           // services.AddScoped<IOrderService, OrderService>();
            //Token
            services.AddScoped<ITokenService, TokenService>();
            //Configuration Related
            services.Configure<ApiBehaviorOptions>(options =>
           {
               options.InvalidModelStateResponseFactory = actionContext =>
               {
                   var errors = actionContext.ModelState
                       .Where(e => e.Value.Errors.Count > 0)
                       .SelectMany(x => x.Value.Errors)
                       .Select(x => x.ErrorMessage).ToArray();
                   var errorResponse = new ApiValidationErrorResponse
                   {
                       Errors = errors
                   };
                   return new BadRequestObjectResult(errorResponse);
               };
           });
        }
    }
}