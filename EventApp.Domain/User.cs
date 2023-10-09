using Microsoft.AspNetCore.Identity;

namespace Domain;

/// <summary>
/// <inheritdoc cref="IdentityUser"/>
/// </summary>
public class User : IdentityUser { }

/// <summary>
/// Base app user. Predefined in configuration
/// </summary>
public class PreConfiguredUser
{
    /// <summary>
    /// Predefined email
    /// </summary>
    public string Email { get; init; }

    /// <summary>
    /// Predefined password as a plaintext
    /// </summary>
    public string Password { get; init; }
}