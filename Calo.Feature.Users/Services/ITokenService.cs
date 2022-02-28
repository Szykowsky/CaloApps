namespace Calo.Feature.Users.Services
{
    public interface ITokenService
    {
        public string GetToken(string login, Guid id);
    }
}
