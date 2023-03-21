using ApplicationCore.Models.Dtos;
using Microsoft.AspNetCore.WebUtilities;
using WebUI.Clients.Contracts;

namespace WebUI.Clients
{
    public class AuthenticationClient : IAuthenticationClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<IAuthenticationClient> _logger;

        public AuthenticationClient(HttpClient httpClient, ILogger<IAuthenticationClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> LoginUser(UserDto userDto)
        {
            var queryParams = new Dictionary<string, string?>
            {
                { "username", userDto.UserName },
                { "password", userDto.Password }
            };

            //var url = QueryHelpers.AddQueryString("Login", queryParams);
            var url = $"Login?username={userDto.UserName}&password={userDto.Password}";
            _logger.LogInformation("--> send login request: {value}", url);
            var responseMessage = await _httpClient.GetAsync(url);
            if (!responseMessage.IsSuccessStatusCode)
                return string.Empty;
            var content = await responseMessage.Content.ReadAsStringAsync();
            _logger.LogInformation("--> received token: {value}", content);
            return content;
        }
    }
}
