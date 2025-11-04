using MamConnect.Domain.Entities;

namespace MamConnect.Api.Services;

/// <summary>
/// Provides access to the authenticated user associated with the current HTTP context.
/// </summary>
public interface ICurrentUserContext
{
    /// <summary>
    /// Tries to retrieve the authenticated user for the current request.
    /// </summary>
    /// <param name="currentUser">The authenticated user when available.</param>
    /// <returns>True when the current request includes an authenticated user.</returns>
    bool TryGetCurrentUser(out CurrentUser? currentUser);
}

/// <summary>
/// Represents the authenticated user for the current HTTP request.
/// </summary>
public sealed class CurrentUser
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CurrentUser"/> class.
    /// </summary>
    /// <param name="userId">The identifier of the authenticated user.</param>
    /// <param name="role">The role associated with the authenticated user.</param>
    public CurrentUser(int userId, UserRole role)
    {
        UserId = userId;
        Role = role;
    }

    /// <summary>
    /// Gets the identifier of the authenticated user.
    /// </summary>
    public int UserId { get; }

    /// <summary>
    /// Gets the role associated with the authenticated user.
    /// </summary>
    public UserRole Role { get; }
}
