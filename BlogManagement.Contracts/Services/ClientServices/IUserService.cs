using BlogManagement.Common.Common;
using BlogManagement.Common.DTOs.UserDTOs;
using BlogManagement.Common.Models.AuthorVMs;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace BlogManagement.Contracts.Services.ClientServices
{
    public interface IUserService
    {
        Task<List<AuthorAdminIndexVM>> GetAuthorAdminIndexVM(int pageNumber = 1, int pageSize = 10);
        Task<AuthorDetailVM> FindAuthorDetailVMAsync(long id);
        Task<List<AuthorVM>> FindAuthorVMsAsync(string keyword);
        Task<bool> EditUserStatusesAsync(long id, AuthorDetailVM request);
        Task<(IdentityResult, User)> RegisterAsync(UserRegisterDTO userRegisterDTO);
        Task<Token> LoginAsync(UserLoginDTO userLoginDTO);
    }
}
