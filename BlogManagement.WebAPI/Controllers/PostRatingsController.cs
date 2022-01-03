using System;
using System.Threading.Tasks;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models.PostRatingVMs;
using BlogManagement.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlogManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostRatingsController : ControllerBase
    {
        private readonly IPostRatingService _postRatingService;
        private readonly ILogger<PostRatingsController> _logger;

        public PostRatingsController(
            IPostRatingService postRatingService,
            ILogger<PostRatingsController> logger)
        {
            _postRatingService = postRatingService;
            _logger = logger;
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PostRatingVM>> GetAsync(long id)
        {
            PostRatingVM postRatingVM = null;

            try
            {
                postRatingVM = await _postRatingService.GetPostRatingVMAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAsync));
            }

            return postRatingVM is null ?
                NotFound(Constants.NotFoundResponse) : postRatingVM;
        }

        [HttpPost]
        public async Task<ActionResult<PostRatingVM>> PostAsync(PostRatingCreateVM request)
        {
            try
            {
                if (request is null)
                    return BadRequest(Constants.InvalidArgument);

                var postRatingVM = await _postRatingService.RateAPostAsync(request);

                if (postRatingVM is null)
                    return BadRequest(Constants.ErrorForUser);

                return CreatedAtAction("Get", new {id = postRatingVM.Id}, postRatingVM);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(PostAsync));
            }

            return BadRequest(Constants.ErrorForUser);
        }

    }
}
