using BlogManagement.Contracts.Repositories;
using Microsoft.JSInterop;

namespace BlogManagement.Repository.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IJSRuntime iJSRuntime;

        public TokenRepository(IJSRuntime iJsRuntime)
        {
            iJSRuntime = iJsRuntime;
        }

        public async Task<string> GetToken()
        {
            return await iJSRuntime.InvokeAsync<string>("sessionStorage.getItem", "token");
        }

        public async Task SetToken(string token)
        {
            await iJSRuntime.InvokeVoidAsync("sessionStorage.setItem", "token", token);
        }
    }
}
