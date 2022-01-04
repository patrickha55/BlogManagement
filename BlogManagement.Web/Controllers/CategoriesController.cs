using BlogManagement.Common.Common;
using BlogManagement.Common.Models.CategoryVMs;
using BlogManagement.Contracts.Services.ClientServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogManagement.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    [Route("Admins/Categories")]
    public class CategoriesController : Controller
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ICategoryService _categoryService;


        public CategoriesController(
            ILogger<CategoriesController> logger,
            ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var categoryVMs = new List<CategoryVM>();
            try
            {
                categoryVMs = await _categoryService.GetCategoryVMsAsync(pageNumber, pageSize);
            }
            catch (Exception e)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Index));
            }

            return View("~/Views/Admins/Categories/Index.cshtml", categoryVMs);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> Details(long id)
        {
            var token = HttpContext.Session.GetString(nameof(Token.JwtToken));
            var categoryVM = await _categoryService.GetCategoryVMAsync(token, id);
            return View("~/Views/Admins/Categories/Details.cshtml", categoryVM);
        }

        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            var token = HttpContext.Session.GetString(nameof(Token.JwtToken));
            ViewBag.CategoryName = new SelectList(await _categoryService.GetCategoriesForSelectListAsync(token), "Id", "Title");

            return View("~/Views/Admins/Categories/Create.cshtml");
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM request)
        {
            var token = HttpContext.Session.GetString(nameof(Token.JwtToken));
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _categoryService.CreateCategoryAsync(token, request);

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

            ViewBag.CategoryName = await _categoryService.GetCategoriesForSelectListAsync(token, request.ParentId);

            return View("~/Views/Admins/Categories/Create.cshtml", request);
        }

        [Route("Edit/{id:long}")]
        public async Task<IActionResult> Edit(long id)
        {
            var categoryVM = new CategoryEditVM();

            try
            {
                var token = HttpContext.Session.GetString(nameof(Token.JwtToken));
                categoryVM = await _categoryService.GetCategoryEditVMsAsync(token, id);

                ViewBag.CategoryName = new SelectList(await _categoryService.GetCategoriesForSelectListAsync(token), "Id", "Title", categoryVM.ParentId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Edit));
            }

            return View("~/Views/Admins/Categories/Edit.cshtml", categoryVM);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("Edit/{id:long}")]
        public async Task<IActionResult> Edit(long id, CategoryEditVM request)
        {
            var token = HttpContext.Session.GetString(nameof(Token.JwtToken));
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _categoryService.UpdateCategoryAsync(token, id, request);

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
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Edit));
            }

            ViewBag.CategoryName = new SelectList(await _categoryService.GetCategoriesForSelectListAsync(token), "Id", "Title", request.ParentId);

            return View("~/Views/Admins/Categories/Edit.cshtml", request);
        }

        [HttpPost]
        [Route("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var token = HttpContext.Session.GetString(nameof(Token.JwtToken));
                var result = await _categoryService.DeleteCategoryAsync(token, id);

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
