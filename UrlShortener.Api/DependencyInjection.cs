using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.OpenApi.Models;
using UrlShortener.Api.Common.Mapping;

namespace UrlShortener.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddHsts();
        services.AddHttpsRedirection();
        services.AddMapping();
        services.AddControllers();
        services.AddOpenApi();
        return services;
    }
    private static IServiceCollection AddOpenApi(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "UrlShortener API",
                Version = "v1"
            });

            // Optional: JWT bearer auth in Swagger
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter JWT Bearer token"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }
    
    private static IServiceCollection AddHttpsRedirection(this IServiceCollection services)
    {
        services.AddHttpsRedirection(options =>
        {
            options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
            options.HttpsPort = 7237;
        });
        
        return services;
    }
    
    private static IServiceCollection AddHsts(this IServiceCollection services)
    {
        services.Configure<HstsOptions>(options =>
        {
            options.Preload = true;
            options.MaxAge = TimeSpan.FromDays(365);
            options.IncludeSubDomains = true;
        });
        
        return services;
    }
}