using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace MyClassLib.security;

public class MyAuthentication(RequestDelegate next)
{
    private readonly string API_KEY = "MY_API_KEY";

    public async Task InvokeAsync(HttpContext httpContext)
    {
        Console.WriteLine("Authentication middleware invoked.");
        if (!httpContext.Request.Headers.TryGetValue("Authorization", out var value) || value != API_KEY)
        {
            httpContext.Response.StatusCode = 401;
            Console.WriteLine("User is not authenticated.");
            await httpContext.Response.WriteAsync("Unauthorized user!!");
            return;
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "TestUser"),
            new Claim(ClaimTypes.Role, "Admin")
        };
        var identity = new ClaimsIdentity(claims, "ApiKey");
        httpContext.User = new ClaimsPrincipal(identity);
        await next(httpContext);
    }
}