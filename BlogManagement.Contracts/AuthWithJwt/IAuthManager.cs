using BlogManagement.Common.DTOs.UserDTOs;

namespace BlogManagement.Contracts.AuthWithJwt
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuthManager
    {
        /// <summary>
        /// This method validates a user when logging in the api.
        /// </summary>
        /// <param name="request">User's info</param>
        /// <returns>Return true if success, else return false</returns>
        Task<bool> ValidateUserAsync(UserLoginDTO request);

        /// <summary>
        /// This method creates a JWT token for the current signed in user.
        /// </summary>
        /// <returns>A JWT token as string</returns>
        Task<string> CreateTokenAsync();

        /// <summary>
        /// This method validates the JWT token sent to the api.
        /// </summary>
        /// <param name="token">Token to check</param>
        /// <returns>Return true if the token is valid, else return false</returns>
        Task<bool> VerifyToken(string token);
    }
}
