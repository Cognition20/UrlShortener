using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace UrlShortener.UnitTests.SecurityHeadersTest;

public class HstsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public HstsTests(WebApplicationFactory<Program> factory)
    {
        var client = factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Test");

            builder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
            });

            builder.ConfigureTestServices(services =>
            {
                services.Configure<HstsOptions>(options =>
                {
                    options.ExcludedHosts.Clear();
                });
            });
        });

        _client = client.CreateDefaultClient(new Uri("https://localhost"));
    }

    [Fact]
    public async Task Response_ShouldContain_Hsts_Header()
    {
        var response = await _client.GetAsync("/health");
        Assert.True(response.Headers.Contains("Strict-Transport-Security"));
    }
}