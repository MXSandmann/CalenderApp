using ApplicationCore.Models.Dtos;

namespace WebUI.Clients.Contracts
{
    public interface IAuthenticationClient
    {
        Task<string> LoginUser(UserDto userDto);
    }
}
