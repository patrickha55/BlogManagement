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
using BlogManagement.Common.Models;

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

        public async Task<List<AuthorAdminIndexVM>> GetAuthorAdminIndexVMAsync(string token, PagingRequest pagingRequest = null)
        {
            List<AuthorAdminIndexVM> authorVMs;

            try
            {
                pagingRequest ??= new PagingRequest();

                var request = new HttpRequestMessage(HttpMethod.Get, $"users/users-admin-page?PageNumber={pagingRequest.PageNumber}&PageSize={pagingRequest.PageSize}");
                var client = _clientFactory.CreateClient(Constants.HttpClientName);

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(token);

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await using var responseStream = await response.Content.ReadAsStreamAsync();
                    authorVMs = await JsonSerializer.DeserializeAsync<List<AuthorAdminIndexVM>>(responseStream, _options);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAuthorAdminIndexVMAsync));
                throw;
            }

            return authorVMs;
        }

        public async Task<AuthorDetailVM> GetAuthorDetailVMAsync(long id)
        {
            AuthorDetailVM userVM;

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"users/details/{id}");
                var client = _clientFactory.CreateClient(Constants.HttpClientName);
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await using var reponseStream = await response.Content.ReadAsStreamAsync();
                    userVM = await JsonSerializer.DeserializeAsync<AuthorDetailVM>(reponseStream, _options);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAuthorDetailVMAsync));
                throw;
            }

            return userVM;
        }

        public async Task<List<AuthorVM>> FindAuthorVMsAsync(string keyword)
        {
            List<AuthorVM> userVM;

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"users/?keyword={keyword}");
                var client = _clientFactory.CreateClient(Constants.HttpClientName);
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await using var reponseStream = await response.Content.ReadAsStreamAsync();
                    userVM = await JsonSerializer.DeserializeAsync<List<AuthorVM>>(reponseStream, _options);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(FindAuthorVMsAsync));
                throw;
            }

            return userVM;
        }

        public async Task<bool> EditUserStatusesAsync(string token, long id, AuthorDetailVM request)
        {
            try
            {
                var client = _clientFactory.CreateClient(Constants.HttpClientName);

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(token);

                var userJson = new StringContent(
                    JsonSerializer.Serialize(request, _options),
                    encoding: Encoding.UTF8,
                    "application/json"
                );

                using var httpResponse = await client.PutAsync($"users/status/{id}", userJson);

                httpResponse.EnsureSuccessStatusCode();

                return httpResponse.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(EditUserStatusesAsync));
                throw;
            }
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
