namespace Calo.Core.Models
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string Pepper { get; set; }
        public JWTSecurity JWTSecurity { get; set; }
    }

    public class JWTSecurity
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiredTime { get; set; }
    }
}
