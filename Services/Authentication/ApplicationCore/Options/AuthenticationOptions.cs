namespace ApplicationCore.Options
{
    public class AuthenticationOptions
    {
        public string SecretKey { get; set; } = string.Empty;
        public string KeyId { get; set; } = string.Empty;
        public List<string> Audience { get; set; } = null!;
        public string Issuer { get; set; } = string.Empty;
    }
}
