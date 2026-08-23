namespace UrlShortener.Api.Middlewares.SecurityHeaders;

public class SecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ISecurityHeadersPolicy _policy;

    public SecurityHeadersMiddleware(RequestDelegate next, ISecurityHeadersPolicy policy)
    {
        _next = next;
        _policy = policy;
    }

    public async Task Invoke(HttpContext context)
    {
        context.Response.OnStarting(() =>
        {
            foreach (var header in _policy.SetHeaders)
            {
                context.Response.Headers[header.Key] = header.Value;
            }

            foreach (var header in _policy.RemoveHeaders)
            {
                context.Response.Headers.Remove(header);
            }

            return Task.CompletedTask;
        });

        await _next(context);
    }
}