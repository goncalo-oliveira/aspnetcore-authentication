using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Faactory.AspNetCore.Authentication;

public sealed class SecretKeyAuthenticationHandler(
    IOptionsMonitor<SecretKeyAuthenticationOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder
)
: AuthenticationHandler<SecretKeyAuthenticationOptions>( options, logger, encoder )
{
    private readonly IOptionsMonitor<SecretKeyAuthenticationOptions> options = options;

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var secretKeys = options.CurrentValue.SecretKeys;

        if ( secretKeys.Count == 0 )
        {
            return Task.FromResult( AuthenticateResult.NoResult() );
        }

        if ( !Request.Headers.TryGetValue( "Authorization", out Microsoft.Extensions.Primitives.StringValues value ) )
        {
            return Task.FromResult( AuthenticateResult.Fail( "Missing Authorization Header" ) );
        }

        var authHeader = value.ToString();
        if ( !authHeader.StartsWith( "Bearer ", StringComparison.OrdinalIgnoreCase ) )
        {
            return Task.FromResult( AuthenticateResult.Fail( "Invalid Authorization Header" ) );
        }

        var token = authHeader["Bearer ".Length..].Trim();

        if ( !secretKeys.TryGetValue( token, out var nameIdentifier ) )
        {
            return Task.FromResult( AuthenticateResult.Fail( "Invalid Token" ) );
        }

        var claims = options.CurrentValue.CustomClaimsFactory?.Invoke( nameIdentifier ).ToList() ?? [];

        claims.Add( new Claim( ClaimTypes.NameIdentifier, nameIdentifier ) );

        var principal = new ClaimsPrincipal(
            new ClaimsIdentity( claims, Scheme.Name )
        );
        var ticket = new AuthenticationTicket( principal, Scheme.Name );

        return Task.FromResult( AuthenticateResult.Success( ticket ) );
    }
}
