using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BlogManagement.Application.Contracts;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models.AuthorVMs;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using X.PagedList;

namespace BlogManagement.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class AdminsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AdminsController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AdminsController(
            ILogger<AdminsController> logger, 
            UserManager<User> userManager,
            IMapper mapper, 
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            this._mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Admins/UsersManagement")]
        public async Task<IActionResult> UserIndex()
        {
            var userVMs = new List<AuthorAdminIndexVM>();
            try
            {
                var users = await _userManager.Users.ToListAsync();
                userVMs = _mapper.Map<List<AuthorAdminIndexVM>>(users);
            }
            catch (Exception e)
            {
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
                if (id <= 0)
                    throw new ArgumentException(Constants.InvalidArgument);

                var user = await _unitOfWork.UserRepository.FindUserDetailAsync(u => u.Id == id);

                if (user == null)
                    throw new ArgumentException(Constants.InvalidArgument);

                userVM = _mapper.Map<AuthorDetailVM>(user);
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, "{0} {1}", Constants.InvalidArgument, nameof(UserDetails));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(UserDetails));
            }

            return View("~/Views/Admins/UsersManagement/Details.cshtml", userVM);
        }

        public async Task<IActionResult> EditUserStatuses(long id, AuthorDetailVM request)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException(Constants.InvalidArgument);

                var user = await _unitOfWork.UserRepository.GetAsync(u => u.Id == id);

                if (user == null)
                    throw new ArgumentException(Constants.InvalidArgument);

                user.IsEnabled = request.IsEnabled;
                user.IsPublic = request.IsPublic;

                var result = await _unitOfWork.UserRepository.UpdateAsync(user);

                if (result)
                {
                    await _unitOfWork.SaveAsync();
                    TempData["Status"] = "User statuses updated successfully!";
                    return RedirectToAction(nameof(UserDetails), new { id });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(UserDetails));
            }

            TempData["Status"] = "User statuses updated failed!";
            return RedirectToAction(nameof(UserDetails), new { id });
        }

        public IActionResult Test()
        {
            return View();
        }
    }
}
