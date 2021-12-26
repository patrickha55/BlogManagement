using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BlogManagement.Application.Contracts;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.PostVMs;
using BlogManagement.Common.Models.TagVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlogManagement.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
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

            return View("~/Views/Admin/Tags/Index.cshtml", tagVMs);
        }
    }
}
