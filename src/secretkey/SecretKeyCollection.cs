using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Faactory.AspNetCore.Authentication;

/// <summary>
/// A collection of secret keys to be used for authentication. The key is the secret key and the value is the key identifier.
/// </summary>
public sealed class SecretKeyCollection : IEnumerable<KeyValuePair<string, string>>
{
    private readonly Dictionary<string, string> _secretKeys = [];

    /// <summary>
    /// Gets the number of secret keys in the collection.
    /// </summary>
    public int Count => _secretKeys.Count;

    /// <summary>
    /// Adds a secret key to the collection.
    /// </summary>
    /// <param name="keyIdentifier">The key identifier.</param>
    /// <param name="secretKey">The secret key.</param>
    public void Add( string keyIdentifier, string secretKey )
        => _secretKeys.Add( secretKey, keyIdentifier );

    /// <summary>
    /// Adds multiple secret keys to the collection from a dictionary. Ensure that the dictionary key is the secret key and the value is the key identifier.
    /// </summary>
    /// <param name="dictionary">A dictionary of (secret, id) keys to add to the collection.</param>
    public void Add( IEnumerable<KeyValuePair<string, string>> dictionary )
    {
        foreach( var secretKey in dictionary )
        {
            _secretKeys.Add( secretKey.Key, secretKey.Value );
        }
    }

    /// <summary>
    /// Attempts to retrieve the key identifier for a given secret key.
    /// </summary>
    /// <param name="secretKey">The secret key to search for.</param>
    /// <param name="keyIdentifier">The key identifier if the secret key is found.</param>
    /// <returns><c>true</c> if the secret key is found; <c>false</c> otherwise.</returns>
    public bool TryGetValue( string secretKey, [NotNullWhen( true )] out string? keyIdentifier )
        => _secretKeys.TryGetValue( secretKey, out keyIdentifier );

    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        => _secretKeys.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}
