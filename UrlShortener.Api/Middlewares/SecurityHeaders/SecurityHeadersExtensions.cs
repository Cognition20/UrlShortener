namespace UrlShortener.Api.Middlewares.SecurityHeaders;

public static class SecurityHeadersExtensions
{
    public static IApplicationBuilder UseSecurityHeadersMiddleware(this IApplicationBuilder app, SecurityHeadersBuilder builder)
    {
        SecurityHeadersPolicy policy = builder.Build();
        
        return app.UseMiddleware<SecurityHeadersMiddleware>(policy);
    }
}