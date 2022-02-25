namespace CaloApps.Users.Services
{
    public interface ITokenService
    {
        public string GetToken(string login, Guid id);
    }
}
