using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebUI.Clients.Contracts;
using WebUI.Jwt.Contracts;
using WebUI.Models.Dtos;

namespace WebUI.Clients
{
    public class AuthenticationClient : IAuthenticationClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<IAuthenticationClient> _logger;
        private readonly IConfiguration _configuration;
        private readonly IJwtValidator _jwtValidator;


        public AuthenticationClient(HttpClient httpClient, ILogger<IAuthenticationClient> logger, IConfiguration configuration, IJwtValidator jwtValidator)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
            _jwtValidator = jwtValidator;
        }

        public async Task<ClaimsPrincipal> LoginUser(UserDto userDto)
        {
            var queryParams = new Dictionary<string, string?>
            {
                { "username", userDto.UserName },
                { "password", userDto.Password }
            };

            var url = QueryHelpers.AddQueryString("Login", queryParams);            
            var responseMessage = await _httpClient.GetAsync(url);
            if (!responseMessage.IsSuccessStatusCode)
                throw new BadHttpRequestException($"Unpossible to login user {userDto.UserName}");

            var content = await responseMessage.Content.ReadAsStringAsync();
            var claimsPrincipal = _jwtValidator.ValidateToken(content);
            return claimsPrincipal;
        }

        //private ClaimsPrincipal GetClaimsPrincipal(string token)
        //{
        //    var secret = _configuration.GetValue<string>("Authentication:SecretKey");            
        //    var secretBytes = Encoding.UTF8.GetBytes(secret);
        //    var signingKey = new SymmetricSecurityKey(secretBytes);

        //    var handler = new JwtSecurityTokenHandler();
        //    var jwtToken = handler.ReadJwtToken(token);
        //    ArgumentNullException.ThrowIfNull(jwtToken, nameof(jwtToken));

        //    signingKey.KeyId = "123";

        //    var parameter = new TokenValidationParameters
        //    {
        //        RequireExpirationTime = false,
        //        ValidateIssuer = false,
        //        ValidateAudience = false,
        //        IssuerSigningKey = signingKey,
        //        ValidateIssuerSigningKey = true
        //    };

        //    var claimsPrincipal = handler.ValidateToken(token, parameter, out _);
        //    ArgumentNullException.ThrowIfNull(claimsPrincipal, nameof(claimsPrincipal));
        //    return claimsPrincipal;
        //}

        
    }
}
