using System.Security.Claims;

namespace WebUI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetIdFromClaims(this ClaimsPrincipal claimsPrincipal)
        {
            var userIdString = claimsPrincipal.Identities.First().Claims.FirstOrDefault(x => x.Type.Equals("Id"))?.Value;
            if(userIdString == null) 
                return Guid.Empty;
            if (Guid.TryParse(userIdString, out var userId))
                return userId;
            return Guid.Empty;
        }
    }
}
