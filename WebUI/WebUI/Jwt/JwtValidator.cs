using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebUI.Jwt.Contracts;
using WebUI.Options;

namespace WebUI.Jwt
{
    public class JwtValidator : IJwtValidator
    {
        private readonly AuthenticationOptions _authenticationOptions;

        public JwtValidator(IOptions<AuthenticationOptions> authenticationOptions)
        {
            
            _authenticationOptions = authenticationOptions.Value;
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var secret = _authenticationOptions.SecretKey;
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            var signingKey = new SymmetricSecurityKey(secretBytes);

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            ArgumentNullException.ThrowIfNull(jwtToken, nameof(jwtToken));

            signingKey.KeyId = _authenticationOptions.KeyId;

            var parameter = new TokenValidationParameters
            {
                RequireExpirationTime = false,
                ValidateIssuer = true,
                ValidIssuer = _authenticationOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = _authenticationOptions.Audience,
                IssuerSigningKey = signingKey,
                ValidateIssuerSigningKey = true
            };

            var claimsPrincipal = handler.ValidateToken(token, parameter, out _);
            ArgumentNullException.ThrowIfNull(claimsPrincipal, nameof(claimsPrincipal));
            return claimsPrincipal;
        }
    }
}
