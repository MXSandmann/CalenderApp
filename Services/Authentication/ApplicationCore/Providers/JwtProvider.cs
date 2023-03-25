using ApplicationCore.Options;
using ApplicationCore.Providers.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApplicationCore.Providers
{
    public class JwtProvider : IJwtProvider
    {
        private readonly AuthenticationOptions _authenticationOptions;

        public JwtProvider(IOptions<AuthenticationOptions> authenticationOptions)
        {
            
            _authenticationOptions = authenticationOptions.Value;
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
                issuer: _authenticationOptions.Issuer,
                audience: _authenticationOptions.Audience);
            return handler.WriteToken(token);
        }
    }
}
