namespace TaboAni.Api.Application.Interfaces.Security;

public interface IPasswordHasher
{
    string HashPassword(string password);
}
