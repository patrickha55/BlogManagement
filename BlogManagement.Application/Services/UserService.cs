using AutoMapper;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.AuthorVMs;
using BlogManagement.Contracts;
using BlogManagement.Contracts.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagement.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(
            ILogger<UserService> logger,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<AuthorAdminIndexVM>> GetAuthorAdminIndexVM(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var userVMs = new List<AuthorAdminIndexVM>();

                var pagingRequest = new PagingRequest { PageNumber = pageNumber, PageSize = pageSize };

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
                if (id <= 0)
                    throw new ArgumentException(Constants.InvalidArgument);

                var user = await _unitOfWork.UserRepository.FindUserDetailAsync(u => u.Id == id);

                if (user is null)
                    throw new ArgumentException(Constants.InvalidArgument);

                var userVM = _mapper.Map<AuthorDetailVM>(user);

                return userVM;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(FindAuthorDetailVMAsync));
                throw;
            }
        }

        public async Task<List<AuthorVM>> FindAuthorVMsAsync(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                    throw new ArgumentNullException(Constants.InvalidArgument);

                var users = await _unitOfWork.UserRepository.FindUsersAsync(u => (
                    u.UserName.Contains(keyword) || u.FirstName.Contains(keyword) || u.Email.Contains(keyword)) && u.IsPublic == true);

                return _mapper.Map<List<AuthorVM>>(users);
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
                if (id <= 0)
                    throw new ArgumentException(Constants.InvalidArgument);

                var user = await _unitOfWork.UserRepository.GetAsync(u => u.Id == id);

                if (user == null)
                    throw new ArgumentException(Constants.InvalidArgument);

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
    }
}
