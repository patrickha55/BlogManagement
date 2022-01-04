using BlogManagement.Common.Common;
using BlogManagement.Common.DTOs.UserDTOs;
using BlogManagement.Common.Models.AuthorVMs;
using BlogManagement.Contracts.Services.ClientServices;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogManagement.Application.Services.ClientServices
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions _options;

        public UserService(ILogger<UserService> logger,
            IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<AuthorAdminIndexVM>> GetAuthorAdminIndexVM(int pageNumber = 1, int pageSize = 10)
        {
            List<AuthorAdminIndexVM> authorVMs;

            try
            {
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAuthorAdminIndexVM));
                throw;
            }

            return null;
        }

        public Task<AuthorDetailVM> FindAuthorDetailVMAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AuthorVM>> FindAuthorVMsAsync(string keyword)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditUserStatusesAsync(long id, AuthorDetailVM request)
        {
            throw new NotImplementedException();
        }

        public async Task<(IdentityResult, User)> RegisterAsync(UserRegisterDTO userRegisterDTO)
        {
            var client = _clientFactory.CreateClient(Constants.HttpClientName);
            var userRegisterJson = new StringContent(
                JsonSerializer.Serialize(userRegisterDTO, _options),
                encoding: Encoding.UTF8,
                "application/json"
            );

            using var httpResponse = await client.PostAsync("auth/register", userRegisterJson);

            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content.ReadFromJsonAsync<(IdentityResult, User)>();
        }

        public async Task<Token> LoginAsync(UserLoginDTO userLoginDTO)
        {
            var client = _clientFactory.CreateClient(Constants.HttpClientName);
            var userLoginJson = new StringContent(
                JsonSerializer.Serialize(userLoginDTO, _options),
                encoding: Encoding.UTF8,
                "application/json"
            );

            using var httpResponse = await client.PostAsync("auth/login", userLoginJson);

            httpResponse.EnsureSuccessStatusCode();

            var token = await JsonSerializer.DeserializeAsync<Token>(await httpResponse.Content.ReadAsStreamAsync(), _options);

            return token;
        }
    }
}
