using Microsoft.AspNetCore.Http;

namespace MyClassLib.security;

public class MyAuthorization(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity is { IsAuthenticated: false })
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Who are you?");
            return;
        }

        if (!context.User.IsInRole("Admin"))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("You are not an administrator.");
            return;
        }
        await next(context);
    }
}