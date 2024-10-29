using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace MyTests;

public class UnitTest1
{
    [Fact]
public async Task HelloEndpoint_ReturnsMyString()
{
    // Arrange
    var webApp = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.Configure<MySetting>(options => 
                {
                    options.MyString = "Hello, World!";
                });
            });
        });

    var client = webApp.CreateClient();

    // Act
    var response = await client.GetAsync("/hello");

    // Assert
    response.EnsureSuccessStatusCode();
    var content = await response.Content.ReadAsStringAsync();
    Assert.Equal("Hello, World!", content);
}
}