using System.Security.Cryptography;
using TaboAni.Api.Application.Interfaces.Security;

namespace TaboAni.Api.Application.Security;

public sealed class Pbkdf2PasswordHasher : IPasswordHasher
{
    private const string AlgorithmName = "PBKDF2";
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int IterationCount = 100_000;

    public string HashPassword(string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            IterationCount,
            HashAlgorithmName.SHA256,
            KeySize);

        return $"{AlgorithmName}${IterationCount}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            return false;
        }

        var parts = passwordHash.Split('$', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (parts.Length != 4 ||
            !string.Equals(parts[0], AlgorithmName, StringComparison.Ordinal) ||
            !int.TryParse(parts[1], out var iterations))
        {
            return false;
        }

        try
        {
            var salt = Convert.FromBase64String(parts[2]);
            var expectedHash = Convert.FromBase64String(parts[3]);
            var actualHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256,
                expectedHash.Length);

            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
