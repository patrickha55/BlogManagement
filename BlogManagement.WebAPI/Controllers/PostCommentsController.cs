using BlogManagement.Common.Common;
using BlogManagement.Common.Models.PostCommentVMs;
using BlogManagement.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using BlogManagement.Contracts.Services.APIServices;

namespace BlogManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostCommentsController : ControllerBase
    {
        private readonly IPostCommentService _postCommentService;
        private readonly ILogger<PostCommentsController> _logger;

        public PostCommentsController(IPostCommentService postCommentService, ILogger<PostCommentsController> logger)
        {
            _postCommentService = postCommentService;
            _logger = logger;
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PostCommentVM>> GetAsync(long id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(Constants.InvalidArgument);

                var post = await _postCommentService.GetPostCommentVMAsync(id);

                if (post is null)
                    return NotFound(Constants.NotFoundResponse);

                return Ok(post);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAsync));
            }

            return BadRequest(Constants.ErrorForUser);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PostCommentVM>> CreateAsync(PostCommentCreateVM request)
        {
            try
            {
                if (request is null)
                    return BadRequest(Constants.InvalidArgument);

                var postCommentVM = await _postCommentService.CreatePostCommentAsync(request);

                if (postCommentVM is null)
                    return BadRequest(Constants.ErrorForUser);

                return CreatedAtAction("Get", new { id = postCommentVM.Id }, postCommentVM);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(CreateAsync));
            }

            return BadRequest(Constants.ErrorForUser);
        }

    }
}
