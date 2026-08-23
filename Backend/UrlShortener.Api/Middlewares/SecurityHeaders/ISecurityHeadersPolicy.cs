namespace UrlShortener.Api.Middlewares.SecurityHeaders;

public interface ISecurityHeadersPolicy
{
    IDictionary<string, string> SetHeaders { get; }
    ISet<string> RemoveHeaders { get; }
}