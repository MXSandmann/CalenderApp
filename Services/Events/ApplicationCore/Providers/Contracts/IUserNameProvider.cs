namespace ApplicationCore.Providers.Contracts
{
    public interface IUserNameProvider
    {
        string GetUserName();
        void SetUserName(string userName);
    }
}
