using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using WebUI.Clients.Contracts;
using WebUI.Jwt.Contracts;
using WebUI.Models.Dtos;
using WebUI.Services.Contracts;

namespace WebUI.Clients
{
    public class AuthenticationClient : IAuthenticationClient
    {
        private readonly HttpClient _httpClient;
        private readonly IJwtValidator _jwtValidator;
        private readonly IPasswordHasher _passwordHasher;


        public AuthenticationClient(HttpClient httpClient, IJwtValidator jwtValidator, IPasswordHasher passwordHasher)
        {
            _httpClient = httpClient;
            _jwtValidator = jwtValidator;
            _passwordHasher = passwordHasher;
        }

        public async Task<ClaimsPrincipal> LoginUser(UserDto userDto)
        {
            var hashedPassword = _passwordHasher.HashPassword(userDto.Password);
            var queryParams = new Dictionary<string, string?>
            {
                { "username", userDto.UserName },
                { "password", hashedPassword }
            };

            var url = QueryHelpers.AddQueryString("Login", queryParams);
            var responseMessage = await _httpClient.GetAsync(url);
            if (!responseMessage.IsSuccessStatusCode)
                throw new BadHttpRequestException($"Unpossible to login user {userDto.UserName}");

            var content = await responseMessage.Content.ReadAsStringAsync();
            var claimsPrincipal = _jwtValidator.ValidateToken(content);
            return claimsPrincipal;
        }
    }
}
