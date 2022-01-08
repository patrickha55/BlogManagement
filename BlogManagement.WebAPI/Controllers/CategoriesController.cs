using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.CategoryVMs;
using BlogManagement.Contracts.Services.APIServices;
using BlogManagement.WebAPI.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(
            ICategoryService categoryService,
            ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Paginated<CategoryVM>>> Get([FromQuery] PagingRequest pagingRequest)
        {
            try
            {
                pagingRequest ??= new PagingRequest();

                var categories = await _categoryService.GetCategoryVMsAsync(pagingRequest.PageNumber, pagingRequest.PageSize);

                if (categories is not null)
                {
                    return Ok(new Paginated<CategoryVM>
                    {
                        CurrentPage = categories.CurrentPage,
                        TotalPages = categories.TotalPages,
                        PageSize = categories.PageSize,
                        TotalCount = categories.TotalCount,
                        Objects = categories
                    });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Get));
                return StatusCode(500, Constants.ErrorMessage);
            }

            return NotFound(Constants.ItsEmpty);
        }

        [HttpGet("categories-select-list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CategoryVM>>> GetCategoriesForSelectListAsync([FromQuery] long? parentId = null)
        {
            try
            {
                var categoryVMs = await _categoryService.GetCategoriesForSelectListAsync(parentId);

                if (categoryVMs is null)
                    return NotFound(Constants.ItsEmpty);

                return Ok(categoryVMs);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetCategoriesForSelectListAsync));
                return StatusCode(500, Constants.ErrorMessage);
            }
        }

        [HttpGet("categories-id-name")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CategoryVM>>> GetAllIdAndNameWithoutPagingAsync()
        {
            try
            {
                var categoryVMs = await _categoryService.GetAllIdAndNameWithoutPagingAsync();

                if (categoryVMs is null)
                    return NotFound(Constants.ItsEmpty);

                return Ok(categoryVMs);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetCategoriesForSelectListAsync));
                return StatusCode(500, Constants.ErrorMessage);
            }
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [JwtTokenAuthFilter]
        public async Task<ActionResult<CategoryVM>> Get(long id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(Constants.InvalidArgument);

                var categoryVM = await _categoryService.GetCategoryVMAsync(id);

                if (categoryVM is null)
                    return NotFound(Constants.NotFoundResponse);

                return Ok(categoryVM);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Get));
                return StatusCode(500, Constants.ErrorMessage);
            }
        }

        [JwtTokenAuthFilter]
        [HttpGet("category-for-edit/{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CategoryEditVM>> GetCategoryEditVMAsync(long id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(Constants.InvalidArgument);

                var categoryVM = await _categoryService.GetCategoryEditVMsAsync(id);

                if (categoryVM is null)
                    return NotFound(Constants.NotFoundResponse);

                return Ok(categoryVM);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetCategoryEditVMAsync));
                return StatusCode(500, Constants.ErrorMessage);
            }
        }

        // POST api/<CategoriesController>
        [HttpPost]
        [JwtTokenAuthFilter]
        public async Task<ActionResult> Post([FromBody] CategoryCreateVM request)
        {
            try
            {
                if (request is null)
                    return BadRequest(Constants.PleaseFillIn);

                var result = await _categoryService.CreateCategoryAsync(request);

                if (result)
                    return Created(nameof(Get), request);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Post));
                return StatusCode(500, Constants.ErrorMessage);
            }

            return BadRequest();
        }

        // PUT api/<CategoriesController>/5
        [JwtTokenAuthFilter]
        [HttpPut("{id:long}")]
        public async Task<ActionResult> Put(long id, [FromBody] CategoryEditVM request)
        {
            try
            {
                if (request is null) return BadRequest(Constants.PleaseFillIn);

                if (id != request.Id) return BadRequest(Constants.ErrorForUser);

                var result = await _categoryService.UpdateCategoryAsync(request.Id, request);

                if (result)
                    return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Post));
                return StatusCode(500, Constants.ErrorMessage);
            }

            return BadRequest();
        }

        // DELETE api/<CategoriesController>/5
        [JwtTokenAuthFilter]
        [HttpDelete("{id:long}")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(Constants.InvalidArgument);

                var categoryVM = await _categoryService.GetCategoryVMAsync(id);

                if (categoryVM is null)
                    return NotFound(Constants.NotFoundResponse);

                var result = await _categoryService.DeleteCategoryAsync(categoryVM);

                if (result) return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Delete));
                return StatusCode(500, Constants.ErrorMessage);
            }

            return BadRequest();
        }
    }
}
