using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.CategoryVMs;
using BlogManagement.Common.Models.PostVMs;
using BlogManagement.Common.Models.TagVMs;
using BlogManagement.Contracts.Services.ClientServices;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlogManagement.Common.DTOs.PostDTOs;

namespace BlogManagement.Application.Services.ClientServices
{
    public class PostService : IPostService
    {
        private readonly ILogger<PostService> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions _options;
        private readonly UserManager<User> _userManager;

        public PostService(ILogger<PostService> logger, IHttpClientFactory clientFactory, UserManager<User> userManager)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _userManager = userManager;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<PostForIndexVM>> GetPostsForIndexVMsAsync(PagingRequest pagingRequest)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"posts?PageNumber={pagingRequest.PageNumber}&PageSize={pagingRequest.PageSize}");
                var client = _clientFactory.CreateClient(Constants.HttpClientName);
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await using var reponseStream = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<List<PostForIndexVM>>(reponseStream, _options);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostsForIndexVMsAsync));
                throw;
            }

            return null;
        }

        public async Task<List<PostForIndexVM>> GetPostsOfAnAuthorAsync(PagingRequest pagingRequest, string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);

                var request = new HttpRequestMessage(HttpMethod.Get, $"posts/posts-of-an-author/{user.Id}?PageNumber={pagingRequest.PageNumber}&PageSize={pagingRequest.PageSize}");
                var client = _clientFactory.CreateClient(Constants.HttpClientName);
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await using var reponseStream = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<List<PostForIndexVM>>(reponseStream, _options);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostsOfAnAuthorAsync));
                throw;
            }

            return null;
        }

        public async Task<List<PostForAdminIndexVM>> GetPostForAdminIndexVMsAsync(PagingRequest pagingRequest)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get,
                    $"posts-admins?PageNumber={pagingRequest.PageNumber}&PageSize={pagingRequest.PageSize}");
                var client = _clientFactory.CreateClient(Constants.HttpClientName);
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await using var reponseStream = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<List<PostForAdminIndexVM>>(reponseStream, _options);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostForAdminIndexVMsAsync));
                throw;
            }

            return null;
        }

        public Task<Post> GetPostAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<PostVM> GetPostVMAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<PostDetailVM> GetPostDetailVMAsync(long id, string userName)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"posts/detail/{id}?userName={userName}");
                var client = _clientFactory.CreateClient(Constants.HttpClientName);
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await using var reponseStream = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<PostDetailVM>(reponseStream, _options);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostDetailVMAsync));
                throw;
            }

            return null;
        }

        public Task<PostEditVM> GetPostEditVMsAsync(string token, long id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreatePostAsync(string token, PostCreateVM request, string userName)
        {
            try
            {
                var client = _clientFactory.CreateClient(Constants.HttpClientName);

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(token);
                var postJson = new StringContent(
                    JsonSerializer.Serialize(request, _options),
                    encoding: Encoding.UTF8,
                    "application/json"
                );

                using var httpResponse = await client.PostAsync($"posts?userName={userName}", postJson);

                httpResponse.EnsureSuccessStatusCode();

                return httpResponse.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(CreatePostAsync));
                throw;
            }
        }

        public async Task<bool> UpdatePostAsync(string token, long id, PostEditVM request)
        {
            try
            {
                var client = _clientFactory.CreateClient(Constants.HttpClientName);

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(token);
                var postJson = new StringContent(
                    JsonSerializer.Serialize(request, _options),
                    encoding: Encoding.UTF8,
                    "application/json"
                );

                using var httpResponse = await client.PutAsync($"posts/{id}", postJson);

                httpResponse.EnsureSuccessStatusCode();

                return httpResponse.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(UpdatePostAsync));
                throw;
            }
        }

        public async Task<bool> DeletePostAsync(string token, long id)
        {
            try
            {
                var client = _clientFactory.CreateClient(Constants.HttpClientName);

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(token);

                using var httpResponse = await client.DeleteAsync($"posts/{id}");

                httpResponse.EnsureSuccessStatusCode();

                return httpResponse.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(DeletePostAsync));
                throw;
            }
        }

        public Task<bool> IsPostExistAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<PostRelatedListOfObjectsDTO> GetSelectListsForPostCreationAsync(string token)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "posts/select-lists-for-posts-creation");
                var client = _clientFactory.CreateClient(Constants.HttpClientName);
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(token);
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await using var reponseStream = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<PostRelatedListOfObjectsDTO>(reponseStream, _options);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostsForIndexVMsAsync));
                throw;
            }

            return null;
        }
    }
}
