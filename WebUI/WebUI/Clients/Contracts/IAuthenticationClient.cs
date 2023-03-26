using System.Security.Claims;
using WebUI.Models.Dtos;

namespace WebUI.Clients.Contracts
{
    public interface IAuthenticationClient
    {
        Task<ClaimsPrincipal> LoginUser(UserDto userDto);
        Task RegisterNewUser(UserRegistrationDto userRegistrationDto);

    }
}
