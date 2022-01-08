using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AutoMapper;
using BlogManagement.Common.Common;
using BlogManagement.Common.DTOs.UserDTOs;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.AuthorVMs;
using BlogManagement.Contracts;
using BlogManagement.Contracts.Services.APIServices;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using X.PagedList;

namespace BlogManagement.Application.Services.APIServices
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole<long>> _roleManager;
        private readonly UserManager<User> _userManager;

        public UserService(
            ILogger<UserService> logger,
            IUnitOfWork unitOfWork, IMapper mapper, RoleManager<IdentityRole<long>> roleManager, UserManager<User> userManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<List<AuthorAdminIndexVM>> GetAuthorAdminIndexVM(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var userVMs = new List<AuthorAdminIndexVM>();

                var pagingRequest = new PagingRequest(pageNumber, pageSize);

                var users = await _unitOfWork.UserRepository.GetAllAsync(pagingRequest);

                userVMs = _mapper.Map<List<AuthorAdminIndexVM>>(users);

                return userVMs;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAuthorAdminIndexVM));
                throw;
            }
        }

        public async Task<AuthorDetailVM> FindAuthorDetailVMAsync(long id)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.FindUserDetailAsync(u => u.Id == id);

                var userVM = _mapper.Map<AuthorDetailVM>(user);

                return userVM;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(FindAuthorDetailVMAsync));
                throw;
            }
        }

        public async Task<List<AuthorVM>> FindAuthorVMsAsync(string keyword, PagingRequest pagingRequest)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                    throw new ArgumentNullException(Constants.InvalidArgument);

                var users = await _unitOfWork.UserRepository.FindUsersAsync(u => (
                    u.UserName.Contains(keyword) || u.FirstName.Contains(keyword) || u.Email.Contains(keyword)) && u.IsPublic == true, pagingRequest);
                
                return _mapper.Map<List<AuthorVM>>(users); ;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(FindAuthorVMsAsync));
                throw;
            }
        }

        public async Task<bool> EditUserStatusesAsync(long id, AuthorDetailVM request)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetAsync(u => u.Id == id);

                if (user == null)
                    return false;

                user.IsEnabled = request.IsEnabled;
                user.IsPublic = request.IsPublic;

                var result = await _unitOfWork.UserRepository.UpdateAsync(user);

                if (result)
                {
                    await _unitOfWork.SaveAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(EditUserStatusesAsync));
                throw;
            }

            return false;
        }

        public async Task<(IdentityResult, User)> RegisterAsync(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userRegisterDTO);

                var result = await _userManager.CreateAsync(user, userRegisterDTO.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    #region Add new user to a user or author role.

                    var isRoleUserExists = await _roleManager.Roles
                        .AnyAsync(r => r.Name == Roles.User);
                    var isRoleAuthorExists = await _roleManager.Roles
                        .AnyAsync(r => r.Name == Roles.Author);

                    if (!isRoleUserExists || !isRoleAuthorExists)
                        throw new KeyNotFoundException(Constants.NoRolesFound + nameof(RegisterAsync));

                    await _userManager.AddToRoleAsync(user, userRegisterDTO.IsAuthor ? Roles.Author : Roles.User);

                    #endregion

                    return (result, user);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(RegisterAsync));
                throw;
            }

            return (null, null);
        }
    }
}
