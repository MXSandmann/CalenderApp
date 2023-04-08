using ApplicationCore.Options;
using ApplicationCore.Providers.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApplicationCore.Providers
{
    public class JwtProvider : IJwtProvider
    {
        private readonly AuthenticationOptions _authenticationOptions;
        private readonly ILogger<IJwtProvider> _logger;

        public JwtProvider(IOptions<AuthenticationOptions> authenticationOptions, ILogger<IJwtProvider> logger)
        {

            _authenticationOptions = authenticationOptions.Value;
            _logger = logger;
        }

        public string CreateToken(IEnumerable<Claim> claims)
        {
            var key = _authenticationOptions.SecretKey;
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            {
                KeyId = _authenticationOptions.KeyId
            };

            var handler = new JwtSecurityTokenHandler();
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);
            var identity = new ClaimsIdentity(claims);
            var token = handler.CreateJwtSecurityToken(subject: identity,
                signingCredentials: signingCredentials,
                issuer: _authenticationOptions.Issuer);

            _logger.LogInformation("--> Audience: {value}", JsonConvert.SerializeObject(_authenticationOptions.Audience));

            token.Payload["aud"] = _authenticationOptions.Audience;
            return handler.WriteToken(token);
        }
    }
}
