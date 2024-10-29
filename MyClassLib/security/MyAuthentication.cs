using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace MyClassLib.security;

public class MyAuthentication(RequestDelegate next)
{
    private readonly string API_KEY = "MY_API_KEY";

    public async Task InvokeAsync(HttpContext httpContext)
    {
        if (!httpContext.Request.Headers.TryGetValue("Authorization", out var value) || value != API_KEY)
        {
            httpContext.Response.StatusCode = 401;
            await httpContext.Response.WriteAsync("Unauthorized user!!");
            return;
        }
        await next(httpContext);
    }
}