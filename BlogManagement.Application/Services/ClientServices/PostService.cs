using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.PostVMs;
using BlogManagement.Contracts.Services.ClientServices;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

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
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostsForIndexVMsAsync));
                throw;
            }

            return null;
        }

        public Task<List<PostForAdminIndexVM>> GetPostForAdminIndexVMsAsync(int pageNumber = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
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
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostsForIndexVMsAsync));
                throw;
            }

            return null;
        }

        public Task<PostEditVM> GetPostEditVMsAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<PostVM> CreatePostAsync(PostCreateVM request, string userName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePostAsync(long id, PostEditVM request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePostAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsPostExistAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<(SelectList categories, SelectList tags, SelectList posts)> GetSelectListsForPostCreationAsync(long? categoryId = null, IEnumerable<long> tagIds = null, long? postId = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePostViewCountAsync(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
