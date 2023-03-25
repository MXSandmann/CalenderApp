using System.Security.Claims;

namespace WebUI.Jwt.Contracts
{
    public interface IJwtValidator
    {
        ClaimsPrincipal ValidateToken(string token);
    }
}
