using AutoMapper;
using BlogManagement.Common.Common;
using BlogManagement.Contracts;
using BlogManagement.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using BlogManagement.Contracts.Services.APIServices;

namespace BlogManagement.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<AdminsController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public UsersController(
            ILogger<AdminsController> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        // GET: UsersController/Details/5
        public async Task<ActionResult> Details(long id)
        {
            try
            {
                var userVM = await _userService.FindAuthorDetailVMAsync(id);

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
