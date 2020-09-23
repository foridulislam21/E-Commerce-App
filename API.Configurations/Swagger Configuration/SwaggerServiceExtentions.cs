using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Configurations.Swagger_Configuration {
    public static class SwaggerServiceExtentions {
        public static void AddSwaggerDocumentation (IServiceCollection services) {
            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new OpenApiInfo { Title = "Mid Street Cafe", Version = "v1" });
            });
        }
        public static void UseSwaggerDocumentation (IApplicationBuilder app) {
            app.UseSwagger ();

            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "Mid Street Cafe v1");
            });
        }
    }
}