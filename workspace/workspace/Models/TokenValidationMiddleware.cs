using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace workspace.Models
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var user = await userManager.Users.SingleOrDefaultAsync(u => u.ActiveToken == token);

                if (user == null && !context.Request.Path.Value.Contains("Logout", StringComparison.OrdinalIgnoreCase))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token invalide ou expiré.");
                    return;
                }
            }

            await _next(context);
        }
    }

}

