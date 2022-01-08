using BlogManagement.Common.Common;
using BlogManagement.Common.DTOs.PostDTOs;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.PostVMs;
using BlogManagement.Contracts.Services.APIServices;
using BlogManagement.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<ActionResult<Paginated<PostForIndexVM>>> GetPostsForIndexVMs([FromQuery] PagingRequest pagingRequest)
        {
            try
            {
                pagingRequest ??= new PagingRequest();

                var postVMs = await _postService.GetPostsForIndexVMsAsync(pagingRequest);

                if (postVMs.Any())
                {
                    return Ok(new Paginated<PostForIndexVM>
                    {
                        CurrentPage = postVMs.CurrentPage,
                        TotalPages = postVMs.TotalPages,
                        PageSize = postVMs.PageSize,
                        TotalCount = postVMs.TotalCount,
                        Objects = postVMs
                    });
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Post));
                return StatusCode(500, Constants.ErrorMessage);
            }

            return NotFound(Constants.ItsEmpty);
        }

        // GET: api/<PostsController>/posts-admins
        [HttpGet("posts-admins")]
        public async Task<ActionResult<Paginated<PostForAdminIndexVM>>> GetPostsForAdminIndex([FromQuery] PagingRequest pagingRequest)
        {
            try
            {
                pagingRequest ??= new PagingRequest();

                var postVMs = await _postService.GetPostForAdminIndexVMsAsync(pagingRequest.PageNumber, pagingRequest.PageSize);

                if (postVMs.Any())
                    return Ok(new Paginated<PostForAdminIndexVM>
                    {
                        CurrentPage = postVMs.CurrentPage,
                        TotalPages = postVMs.TotalPages,
                        PageSize = postVMs.PageSize,
                        TotalCount = postVMs.TotalCount,
                        Objects = postVMs
                    });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostsOfAnAuthor));
                return StatusCode(500, Constants.ErrorMessage);
            }

            return NotFound(Constants.ItsEmpty);
        }

        // GET: api/<PostsController>/specific-posts
        [HttpGet("specific-posts")]
        public async Task<ActionResult<Paginated<PostForIndexVM>>> FindPostsAsync([FromQuery] SearchRequest request)
        {
            try
            {
                if (request?.Keyword is null)
                    return BadRequest(Constants.InvalidArgument);

                var pagingRequest = new PagingRequest(request.PageNumber, request.PageSize);

                var postVMs = await _postService.FindPostsAsync(pagingRequest, request.Keyword);

                if (postVMs.Any())
                    return Ok(new Paginated<PostForIndexVM>
                    {
                        CurrentPage = postVMs.CurrentPage,
                        TotalPages = postVMs.TotalPages,
                        PageSize = postVMs.PageSize,
                        TotalCount = postVMs.TotalCount,
                        Objects = postVMs
                    });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(FindPostsAsync));
                return StatusCode(500, Constants.ErrorMessage);

            }

            return NotFound(Constants.ItsEmpty);
        }


        // GET: api/<PostsController>/select-lists-for-posts-creation
        [JwtTokenAuthFilter]
        [HttpGet("select-lists-for-posts-creation")]
        public async
            Task<ActionResult<PostRelatedListOfObjectsDTO>>
            GetSelectListsForPostCreationAsync()
        {
            PostRelatedListOfObjectsDTO postRelatedList;

            try
            {
                postRelatedList = await _postService.GetSelectListsForPostCreationAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostsOfAnAuthor));
                return StatusCode(500, Constants.ErrorMessage);
            }

            return Ok(postRelatedList);
        }

        // GET: api/<PostsController>/posts-of-an-author/1
        [HttpGet("posts-of-an-author/{id:long}")]
        public async Task<ActionResult<Paginated<PostForIndexVM>>> GetPostsOfAnAuthor(long id, [FromQuery] PagingRequest pagingRequest)
        {
            try
            {
                pagingRequest ??= new PagingRequest();

                var postVMs = await _postService.GetPostsForIndexVMsAsync(pagingRequest, id);

                if (postVMs.Any())
                    return Ok(new Paginated<PostForIndexVM>
                    {
                        CurrentPage = postVMs.CurrentPage,
                        TotalPages = postVMs.TotalPages,
                        PageSize = postVMs.PageSize,
                        TotalCount = postVMs.TotalCount,
                        Objects = postVMs
                    });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostsOfAnAuthor));
                return StatusCode(500, Constants.ErrorMessage);

            }

            return NotFound(Constants.ItsEmpty);
        }


        // GET api/<PostsController>/detail/5?userName=name
        [HttpGet("detail/{id:long}")]
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
                return StatusCode(500, Constants.ErrorMessage);
            }
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<PostVM>> Get(long id)
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
                return StatusCode(500, Constants.ErrorMessage);
            }
        }

        // POST api/<PostsController>
        [HttpPost]
        [JwtTokenAuthFilter]
        public async Task<ActionResult<PostVM>> Post([FromForm] PostCreateVM request)
        {
            try
            {
                if (request?.UserName is null) return BadRequest();

                var postVM = await _postService.CreatePostAsync(request, request.UserName);

                if (postVM is not null)
                    return CreatedAtAction(nameof(Get), new { id = postVM.Id }, postVM);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Post));
                return StatusCode(500, Constants.ErrorMessage);
            }

            return BadRequest(Constants.ErrorForUser);
        }

        // PUT api/<PostsController>/5
        [JwtTokenAuthFilter]
        [HttpPut("{id:long}")]
        public async Task<ActionResult> Put(long id, [FromForm] PostEditVM request)
        {
            try
            {
                if (id <= 0 || request is null || id != request.Id)
                    return BadRequest(Constants.InvalidArgument);

                if (await _postService.GetPostVMAsync(id) is null)
                    return NotFound(Constants.NotFoundResponse);

                var result = await _postService.UpdatePostAsync(id, request);

                if (result)
                    return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Put));
                return StatusCode(500, Constants.ErrorMessage);
            }

            return BadRequest(Constants.ErrorForUser);
        }

        [JwtTokenAuthFilter]
        [HttpPut("post-status/{id:long}")]
        public async Task<ActionResult> PutPublishStatus(long id, [FromBody] PostDetailVM request)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(Constants.InvalidArgument);

                var result = await _postService.PublishPostAsync(id, request.Published);

                if (result)
                    return NoContent();
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(PutPublishStatus));
                return BadRequest(Constants.NotFoundResponse);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(PutPublishStatus));
                return StatusCode(500, Constants.ErrorMessage);
            }

            return BadRequest(Constants.ErrorForUser);
        }

        // DELETE api/<PostsController>/5
        [JwtTokenAuthFilter]
        [HttpDelete("{id:long}")]
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
                return StatusCode(500, Constants.ErrorMessage);
            }
        }
    }
}