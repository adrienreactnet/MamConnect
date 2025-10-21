using MamConnect.Domain.Entities;

namespace MamConnect.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user">The user for which to generate the token.</param>
    /// <returns>The encoded JWT token string.</returns>
    string GenerateToken(User user);
}
