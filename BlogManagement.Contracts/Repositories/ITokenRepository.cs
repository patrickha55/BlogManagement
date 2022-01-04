namespace BlogManagement.Contracts.Repositories
{
    public interface ITokenRepository
    {
        Task<string> GetToken();
        Task SetToken(string token);
    }
}
