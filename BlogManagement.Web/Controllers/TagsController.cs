using BlogManagement.Common.Common;
using BlogManagement.Common.Models.TagVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var tagVMs = new List<TagVM>();

            try
            {
                tagVMs = await _tagService.GetTagVMsAsync();
            }
            catch (Exception e)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Index));
            }

            return View("~/Views/Admins/Tags/Index.cshtml", tagVMs);
        }

        [HttpGet("{id:long}")]
        public IActionResult Details(long id)
        {
            return View("~/Views/Admins/Tags/Details.cshtml");
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
                    var result = await _tagService.CreateTagAsync(request);

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
                tagVM = await _tagService.GetTagEditVMsAsync(id);
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
                    var result = await _tagService.UpdateTagAsync(id, request);

                    if (result)
                    {
                        TempData[Constants.Success] = Constants.SuccessMessage;
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!await _tagService.IsTagExist(id))
                    return NotFound();

                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogError(e, "{0} {1}", Constants.ObjectAlreadyExists, nameof(Edit));
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
                var result = await _tagService.DeleteTagAsync(id);

                if (!result) TempData[Constants.Error] = Constants.ErrorMessage;
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
