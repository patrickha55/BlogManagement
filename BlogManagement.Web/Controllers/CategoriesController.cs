using AutoMapper;
using BlogManagement.Application.Contracts;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.TagVMs;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogManagement.Common.Models.CategoryVMs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogManagement.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    [Route("Admins/Categories")]
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CategoriesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var categoryVMs = new List<CategoryVM>();

            try
            {
                var pagingRequest = new PagingRequest
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                var includes = new List<string> {"ParentCategory"};

                var categories =
                    await _unitOfWork.CategoryRepository
                        .GetAllAsync(null, pagingRequest, includes);

                categoryVMs = _mapper.Map<List<CategoryVM>>(categories);
            }
            catch (Exception e)
            {
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
            ViewBag.CategoryName = await GetCategoriesForSelectListAsync();

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
                    var category = _mapper.Map<Category>(request);
                    var result = await _unitOfWork.CategoryRepository.CreateAsync(category);

                    if (result)
                    {
                        await _unitOfWork.SaveAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Create));
            }

            ViewBag.CategoryName = await GetCategoriesForSelectListAsync(request.ParentId);

            return View("~/Views/Admins/Categories/Create.cshtml", request);
        }

        [Route("Edit/{id:long}")]
        public async Task<IActionResult> Edit(long id)
        {
            var categoryVM = new CategoryEditVM();
            try
            {
                if (id <= 0)
                    throw new ArgumentException(Constants.InvalidArgument);

                var category = await _unitOfWork.CategoryRepository.GetAsync(t => t.Id == id);

                if (category is null)
                    throw new ArgumentException(Constants.InvalidArgument);

                ViewBag.CategoryName = await GetCategoriesForSelectListAsync(category.ParentId);

                categoryVM = _mapper.Map<CategoryEditVM>(category);
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, "{0} {1}", Constants.InvalidArgument, nameof(Edit));
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
                if (id != request.Id)
                    return NotFound();

                if (ModelState.IsValid)
                {
                    var result = await _unitOfWork.CategoryRepository.UpdateAsync(_mapper.Map<Category>(request));

                    if (result)
                    {
                        await _unitOfWork.SaveAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!await _unitOfWork.CategoryRepository.IsExistsAsync(id))
                    return NotFound();

                _logger.LogInformation(e, "{0} {1}", Constants.ObjectAlreadyExists, nameof(Edit));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Edit));
            }

            ViewBag.CategoryName = await GetCategoriesForSelectListAsync(request.ParentId);

            return View("~/Views/Admins/Categories/Edit.cshtml", request);
        }

        [HttpPost]
        [Route("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if (id <= 0)
                    return NotFound();

                var category = await _unitOfWork.CategoryRepository.GetAsync(t => t.Id == id);

                if (category is null) return NotFound();

                var result = await _unitOfWork.CategoryRepository.DeleteAsync(category);

                if (result) await _unitOfWork.SaveAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Delete));
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private async Task<SelectList> GetCategoriesForSelectListAsync(long? parentId = null)
        {
            var categories =
                await _unitOfWork.CategoryRepository.GetAllIdAndNameWithoutPagingAsync();

            return new SelectList(categories, "Id", "Title", parentId);
        }
    }
}
