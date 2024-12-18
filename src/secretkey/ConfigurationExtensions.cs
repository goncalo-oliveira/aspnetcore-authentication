using Faactory.AspNetCore.Authentication;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.Configuration;
#pragma warning restore IDE0130

public static class SecretKeyAuthenticationConfigurationExtensions
{
    /// <summary>
    /// Retrieves all secret keys from the configuration. The default configuration key is specified by <see cref="SecretKeyDefaults"/>.
    /// </summary>
    /// <param name="configurationKey">The configuration key to use for retrieving secret keys.</param>
    /// <returns>A read-only dictionary containing the secret keys.</returns>
    public static IReadOnlyDictionary<string, string> GetAuthSecretKeys( this IConfiguration configuration, string configurationKey = SecretKeyDefaults.ConfigurationKey )
    {
        /*
        Secret keys can be defined either as a single secret or as a collection of secrets.

        Single secret uses the following key format: "<configurationKey>"
        If a single secret is defined, it is returned as a dictionary with a single key-value pair.
        The name is an empty string.

        To define multpiple secrets, the configuration key is used as a prefix.
        The key format for multiple secrets is: "<configurationKey>_<name>".
        */

        // attempt to retrieve a single secret
        var secret = configuration[configurationKey];

        if ( !string.IsNullOrWhiteSpace( secret ) )
        {
            return new Dictionary<string, string> { [secret] = string.Empty }
                .AsReadOnly();
        }

        var prefix = $"{configurationKey}_";

        return configuration.AsEnumerable()
            .Where( kvp => kvp.Key.StartsWith( prefix, StringComparison.OrdinalIgnoreCase ) )
            .Where( kvp => !string.IsNullOrWhiteSpace( kvp.Value ) )
            .Select( kvp => (kvp.Value!, kvp.Key[prefix.Length..]) )
            .ToDictionary( kvp => kvp.Item1, kvp => kvp.Item2 )
            .AsReadOnly();
    }
}
