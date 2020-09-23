using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Configurations.AutoMapperProfile;
using API.Configurations.DI_Configuration;
using API.Configurations.Error;
using API.Configurations.MiddleWare;
using API.StorageCenter;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace API {
    public class Startup {
        private readonly IConfiguration _configuration;

        public Startup (IConfiguration configuration) {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddAutoMapper (typeof (MapperProfile));
            services.AddControllers ()
                .AddNewtonsoftJson (option =>
                    option.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            services.AddDbContext<StoreContext> (x =>
                x.UseSqlite (_configuration.GetConnectionString ("DefaultConnection")));
            ServicesConfiguration.Configure (services);
            services.Configure<ApiBehaviorOptions> (options => {
                options.InvalidModelStateResponseFactory = actionContext => {
                    var errors = actionContext.ModelState
                        .Where (e => e.Value.Errors.Count > 0)
                        .SelectMany (x => x.Value.Errors)
                        .Select (x => x.ErrorMessage).ToArray ();
                    var errorResponse = new ApiValidationErrorResponse {
                        Errors = errors
                    };
                    return new BadRequestObjectResult (errorResponse);
                };
            });
            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new OpenApiInfo { Title = "Mid Street Cafe", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseMiddleware<ExceptionMiddleware> ();

            app.UseStatusCodePagesWithReExecute ("/errors/{0}");

            app.UseHttpsRedirection ();

            app.UseRouting ();

            app.UseStaticFiles ();

            app.UseAuthorization ();

            app.UseSwagger ();

            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "Mid Street Cafe v1");
            });

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}