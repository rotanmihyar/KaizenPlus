namespace kaizenplus.Security.Token.Models
{
    public class TokenConfigurations
    {
        public string Secret { get; set; }
        public int Validity { get; set; }
        public int RefreshTokenValidity { get; set; }
    }
}