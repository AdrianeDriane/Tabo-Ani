using System.Security.Cryptography;

namespace TaboAni.Api.Infrastructure.Implementations.Service;

public static class OrderNumberGenerator
{
    public const string Prefix = "TA-OR-";
    public const int UniquePartLength = 12;

    private const string AllowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public static string Generate()
    {
        Span<char> suffix = stackalloc char[UniquePartLength];

        for (var index = 0; index < UniquePartLength; index++)
        {
            suffix[index] = AllowedCharacters[RandomNumberGenerator.GetInt32(AllowedCharacters.Length)];
        }

        return $"{Prefix}{new string(suffix)}";
    }
}
