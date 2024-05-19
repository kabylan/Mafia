using Mafia.Application.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace Mafia.WebApi
{
    internal static class DependencyInjection
    {
        internal static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MyProject API",
                    Description = "MyProject"
                });
                options.DocInclusionPredicate((docName, description) => true);

                // options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme()
                // "bearerAuth" -> "oauth2"
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                // Add this filter as well.
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            return services;
        }

        internal static IServiceCollection AddJwtConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication().AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            })
            .AddCookie(options => //CookieAuthenticationOptions
            {
                options.LoginPath = new PathString("/Account/Login");
            });

            return services;
        }
    }
}