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

namespace BlogManagement.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    [Route("Admins/Tags")]
    public class TagsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TagsController> _logger;

        public TagsController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<TagsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var tagVMs = new List<TagVM>();

            try
            {
                var pagingRequest = new PagingRequest
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
                var tags = await _unitOfWork.TagRepository.GetAllAsync(null, pagingRequest);

                tagVMs = _mapper.Map<List<TagVM>>(tags);
            }
            catch (Exception e)
            {
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
                    var tag = _mapper.Map<Tag>(request);
                    var result = await _unitOfWork.TagRepository.CreateAsync(tag);

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

            return View("~/Views/Admins/Tags/Create.cshtml", request);
        }

        [Route("Edit/{id:long}")]
        public async Task<IActionResult> Edit(long id)
        {
            var tagVM = new TagEditVM();
            try
            {
                if (id <= 0)
                    throw new ArgumentException(Constants.InvalidArgument);

                var tag = await _unitOfWork.TagRepository.GetAsync(t => t.Id == id);

                if (tag is null)
                    throw new ArgumentException(Constants.InvalidArgument);

                tagVM = _mapper.Map<TagEditVM>(tag);
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, "{0} {1}", Constants.InvalidArgument, nameof(Edit));
            }
            catch (Exception e)
            {
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
                if (id != request.Id)
                    return NotFound();

                if (ModelState.IsValid)
                {
                    var result = await _unitOfWork.TagRepository.UpdateAsync(_mapper.Map<Tag>(request));

                    if (result)
                    {
                        await _unitOfWork.SaveAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!await _unitOfWork.TagRepository.IsExistsAsync(id))
                    return NotFound();

                _logger.LogInformation(e, "{0} {1}", Constants.ObjectAlreadyExists, nameof(Edit));
            }
            catch (Exception e)
            {
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
                if (id <= 0)
                    return NotFound();

                var tag = await _unitOfWork.TagRepository.GetAsync(t => t.Id == id);

                if (tag is null) return NotFound();

                var result = await _unitOfWork.TagRepository.DeleteAsync(tag);

                if (result) await _unitOfWork.SaveAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Delete));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
