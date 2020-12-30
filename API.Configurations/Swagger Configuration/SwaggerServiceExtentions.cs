using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Configurations.Swagger_Configuration {
    public static class SwaggerServiceExtentions {
        public static void AddSwaggerDocumentation (IServiceCollection services) {
            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new OpenApiInfo { Title = "Mid Street Cafe", Version = "v1" });
                var securitySchema = new OpenApiSecurityScheme{
                    Description = "JWT Auth Bearer Scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirement = new OpenApiSecurityRequirement{{securitySchema, new[]{"Bearer"}}};
                c.AddSecurityRequirement(securityRequirement);
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