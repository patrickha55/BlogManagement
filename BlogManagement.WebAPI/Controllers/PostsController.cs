﻿using System;
using BlogManagement.Common.Models.PostVMs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ILogger<PostsController> _logger;
        private readonly IPostService _postService;

        public PostsController(
            ILogger<PostsController> logger,
            IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        // GET: api/<PostsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PostForIndexVM>>> GetPostsForIndexVMs([FromQuery] PagingRequest pagingRequest)
        {
            var postVMs = new List<PostForIndexVM>();

            try
            {
                pagingRequest ??= new PagingRequest();

                postVMs = await _postService.GetPostsForIndexVMsAsync(pagingRequest);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Post));

            }

            return Ok(postVMs.Any() ? postVMs : "There is no post at the moment.");
        }

        // GET: api/<PostsController>/
        [HttpGet("posts-of-an-author/{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PostForIndexVM>>> GetPostsOfAnAuthor(long id, [FromQuery] PagingRequest pagingRequest)
        {
            var postVMs = new List<PostForIndexVM>();

            try
            {
                pagingRequest ??= new PagingRequest();

                postVMs = await _postService.GetPostsForIndexVMsAsync(pagingRequest, id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostsOfAnAuthor));
            }

            return Ok(postVMs.Any() ? postVMs : "There is no post at the moment.");
        }

        // GET api/<PostsController>/5
        [HttpGet("detail/{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PostDetailVM>> GetPostDetail(long id, [FromQuery] string userName = null)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(Constants.InvalidArgument);

                var postVM = await _postService.GetPostDetailVMAsync(id, userName);

                if (postVM is null)
                    return NotFound(Constants.NotFoundResponse);

                return Ok(postVM);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Get));
            }
            return BadRequest(Constants.ErrorForUser);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PostVM>> Get(long id, IFormFile test)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(Constants.InvalidArgument);

                var postVM = await _postService.GetPostVMAsync(id);

                if (postVM is null)
                    return NotFound(Constants.NotFoundResponse);

                return Ok(postVM);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Get));
            }

            return BadRequest(Constants.ErrorForUser);
        }

        // POST api/<PostsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PostVM>> Post([FromQuery] string userName, [FromForm] PostCreateVM request)
        {
            try
            {
                if (userName is null || request is null) return BadRequest();

                var postVM = await _postService.CreatePostAsync(request, userName);

                if (postVM is not null)
                    return CreatedAtAction(nameof(Get), new { id = postVM.Id }, postVM);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Post));
            }

            return BadRequest(Constants.ErrorForUser);
        }

        // PUT api/<PostsController>/5
        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(long id, [FromForm] PostEditVM request)
        {
            try
            {
                if (id <= 0 || request is null || id != request.Id)
                    return BadRequest();

                if (await _postService.GetPostVMAsync(id) is null)
                    return NotFound();

                var result = await _postService.UpdatePostAsync(id, request);

                if (result)
                    return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Put));
            }

            return BadRequest(Constants.ErrorForUser);
        }

        // DELETE api/<PostsController>/5
        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(Constants.InvalidArgument);

                var post =
                    await _postService.GetPostAsync(id);

                if (post is null)
                    return NotFound(Constants.NotFoundResponse);

                await _postService.DeletePostAsync(post);

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Delete));
            }

            return BadRequest(Constants.ErrorForUser);
        }
    }
}