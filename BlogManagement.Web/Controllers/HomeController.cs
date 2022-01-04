using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.PostVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using BlogManagement.Contracts.Services.ClientServices;
using Exception = System.Exception;

namespace BlogManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;

        public HomeController(
            ILogger<HomeController> logger, IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var postForIndexVMs = new List<PostForIndexVM>();

            try
            {
                postForIndexVMs = await _postService.GetPostsForIndexVMsAsync(new PagingRequest(pageNumber, pageSize));
            }
            catch (Exception e)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogError(e, "Error: {0} {1}", Constants.ErrorMessageLogging, nameof(Index));
            }

            return View(postForIndexVMs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchAnything(string keyword)
        {
            try
            {
                ViewData["Users"] = await _userService.FindAuthorVMsAsync(keyword);
                return View("~/Views/Home/SearchAnything.cshtml");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(SearchAnything));
            }

            return RedirectToAction(nameof(Index));
        }*/
    }
}
