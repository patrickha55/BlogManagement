using BlogManagement.Application.Contracts.Services;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models.CategoryVMs;
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
        public IActionResult Details(long id)
        {
            return View("~/Views/Admins/Categories/Details.cshtml");
        }

        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryName = await _categoryService.GetCategoriesForSelectListAsync();

            return View("~/Views/Admins/Categories/Create.cshtml");
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _categoryService.CreateCategoryAsync(request);

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

            ViewBag.CategoryName = await _categoryService.GetCategoriesForSelectListAsync(request.ParentId);

            return View("~/Views/Admins/Categories/Create.cshtml", request);
        }

        [Route("Edit/{id:long}")]
        public async Task<IActionResult> Edit(long id)
        {
            var categoryVM = new CategoryEditVM();

            try
            {
                categoryVM = await _categoryService.GetCategoryEditVMsAsync(id);

                ViewBag.CategoryName = await _categoryService.GetCategoriesForSelectListAsync(categoryVM.ParentId);
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
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _categoryService.UpdateCategoryAsync(id, request);

                    if (result)
                    {
                        TempData[Constants.Success] = Constants.SuccessMessage;
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!await _categoryService.IsCategoryExist(id))
                    return NotFound();

                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogInformation(e, "{0} {1}", Constants.ObjectAlreadyExists, nameof(Edit));
            }
            catch (Exception e)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Edit));
            }

            ViewBag.CategoryName = await _categoryService.GetCategoriesForSelectListAsync(request.ParentId);

            return View("~/Views/Admins/Categories/Edit.cshtml", request);
        }

        [HttpPost]
        [Route("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var result = await _categoryService.DeleteCategoryAsync(id);

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
