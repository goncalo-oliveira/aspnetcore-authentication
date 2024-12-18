# Authentication Extensions for AspNetCore

This project contains a set of authentication extensions for AspNetCore.

## Getting Started

To get started, you need to install the NuGet package. You can do this by running the following command:

```bash
dotnet add Faactory.AspNetCore.Authentication.SecretKey
```

## Usage

To use the secret key authentication, you need to set up your authentication pipeline as follows:

```csharp
services.AddAuthentication( SecretKeyDefaults.AuthenticationScheme )
    .AddSecretKey( options =>
    {
        options.SecretKeys.Add( "key-identifier", "secret-key" );
    } );
```

You can make use of environment variables (or any other configuration source) to store one or more secret keys. By doing so, you can make use of a configuration extension method to retrieve all the secret keys at once:

```csharp
services.AddAuthentication( SecretKeyDefaults.AuthenticationScheme )
    .AddSecretKey( options =>
    {
        var secretKeys = configuration.GetAuthSecretKeys();

        options.SecretKeys.Add( secretKeys );
    } );
```

The above code looks for a single `AUTH_SECRET_KEY` configuration key or multiple configuration keys that start with `AUTH_SECRET_KEY_` and adds them to the secret keys collection. If required, a different *configurationKey* can be passed as an argument to the `GetAuthSecretKeys` method.

> [!IMPORTANT]
> Secret keys can be defined either as a single secret or as a collection of secrets.
>
> A single secret key uses the format `<configurationKey>=<secret>`. E.g. `AUTH_SECRET_KEY=secret`.
> If a single secret is defined, it is read as a dictionary with a single key-value pair where the value is an empty string. A single secret key always takes precedence over multiple secrets.
>
> When defining multiple secrets, the configuration key is used as a prefix. The format is `<configurationKey>_<name>=<secret>`. E.g. `AUTH_SECRET_KEY_key1=secret1` and `AUTH_SECRET_KEY_key2=secret2`. The *name* will be used as the `nameIdentifier` claim.
