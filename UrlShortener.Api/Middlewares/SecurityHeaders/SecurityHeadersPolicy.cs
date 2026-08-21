namespace UrlShortener.Api.Middlewares.SecurityHeaders;

public class SecurityHeadersPolicy : ISecurityHeadersPolicy
{
    public IDictionary<string, string> SetHeaders { get; }
        = new Dictionary<string, string>();
    
    public ISet<string> RemoveHeaders { get; }
        = new HashSet<string>();
}