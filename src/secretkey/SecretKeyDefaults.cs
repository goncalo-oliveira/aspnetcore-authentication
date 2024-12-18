namespace Faactory.AspNetCore.Authentication;

public static class SecretKeyDefaults
{
    /// <summary>
    /// Default value for AuthenticationScheme property in the <see cref="SecretKeyAuthenticationOptions"/>.
    /// </summary>
    public const string AuthenticationScheme = "SecretKey";

    /// <summary>
    /// Default value for secret key configuration keys.
    /// </summary>
    public const string ConfigurationKey = "AUTH_SECRET_KEY";
}
