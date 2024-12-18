using Faactory.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable IDE0130
namespace Microsoft.AspNetCore.Authentication;
#pragma warning restore IDE0130

public static class SecretKeyAuthenticationBuilderExtensions
{
    /// <summary>
    /// Adds secret key authentication.
    /// </summary>
    /// <param name="authenticationScheme">The authentication scheme.</param>
    /// <param name="configure">Action used to configure the secret key authentication options.</param>
    /// <returns>The <see cref="AuthenticationBuilder"/>.</returns>
    public static AuthenticationBuilder AddSecretKey( this AuthenticationBuilder builder, string authenticationScheme, Action<SecretKeyAuthenticationOptions> configure )
    {
        builder.AddScheme<SecretKeyAuthenticationOptions, SecretKeyAuthenticationHandler>( authenticationScheme, null );

        /*
        For some reason, the configure method does not work with the above AddScheme method.
        This is a workaround to allow the caller to configure the options.
        */
        builder.Services.Configure( configure );

        return builder;
    }

    /// <summary>
    /// Adds secret key authentication. The default scheme is specified by <see cref="SecretKeyDefaults"/>.
    /// </summary>
    /// <param name="configure">Action used to configure the secret key authentication options.</param>
    /// <returns>The <see cref="AuthenticationBuilder"/>.</returns>
    public static AuthenticationBuilder AddSecretKey( this AuthenticationBuilder builder, Action<SecretKeyAuthenticationOptions> configure )
        => AddSecretKey( builder, SecretKeyDefaults.AuthenticationScheme, configure );
}
