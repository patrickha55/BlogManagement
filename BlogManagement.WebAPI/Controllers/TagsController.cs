using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.TagVMs;
using BlogManagement.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogManagement.Contracts.Services.APIServices;
using BlogManagement.WebAPI.Filters;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly ILogger<TagsController> _logger;

        public TagsController(
            ITagService tagService,
            ILogger<TagsController> logger)
        {
            _tagService = tagService;
            _logger = logger;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<ActionResult<Paginated<TagVM>>> Get([FromQuery] PagingRequest pagingRequest)
        {
            try
            {
                pagingRequest ??= new PagingRequest();

                var tags =
                    await _tagService.GetTagVMsAsync(pagingRequest.PageNumber, pagingRequest.PageSize);

                if (tags is not null)
                {
                    return Ok(new Paginated<TagVM>
                    {
                        CurrentPage = tags.CurrentPage,
                        TotalPages = tags.TotalPages,
                        PageSize = tags.PageSize,
                        TotalCount = tags.TotalCount,
                        Objects = tags
                    });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Get));
            }

            return NotFound(Constants.ItsEmpty);
        }

        [HttpGet("tags-id-title")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TagVM>>> GetAllIdAndTitleWithoutPagingAsync()
        {
            IEnumerable<TagVM> tagVMs = null;

            try
            {
                tagVMs =
                    await _tagService.GetAllIdAndNameWithoutPagingAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAllIdAndTitleWithoutPagingAsync));
            }

            return Ok(tagVMs is not null ? tagVMs : "There is no tags at the moment.");
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id:long}")]
        [JwtTokenAuthFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TagVM>> Get(long id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(Constants.InvalidArgument);

                var tagVM = await _tagService.GetTagVMAsync(id);

                if (tagVM is null)
                    return NotFound(Constants.NotFoundResponse);

                return Ok(tagVM);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Get));
            }

            return BadRequest();
        }

        [JwtTokenAuthFilter]
        [HttpGet("tag-for-edit/{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TagEditVM>> GetTagEditVMAsync(long id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(Constants.InvalidArgument);

                var tagVM = await _tagService.GetTagEditVMsAsync(id);

                if (tagVM is null)
                    return NotFound(Constants.NotFoundResponse);

                return Ok(tagVM);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetTagEditVMAsync));
            }

            return BadRequest();
        }

        // POST api/<CategoriesController>
        [HttpPost]
        [JwtTokenAuthFilter]
        public async Task<ActionResult> Post([FromBody] TagCreateVM request)
        {
            try
            {
                if (request is null)
                    return BadRequest(Constants.PleaseFillIn);

                var result = await _tagService.CreateTagAsync(request);

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
        [HttpPut("{id:long}")]
        [JwtTokenAuthFilter]
        public async Task<ActionResult> Put(long id, [FromBody] TagEditVM request)
        {
            try
            {
                if (request is null) return BadRequest(Constants.PleaseFillIn);

                if (id != request.Id) return BadRequest(Constants.ErrorForUser);

                var result = await _tagService.UpdateTagAsync(request.Id, request);

                if (result)
                    return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Put));
            }

            return BadRequest();
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id:long}")]
        [JwtTokenAuthFilter]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(Constants.InvalidArgument);

                var tagVM = await _tagService.GetTagVMAsync(id);

                if (tagVM is null)
                    return NotFound(Constants.NotFoundResponse);

                var result = await _tagService.DeleteTagAsync(tagVM);

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
