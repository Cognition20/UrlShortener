namespace UrlShortener.Api.Middlewares.SecurityHeaders;

public static class SecurityHeaderValues
{
    public const string XContentTypeOptions = "nosniff";
    public const string ReferrerPolicy = "no-referrer";
    public const string XFrameOptions = "DENY";
    public const string ContentSecurityPolicy = "frame-ancestors 'none';";

}