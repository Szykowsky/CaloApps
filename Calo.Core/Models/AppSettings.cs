namespace Calo.Core.Models;

public class AppSettings
{
    public string Pepper { get; set; }
    public JWTSecurity JWTSecurity { get; set; }
}

public class JWTSecurity
{
    public string AccessTokenSecret { get; set; }
    public string RefreshTokenSecret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int AccessTokenExpiredTime { get; set; }
    public int RefreshTokenExpiredTime { get; set; }
}
