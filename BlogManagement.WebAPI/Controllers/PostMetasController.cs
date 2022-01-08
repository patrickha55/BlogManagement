using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.PostMetaVMs;
using BlogManagement.Contracts.Services.APIServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostMetasController : ControllerBase
    {
        private readonly ILogger<PostMetasController> _logger;
        private readonly IPostMetaService _postMetaService;

        public PostMetasController(
            ILogger<PostMetasController> logger,
            IPostMetaService postMetaService)
        {
            _logger = logger;
            _postMetaService = postMetaService;
        }

        // GET: api/<PostMetasController>
        [HttpGet("{postId:long}")]
        public async Task<ActionResult<IPagedList<PostMetaVM>>> Get([FromQuery] PagingRequest pagingRequest, long? postId = null)
        {
            try
            {
                pagingRequest ??= new PagingRequest();

                var postMetaVMs =
                    await _postMetaService.GetPostMetaVMsAsync(pagingRequest,
                        postId is null ? null : p => p.Id == postId);

                return Ok(postMetaVMs);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Get));
                return StatusCode(500, Constants.ErrorMessage);
            }

            return NotFound(Constants.ItsEmpty);
        }
        // GET: api/<PostMetasController>/post-meta-without-paging
        [HttpGet("post-meta-without-paging/{postId:long}")]
        public async Task<ActionResult<IEnumerable<PostMetaVM>>> GetPostMetaVMWithoutPaging(long? postId = null)
        {
            try
            {
                var postMetaVMs =
                    await _postMetaService.GetPostMetaVMsWithoutPagingAsync(
                        postId is null ? null : p => p.Id == postId);

                return postMetaVMs is null ? NotFound(Constants.ItsEmpty) : Ok(postMetaVMs);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostMetaVMWithoutPaging));
                return StatusCode(500, Constants.ErrorMessage);
            }
        }

        // GET api/<PostMetasController>/5
        [HttpGet("{id:long}")]
        public async Task<ActionResult<PostMetaVM>> Get(long id)
        {
            try
            {
                if (id <= 0)
                    return NotFound(Constants.InvalidArgument);

                var postMetaVM = await _postMetaService.GetPostMetaVMAsync(id);

                if (postMetaVM is not null)
                    return Ok(postMetaVM);

                return NotFound(Constants.NotFoundResponse);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostMetaVMWithoutPaging));
                return StatusCode(500, Constants.ErrorMessage);
            }
        }

        // POST api/<PostMetasController>
        [HttpPost]
        public async Task<ActionResult<PostMetaVM>> Post([FromBody] PostMetaCreateVM request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var postMetaVM = await _postMetaService.CreatePostMetaVMAsync(request);

                    if (postMetaVM is null)
                        return BadRequest(Constants.ErrorForUser);

                    return CreatedAtAction(nameof(Get), new { id = postMetaVM.Id }, postMetaVM);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Post));
                return StatusCode(500, Constants.ErrorMessage);
            }

            return BadRequest(Constants.ErrorForUser);
        }

        // PUT api/<PostMetasController>/5
        [HttpPut("{id:long}")]
        public async Task<ActionResult> Put(long id, [FromBody] PostMetaEditVM request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id != request.Id)
                        return BadRequest(Constants.InvalidArgument);

                    var result = await _postMetaService.UpdatePostMetaAsync(id, request);

                    if (result)
                        return NoContent();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Put));
                return StatusCode(500, Constants.ErrorMessage);
            }

            return BadRequest(Constants.ErrorForUser);
        }

        // DELETE api/<PostMetasController>/5
        [HttpDelete("{id:long}")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id <= 0)
                        return BadRequest(Constants.InvalidArgument);

                    var postMeta = await _postMetaService.GetPostMetaAsync(id);

                    if (postMeta is null)
                        return NotFound(Constants.NotFoundResponse);

                    var result = await _postMetaService.DeletePostMetaAsync(postMeta);

                    if (result)
                        return NoContent();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Put));
                return StatusCode(500, Constants.ErrorMessage);
            }

            return BadRequest(Constants.ErrorForUser);
        }
    }
}
