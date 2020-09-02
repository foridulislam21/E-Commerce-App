using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Configurations.AutoMapperProfile;
using API.Configurations.DI_Configuration;
using API.StorageCenter;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseHttpsRedirection ();

            app.UseRouting ();
            
            app.UseStaticFiles ();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}