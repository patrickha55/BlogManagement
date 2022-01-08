using BlogManagement.Common.Common;
using BlogManagement.Common.Models.AuthorVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogManagement.Common.Models;
using BlogManagement.Contracts.Services.ClientServices;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class AdminsController : Controller
    {
        private readonly ILogger<AdminsController> _logger;
        private readonly IUserService _userService;

        public AdminsController(
            ILogger<AdminsController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Admins/UsersManagement")]
        public async Task<IActionResult> UserIndex(int pageNumber = 1, int pageSize = 10)
        {
            var userVMs = new List<AuthorAdminIndexVM>();
            try
            {
                var token = HttpContext.Session.GetString(nameof(Token.JwtToken));
                userVMs = await _userService.GetAuthorAdminIndexVMAsync(token, new PagingRequest(pageNumber, pageSize));
            }
            catch (Exception e)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(UserIndex));
            }

            return View("~/Views/Admins/UsersManagement/Index.cshtml", userVMs);
        }

        [Route("Admins/UsersManagement/{id:long}")]
        public async Task<IActionResult> UserDetails(long id)
        {
            var userVM = new AuthorDetailVM();

            try
            {
                userVM = await _userService.GetAuthorDetailVMAsync(id);
            }
            catch (Exception e)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(UserDetails));
            }

            return View("~/Views/Admins/UsersManagement/Details.cshtml", userVM);
        }

        public async Task<IActionResult> EditUserStatuses(long id, AuthorDetailVM request)
        {
            try
            {
                var token = HttpContext.Session.GetString(nameof(Token.JwtToken));
                var result = await _userService.EditUserStatusesAsync(token, id, request);

                if (result)
                {
                    TempData[Constants.Success] = Constants.SuccessMessage;
                    return RedirectToAction(nameof(UserDetails), new { id });
                }
            }
            catch (Exception e)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(UserDetails));
            }

            TempData[Constants.Error] = Constants.ErrorMessage;
            return RedirectToAction(nameof(UserDetails), new { id });
        }

        public IActionResult Test()
        {
            return View();
        }
    }
}
