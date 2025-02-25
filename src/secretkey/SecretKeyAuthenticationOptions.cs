using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Faactory.AspNetCore.Authentication;

public sealed class SecretKeyAuthenticationOptions : AuthenticationSchemeOptions
{
    /// <summary>
    /// A dictionary of secret keys to be used for authentication. The key is the secret key and the value is the user identifier.
    /// </summary>
    public SecretKeyCollection SecretKeys { get; set; } = [];

    /// <summary>
    /// A factory to create custom claims for the authenticated user.
    /// </summary>
    public Func<string, IEnumerable<Claim>>? CustomClaimsFactory { get; set; }
}
