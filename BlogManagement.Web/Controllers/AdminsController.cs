using BlogManagement.Common.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlogManagement.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class AdminsController : Controller
    {
        private readonly ILogger<AdminsController> _logger;

        public AdminsController(ILogger<AdminsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }
    }
}
