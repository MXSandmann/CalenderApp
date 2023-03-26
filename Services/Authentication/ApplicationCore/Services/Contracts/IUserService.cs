namespace ApplicationCore.Services.Contracts
{
    public interface IUserService
    {
        /// <summary>
        /// Log in the user with provided credentials and return json web token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>token</returns>
        Task<string> LoginUser(string username, string password);
        Task CreateUser(string username, string password);
    }
}
