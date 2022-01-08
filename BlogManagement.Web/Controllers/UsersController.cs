using BlogManagement.Common.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using BlogManagement.Contracts.Services.ClientServices;

namespace BlogManagement.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<AdminsController> _logger;
        private readonly IUserService _userService;

        public UsersController(
            ILogger<AdminsController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        // GET: UsersController/Details/5
        public async Task<ActionResult> Details(long id)
        {
            try
            {
                var userVM = await _userService.GetAuthorDetailVMAsync(id);

                return View(userVM);
            }
            catch (Exception e)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Details));
            }

            TempData[Constants.Error] = Constants.ErrorMessage;

            return RedirectToAction(nameof(Index), "Home");
        }

        // GET: UsersController/Edit/5
        public ActionResult Edit(long id)
        {
            throw new NotImplementedException();
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, IFormCollection collection)
        {
            throw new NotImplementedException();
        }
    }
}
