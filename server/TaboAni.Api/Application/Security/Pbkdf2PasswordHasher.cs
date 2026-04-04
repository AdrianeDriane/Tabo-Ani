using System.Security.Cryptography;
using TaboAni.Api.Application.Interfaces.Security;

namespace TaboAni.Api.Application.Security;

public sealed class Pbkdf2PasswordHasher : IPasswordHasher
{
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

        return $"PBKDF2${IterationCount}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
    }
}
