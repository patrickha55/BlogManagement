using BlogManagement.Common.DTOs.UserDTOs;

namespace BlogManagement.Contracts.AuthWithJwt
{
    public interface IAuthManager
    {
        Task<bool> ValidateUserAsync(UserLoginDTO request);
        Task<string> CreateTokenAsync();
    }
}
