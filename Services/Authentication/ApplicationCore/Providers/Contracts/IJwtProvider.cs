using System.Security.Claims;

namespace ApplicationCore.Providers.Contracts
{
    public interface IJwtProvider
    {
        string CreateToken(IEnumerable<Claim> claims);
    }
}
