using System;
using System.Security.Claims;
using System.Security.Principal;
using MamConnect.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace MamConnect.Api.Services;

/// <summary>
/// Implements <see cref="ICurrentUserContext"/> using the ASP.NET Core HTTP context.
/// </summary>
public sealed class CurrentUserContext : ICurrentUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="CurrentUserContext"/> class.
    /// </summary>
    /// <param name="httpContextAccessor">Accessor used to retrieve the current HTTP context.</param>
    public CurrentUserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc />
    public bool TryGetCurrentUser(out CurrentUser? currentUser)
    {
        currentUser = null;

        HttpContext? httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            return false;
        }

        ClaimsPrincipal claimsPrincipal = httpContext.User;
        if (claimsPrincipal == null)
        {
            return false;
        }

        IIdentity? identity = claimsPrincipal.Identity;
        if (identity == null || !identity.IsAuthenticated)
        {
            return false;
        }

        Claim? identifierClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
        Claim? roleClaim = claimsPrincipal.FindFirst(ClaimTypes.Role);
        if (identifierClaim == null || roleClaim == null)
        {
            return false;
        }

        bool userIdParsed = int.TryParse(identifierClaim.Value, out int userId);
        bool roleParsed = Enum.TryParse<UserRole>(roleClaim.Value, out UserRole role);
        if (!userIdParsed || !roleParsed)
        {
            return false;
        }

        currentUser = new CurrentUser(userId, role);
        return true;
    }
}
