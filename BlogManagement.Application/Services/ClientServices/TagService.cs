using BlogManagement.Common.Common;
using BlogManagement.Common.Models.TagVMs;
using BlogManagement.Contracts.Services.ClientServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BlogManagement.Application.Services.ClientServices
{
    public class TagService : ITagService
    {
        private readonly ILogger<TagService> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions _options;

        public TagService(
            ILogger<TagService> logger,
            IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _clientFactory = clientFactory;
        }

        public async Task<Paginated<TagVM>> GetTagVMsAsync(int pageNumber = 1, int pageSize = 10)
        {
            //PaginatedList<TagVM> tagVMs;

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"tags?PageNumber={pageNumber}&PageSize={pageSize}");
                var client = _clientFactory.CreateClient(Constants.HttpClientName);
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var tagVMs = JsonConvert.DeserializeObject<Paginated<TagVM>>(responseString);

                    return tagVMs;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetTagVMsAsync));
                throw;
            }

            return null;
        }

        public async Task<IEnumerable<TagVM>> GetAllIdAndNameWithoutPagingAsync()
        {
            IEnumerable<TagVM> tagVMs;

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "tags/tags-id-title");
                var client = _clientFactory.CreateClient(Constants.HttpClientName);
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await using var responseStream = await response.Content.ReadAsStreamAsync();
                    tagVMs = await JsonSerializer.DeserializeAsync<IEnumerable<TagVM>>(responseStream, _options);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAllIdAndNameWithoutPagingAsync));
                throw;
            }

            return tagVMs;
        }

        public async Task<TagVM> GetTagVMAsync(string token, long id)
        {
            TagVM tagVM;

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"tags/{id}");
                var client = _clientFactory.CreateClient(Constants.HttpClientName);

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(token);
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await using var responseStream = await response.Content.ReadAsStreamAsync();
                    tagVM = await JsonSerializer.DeserializeAsync<TagVM>(responseStream, _options);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetTagVMAsync));
                throw;
            }

            return tagVM;
        }

        public async Task<TagEditVM> GetTagEditVMsAsync(string token, long id)
        {
            TagEditVM tagEditVM;

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"tags/tag-for-edit/{id}");
                var client = _clientFactory.CreateClient(Constants.HttpClientName);

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(token);
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await using var responseStream = await response.Content.ReadAsStreamAsync();
                    tagEditVM = await JsonSerializer.DeserializeAsync<TagEditVM>(responseStream, _options);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetTagEditVMsAsync));
                throw;
            }

            return tagEditVM;
        }

        public async Task<bool> CreateTagAsync(string token, TagCreateVM request)
        {
            try
            {
                var client = _clientFactory.CreateClient(Constants.HttpClientName);

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(token);
                var tagJson = new StringContent(
                    JsonSerializer.Serialize(request, _options),
                    encoding: Encoding.UTF8,
                    "application/json"
                );

                using var httpResponse = await client.PostAsync("tags/", tagJson);

                httpResponse.EnsureSuccessStatusCode();

                return httpResponse.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(CreateTagAsync));
                throw;
            }
        }

        public async Task UpdateTagAsync(string token, long id, TagEditVM request)
        {
            try
            {
                var client = _clientFactory.CreateClient(Constants.HttpClientName);

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(token);
                var tagJson = new StringContent(
                    JsonSerializer.Serialize(request, _options),
                    encoding: Encoding.UTF8,
                    "application/json"
                );

                using var httpResponse = await client.PutAsync($"tags/{id}", tagJson);

                httpResponse.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(UpdateTagAsync));
                throw;
            }
        }

        public async Task DeleteTagAsync(string token, long id)
        {
            try
            {
                var client = _clientFactory.CreateClient(Constants.HttpClientName);

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(token);
                using var httpResponse = await client.DeleteAsync($"tags/{id}");

                httpResponse.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(DeleteTagAsync));
                throw;
            }
        }
    }
}
