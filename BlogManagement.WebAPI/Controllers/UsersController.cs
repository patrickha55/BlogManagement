using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.AuthorVMs;
using BlogManagement.Contracts.Services.APIServices;
using BlogManagement.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlogManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            IUserService userService,
            ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [JwtTokenAuthFilter]
        [HttpGet("users-admin-page")]
        public async Task<ActionResult<List<AuthorAdminIndexVM>>> GetAuthorAdminIndexVM([FromQuery] PagingRequest pagingRequest)
        {
            try
            {
                pagingRequest ??= new PagingRequest();

                var users =
                    await _userService.GetAuthorAdminIndexVM(pagingRequest.PageNumber, pagingRequest.PageSize);
                if (users is not null)
                    return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAuthorAdminIndexVM));
            }

            return NotFound(Constants.ItsEmpty);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorVM>>> FindAuthorVMsAsync([FromQuery] string keyword)
        {
            try
            {
                var authors = await _userService.FindAuthorVMsAsync(keyword);

                if (authors is not null)
                    return Ok(authors);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(FindAuthorVMsAsync));
            }

            return NotFound(Constants.NotFoundResponse);
        }

        [HttpGet("details/{id:long}")]
        public async Task<ActionResult<AuthorDetailVM>> GetAuthorDetailVMAsync(long id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(Constants.InvalidArgument);

                var authors = await _userService.FindAuthorDetailVMAsync(id);

                if (authors is not null)
                    return Ok(authors);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAuthorDetailVMAsync));
            }

            return NotFound(Constants.NotFoundResponse);
        }

        [JwtTokenAuthFilter]
        [HttpPut("status/{id:long}")]
        public async Task<ActionResult> EditUserStatusesAsync(long id, AuthorDetailVM request)
        {
            try
            {
                if (id <= 0 || !ModelState.IsValid)
                    return BadRequest(Constants.InvalidArgument);

                var result = await _userService.EditUserStatusesAsync(id, request);

                if (result)
                    return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAuthorDetailVMAsync));
            }

            return NotFound(Constants.NotFoundResponse);
        }
    }
}
