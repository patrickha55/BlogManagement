using BlogManagement.Common.Common;
using BlogManagement.Common.Models.TagVMs;
using BlogManagement.Contracts.Services.ClientServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BlogManagement.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    [Route("Admins/Tags")]
    public class TagsController : Controller
    {
        private readonly ILogger<TagsController> _logger;
        private readonly ITagService _tagService;

        public TagsController(
            ILogger<TagsController> logger, ITagService tagService)
        {
            _logger = logger;
            _tagService = tagService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 2)
        {
            Paginated<TagVM> tagVMs = null;

            try
            {
                tagVMs = await _tagService.GetTagVMsAsync(pageNumber, pageSize);
            }
            catch (Exception e)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Index));
            }

            return View("~/Views/Admins/Tags/Index.cshtml", tagVMs);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> Details(long id)
        {
            TagVM tagVM = null;

            try
            {
                var token = HttpContext.Session.GetString(nameof(Token.JwtToken));
                tagVM = await _tagService.GetTagVMAsync(token, id);

            }
            catch (Exception e)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Index));
            }

            return View("~/Views/Admins/Tags/Details.cshtml", tagVM);
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View("~/Views/Admins/Tags/Create.cshtml");
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TagCreateVM request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var token = HttpContext.Session.GetString(nameof(Token.JwtToken));
                    var result = await _tagService.CreateTagAsync(token, request);

                    if (result)
                    {
                        TempData[Constants.Success] = Constants.SuccessMessage;
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception e)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Create));
            }

            return View("~/Views/Admins/Tags/Create.cshtml", request);
        }

        [Route("Edit/{id:long}")]
        public async Task<IActionResult> Edit(long id)
        {
            var tagVM = new TagEditVM();
            try
            {
                var token = HttpContext.Session.GetString(nameof(Token.JwtToken));
                tagVM = await _tagService.GetTagEditVMsAsync(token, id);
            }
            catch (Exception e)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Edit));
            }

            return View("~/Views/Admins/Tags/Edit.cshtml", tagVM);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("Edit/{id:long}")]
        public async Task<IActionResult> Edit(long id, TagEditVM request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var token = HttpContext.Session.GetString(nameof(Token.JwtToken));
                    await _tagService.UpdateTagAsync(token, id, request);


                    TempData[Constants.Success] = Constants.SuccessMessage;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Edit));
            }

            return View("~/Views/Admins/Tags/Edit.cshtml", request);
        }

        [HttpPost]
        [Route("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var token = HttpContext.Session.GetString(nameof(Token.JwtToken));
                await _tagService.DeleteTagAsync(token, id);
            }
            catch (Exception e)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Delete));
            }

            TempData[Constants.Success] = Constants.SuccessMessage;
            return RedirectToAction(nameof(Index));
        }
    }
}
