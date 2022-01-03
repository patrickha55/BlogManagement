using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlogManagement.Common.Common;
using BlogManagement.Contracts.Repositories;
using Microsoft.Extensions.Logging;
using MyApp.Repository.ApiClient;

namespace BlogManagement.Application.ApiClient
{
    public class WebApiExecuter : IWebApiExecuter
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;
        private readonly ITokenRepository _tokenRepository;
        private readonly ILogger<WebApiExecuter> _logger;

        public WebApiExecuter(HttpClient httpClient, string baseUrl, ITokenRepository tokenRepository, ILogger<WebApiExecuter> logger)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
            _tokenRepository = tokenRepository;
            _logger = logger;

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> InvokeGet<T>(string uri)
        {
            try
            {
                await AddTokenHeader();

                return await _httpClient.GetFromJsonAsync<T>(GetUrl(uri));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(InvokeGet));
                throw;
            }
        }

        public async Task<T> InvokePost<T>(string uri, T obj)
        {
            HttpResponseMessage response;
            try
            {
                await AddTokenHeader();

                response = await _httpClient.PostAsJsonAsync(GetUrl(uri), obj);

                await HandleError(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(InvokePost));
                throw;
            }

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public Task<string> InvokePostReturnString<T>(string uri, T obj)
        {
            throw new System.NotImplementedException();
        }

        public async Task InvokePut<T>(string uri, T obj)
        {
            try
            {
                await AddTokenHeader();

                var response = await _httpClient.PutAsJsonAsync(GetUrl(uri), obj);

                await HandleError(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(InvokePut));
                throw;
            }
        }
        public async Task InvokeDelete(string uri)
        {
            try
            {
                await AddTokenHeader();

                var response = await _httpClient.DeleteAsync(GetUrl(uri));
                await HandleError(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(InvokeDelete));
                throw;
            }
        }

        /// <summary>
        /// This method add the JWT token in the session in a header of a request.
        /// </summary>
        private async Task AddTokenHeader()
        {
            if (_tokenRepository != null && !string.IsNullOrWhiteSpace(await _tokenRepository.GetToken()))
            {
                _httpClient.DefaultRequestHeaders.Remove("TokenHeader");
                _httpClient.DefaultRequestHeaders.Add("TokenHeader", await _tokenRepository.GetToken());
            }
        }

        private string GetUrl(string uri)
        {
            return $"{_baseUrl}/{uri}";
        }

        private async Task HandleError(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(error);
            }
        }
    }
}
