using AutoMapper;
using BlogManagement.Application.Contracts;
using BlogManagement.Common.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using BlogManagement.Common.Models.AuthorVMs;

namespace BlogManagement.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<AdminsController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(ILogger<AdminsController> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // GET: UsersController/Details/5
        public async Task<ActionResult> Details(long id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException(Constants.InvalidArgument);

                var user = await _unitOfWork.UserRepository.FindUserDetailAsync(u => u.Id == id);

                if (user is null)
                    throw new ArgumentException(Constants.InvalidArgument);

                var userVM = _mapper.Map<AuthorDetailVM>(user);

                return View(userVM);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Details));
            }

            TempData["Error"] = "Something went wrong please try again later.";
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
