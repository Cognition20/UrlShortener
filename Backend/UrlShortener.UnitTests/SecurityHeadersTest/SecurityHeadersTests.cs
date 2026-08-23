using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;


namespace UrlShortener.UnitTests.SecurityHeadersTest;

public class SecurityHeadersTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public SecurityHeadersTests(WebApplicationFactory<Program> factory)
    {
        var client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
            });
        });

        _client = client.CreateClient();
    }

    [Fact]
    public async Task Response_ShouldContainSecurityHeaders()
    {
        var response = await _client.GetAsync("/health");

        Assert.Equal(
            "nosniff",
            response.Headers.GetValues("X-Content-Type-Options").Single());

        Assert.Equal(
            "DENY",
            response.Headers.GetValues("X-Frame-Options").Single());

        Assert.Equal(
            "no-referrer",
            response.Headers.GetValues("Referrer-Policy").Single());
    }
}