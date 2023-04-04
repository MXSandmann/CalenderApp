using System.Security.Claims;
using WebUI.Models.Enums;

namespace WebUI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetInstructorIdFromClaims(this ClaimsPrincipal claimsPrincipal)
        {
            var claims = claimsPrincipal?.Identities?.FirstOrDefault()?.Claims;
            if (claims == null)
                return Guid.Empty;

            var isInstructor = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value == Role.Instructor.ToString();
            if (!isInstructor)
                return Guid.Empty;

            var userIdString = claims.FirstOrDefault(x => x.Type.Equals("Id"))?.Value;
            if(userIdString == null) 
                return Guid.Empty;

            if (Guid.TryParse(userIdString, out var userId))
                return userId;
            return Guid.Empty;
        }
    }
}
