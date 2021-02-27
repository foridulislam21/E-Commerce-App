using System.IO;
using API.Abstractions.BLL.Core;
using API.Configurations.AutoMapperProfile;
using API.Configurations.DI_Configuration;
using API.Configurations.Identity;
using API.Configurations.MiddleWare;
using API.Configurations.Swagger_Configuration;
using API.StorageCenter.Data;
using API.StorageCenter.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using StackExchange.Redis;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup (IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureDevelopmentServices(IServiceCollection services){
            services.AddDbContext<StoreContext> (x =>
                x.UseSqlite (_configuration.GetConnectionString ("DefaultConnection")));

            services.AddDbContext<AppIdentityDbContext> (x =>
            {
                x.UseSqlite (_configuration.GetConnectionString ("IdentityConnection"));
            });
            ConfigureServices(services);
        }
         public void ConfigureProductionServices(IServiceCollection services){
            services.AddDbContext<StoreContext> (x =>
                x.UseMySql (_configuration.GetConnectionString ("DefaultConnection")));

            services.AddDbContext<AppIdentityDbContext> (x =>
            {
                x.UseMySql (_configuration.GetConnectionString ("IdentityConnection"));
            });
            ConfigureServices(services);
        }

        public void ConfigureServices (IServiceCollection services)
        {
            services.AddAutoMapper (typeof (MapperProfile));
            services.AddControllers ()
                .AddNewtonsoftJson (option =>
                    option.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            services.AddSingleton<IConnectionMultiplexer> (c =>
            {
                var config = ConfigurationOptions.Parse (_configuration.GetConnectionString ("Redis"), true);
                return ConnectionMultiplexer.Connect (config);
            });
            ServicesConfiguration.Configure (services);
            services.AddIdentityService(_configuration);
            SwaggerServiceExtentions.AddSwaggerDocumentation (services);
            services.AddCors (options =>
            {
                options.AddPolicy ("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader ()
                        .AllowAnyMethod ()
                        .WithOrigins ("https://localhost:4200");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware> ();

            app.UseStatusCodePagesWithReExecute ("/errors/{0}");

            app.UseHttpsRedirection ();

            app.UseRouting ();

            app.UseStaticFiles ();
            app.UseStaticFiles (new StaticFileOptions{
                FileProvider =  new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Content")
                ), RequestPath = "/content"
            });
            app.UseCors ("CorsPolicy");
            app.UseAuthentication ();
            app.UseAuthorization ();
            SwaggerServiceExtentions.UseSwaggerDocumentation (app);

            app.UseEndpoints (endpoints =>
            {
                endpoints.MapControllers ();
                endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}