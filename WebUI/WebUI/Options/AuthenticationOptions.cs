namespace WebUI.Options
{
    public class AuthenticationOptions
    {
        public string SecretKey { get; set; } = string.Empty;
        public string KeyId { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
    }
}
