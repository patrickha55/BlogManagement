using BlogManagement.Contracts.AuthWithJwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace BlogManagement.WebAPI.Filters
{
    public class JwtTokenAuthFilterAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var authManager = context.HttpContext.RequestServices.GetService(typeof(IAuthManager)) as IAuthManager;

            if (authManager is null || !authManager.VerifyToken(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            };
        }
    }
}
