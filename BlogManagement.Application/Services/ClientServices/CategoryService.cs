using BlogManagement.Common.Common;
using BlogManagement.Common.Models.CategoryVMs;
using BlogManagement.Contracts.Services.ClientServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogManagement.Application.Services.ClientServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ILogger<CategoryService> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions _options;

        public CategoryService(ILogger<CategoryService> logger,
            IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<CategoryVM>> GetCategoryVMsAsync(int pageNumber = 1, int pageSize = 10)
        {
            List<CategoryVM> categoryVM;

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"categories?PageNumber={pageNumber}&PageSize={pageSize}");
                var client = _clientFactory.CreateClient(Constants.HttpClientName);
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await using var reponseStream = await response.Content.ReadAsStreamAsync();
                    categoryVM = await JsonSerializer.DeserializeAsync<List<CategoryVM>>(reponseStream, _options);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetCategoryVMsAsync));
                throw;
            }

            return categoryVM;
        }

        public async Task<CategoryEditVM> GetCategoryEditVMsAsync(string token, long id)
        {
            CategoryEditVM categoryEditVM;

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"categories/category-for-edit/{id}");
                var client = _clientFactory.CreateClient(Constants.HttpClientName);

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(token);
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await using var reponseStream = await response.Content.ReadAsStreamAsync();
                    categoryEditVM = await JsonSerializer.DeserializeAsync<CategoryEditVM>(reponseStream, _options);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetCategoryVMAsync));
                throw;
            }

            return categoryEditVM;
        }

        public async Task<CategoryVM> GetCategoryVMAsync(string token, long id)
        {
            CategoryVM categoryVM;

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"categories/{id}");
                var client = _clientFactory.CreateClient(Constants.HttpClientName);

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(token);
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await using var reponseStream = await response.Content.ReadAsStreamAsync();
                    categoryVM = await JsonSerializer.DeserializeAsync<CategoryVM>(reponseStream, _options);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetCategoryVMAsync));
                throw;
            }

            return categoryVM;
        }

        public async Task<bool> CreateCategoryAsync(string token, CategoryCreateVM request)
        {
            try
            {
                var client = _clientFactory.CreateClient(Constants.HttpClientName);

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(token);
                var categoryJson = new StringContent(
                    JsonSerializer.Serialize(request, _options),
                    encoding: Encoding.UTF8,
                    "application/json"
                );

                using var httpResponse = await client.PostAsync("categories/", categoryJson);

                httpResponse.EnsureSuccessStatusCode();

                return httpResponse.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(CreateCategoryAsync));
                throw;
            }
        }

        public async Task<bool> UpdateCategoryAsync(string token, long id, CategoryEditVM request)
        {
            try
            {
                var client = _clientFactory.CreateClient(Constants.HttpClientName);

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(token);
                var categoryJson = new StringContent(
                    JsonSerializer.Serialize(request, _options),
                    encoding: Encoding.UTF8,
                    "application/json"
                );

                using var httpResponse = await client.PutAsync($"categories/{id}", categoryJson);

                httpResponse.EnsureSuccessStatusCode();

                return httpResponse.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(UpdateCategoryAsync));
                throw;
            }
        }

        public async Task<bool> DeleteCategoryAsync(string token, long id)
        {
            try
            {
                var client = _clientFactory.CreateClient(Constants.HttpClientName);

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(token);

                using var httpResponse = await client.DeleteAsync($"categories/{id}");

                httpResponse.EnsureSuccessStatusCode();

                return httpResponse.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(DeleteCategoryAsync));
                throw;
            }
        }

        public async Task<IEnumerable<CategoryVM>> GetCategoriesForSelectListAsync(string token, long? parentId = null)
        {
            IEnumerable<CategoryVM> categoryVMs = null;

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"categories/categories-select-list?parentId={parentId}");
                var client = _clientFactory.CreateClient(Constants.HttpClientName);

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(token);

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await using var reponseStream = await response.Content.ReadAsStreamAsync();
                    categoryVMs = await JsonSerializer.DeserializeAsync<IEnumerable<CategoryVM>>(reponseStream, _options);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetCategoryVMAsync));
                throw;
            }

            return categoryVMs;
        }

        public async Task<IEnumerable<CategoryVM>> GetAllIdAndNameWithoutPagingAsync()
        {
            IEnumerable<CategoryVM> categoryVMs;

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "categories/categories-id-name");
                var client = _clientFactory.CreateClient(Constants.HttpClientName);
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await using var reponseStream = await response.Content.ReadAsStreamAsync();
                    categoryVMs = await JsonSerializer.DeserializeAsync<IEnumerable<CategoryVM>>(reponseStream, _options);
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

            return categoryVMs;
        }
    }
}
