using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.CategoryVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public async Task<ActionResult<List<CategoryVM>>> Get([FromQuery] PagingRequest pagingRequest)
        {
            var categories = new List<CategoryVM>();

            try
            {
                pagingRequest ??= new PagingRequest();

                categories =
                    await _categoryService.GetCategoryVMsAsync(pagingRequest.PageNumber, pagingRequest.PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Get));
            }

            return Ok(categories.Any() ? categories : "There is no categories at the moment.");
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
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
            }

            return BadRequest();
        }

        // POST api/<CategoriesController>
        [HttpPost]
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
            }

            return BadRequest();
        }

        // PUT api/<CategoriesController>/5
        [HttpPut()]
        public async Task<ActionResult> Put([FromBody] CategoryEditVM request)
        {
            try
            {
                if (request is null) return BadRequest(Constants.PleaseFillIn);

                var result = await _categoryService.UpdateCategoryAsync(request.Id, request);

                if (result)
                    return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Post));
            }

            return BadRequest();
        }

        // DELETE api/<CategoriesController>/5
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
            }

            return BadRequest();
        }
    }
}
