using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using WebUI.Clients.Contracts;
using WebUI.Jwt.Contracts;
using WebUI.Models.Dtos;

namespace WebUI.Clients
{
    public class AuthenticationClient : IAuthenticationClient
    {
        private readonly HttpClient _httpClient;
        private readonly IJwtValidator _jwtValidator;

        public AuthenticationClient(HttpClient httpClient, IJwtValidator jwtValidator)
        {
            _httpClient = httpClient;
            _jwtValidator = jwtValidator;
        }

        public async Task<IEnumerable<GetInstructorDto>> GetAllInstructors()
        {
            var instructors = await _httpClient.GetFromJsonAsync<IEnumerable<GetInstructorDto>>("Instructors");
            if (instructors == null
                || !instructors.Any())
                return Enumerable.Empty<GetInstructorDto>();
            return instructors;
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
                throw new BadHttpRequestException($"Unable to login user {userDto.UserName}");

            var content = await responseMessage.Content.ReadAsStringAsync();
            var claimsPrincipal = _jwtValidator.ValidateToken(content);
            return claimsPrincipal;
        }

        public async Task RegisterNewUser(UserRegistrationDto userRegistrationDto)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync("Register", userRegistrationDto);
            if (!responseMessage.IsSuccessStatusCode)
                throw new BadHttpRequestException($"Unable to register user {userRegistrationDto.UserName}");
            return;
        }
    }
}
