namespace Calo.Feature.Users.Services;

public interface IPasswordService
{
    public string PreparePasswordHash(string password, string salt);
    public string GenerateSalt();
}
