using System.Security.Claims;
using BlazorServer.Data;
using BlazorWasm.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.AspNetCore.Routing;

internal static class IdentityComponentsEndpointRouteBuilderExtensions
{
    // These endpoints are required by the Identity Razor components defined in the /Components/Account/Pages directory of this project
    // and for cookie authentication by BlazorWasm project.
    public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var routeGroup = endpoints.MapGroup("");

        // provide an endpoint to clear the cookie for logout
        //
        // For more information on the logout endpoint and antiforgery, see:
        // https://learn.microsoft.com/aspnet/core/blazor/security/webassembly/standalone-with-identity#antiforgery-support
        routeGroup.MapPost("/logout", async (
            [FromServices] SignInManager<ApplicationUser> signInManager,
            [FromBody] object empty) =>
        {
            if (empty is null)
                return Results.Unauthorized();

            await signInManager.SignOutAsync();
            return Results.Ok();
        }).RequireAuthorization();

        var manageGroup = routeGroup.MapGroup("/manage").RequireAuthorization();

        // provide an endpoint for user roles
        manageGroup.MapGet("/roles", (ClaimsPrincipal user) =>
        {
            if (user.Identity?.IsAuthenticated is null or false)
                return Results.Unauthorized();

            var identity = (ClaimsIdentity)user.Identity;
            var roles = identity.FindAll(identity.RoleClaimType).Select(c =>
                new RoleClaim
                {
                    Issuer = c.Issuer,
                    OriginalIssuer = c.OriginalIssuer,
                    Type = c.Type,
                    Value = c.Value,
                    ValueType = c.ValueType
                });

            return TypedResults.Json(roles);
        });

        var accountGroup = routeGroup.MapGroup("/account");

        accountGroup.MapPost("/logout", async (
            ClaimsPrincipal user,
            [FromServices] SignInManager<ApplicationUser> signInManager,
            [FromForm] string returnUrl) =>
        {
            await signInManager.SignOutAsync();
            return TypedResults.LocalRedirect($"~/{returnUrl}");
        });

        return routeGroup;
    }
}
