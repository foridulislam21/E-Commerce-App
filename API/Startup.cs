using API.Configurations.AutoMapperProfile;
using API.Configurations.DI_Configuration;
using API.Configurations.MiddleWare;
using API.Configurations.Swagger_Configuration;
using API.StorageCenter;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup (IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services)
        {
            services.AddAutoMapper (typeof (MapperProfile));
            services.AddControllers ()
                .AddNewtonsoftJson (option =>
                    option.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            services.AddDbContext<StoreContext> (x =>
                x.UseSqlite (_configuration.GetConnectionString ("DefaultConnection")));
            ServicesConfiguration.Configure (services);
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
            app.UseCors ("CorsPolicy");

            app.UseAuthorization ();
            SwaggerServiceExtentions.UseSwaggerDocumentation (app);

            app.UseEndpoints (endpoints =>
            {
                endpoints.MapControllers ();
            });
        }
    }
}