using ApplicationCore.Providers.Contracts;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Providers
{
    public class UserNameProvider : IUserNameProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserNameProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private string _userName = "Some user";

        public string GetUserName()
        {
            if(!string.IsNullOrWhiteSpace(_userName))
                return _userName;
            
            try
            {
                _userName = _httpContextAccessor.HttpContext.User.Identities.First().Claims.FirstOrDefault(x => x.Type.Equals("UserName"))?.Value ?? _userName;
            }
            catch
            {
                // not necessary
            }
            return _userName;
        }

        public void SetUserName(string userName) => _userName = userName;
    }
}
