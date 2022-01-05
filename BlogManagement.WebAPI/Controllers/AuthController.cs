using BlogManagement.Common.Common;
using BlogManagement.Common.DTOs.UserDTOs;
using BlogManagement.Contracts.AuthWithJwt;
using BlogManagement.Contracts.Services;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using BlogManagement.Contracts.Services.APIServices;

namespace BlogManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;
        private readonly IAuthManager _authManager;

        public AuthController(
            ILogger<AuthController> logger,
            IUserService userService, IAuthManager authManager)
        {
            _logger = logger;
            _userService = userService;
            _authManager = authManager;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<(IdentityResult, User)>> RegisterAsync([FromBody] UserRegisterDTO userRegisterDTO)
        {
            try
            {
                if (userRegisterDTO is null)
                    return BadRequest(Constants.InvalidArgument);

                var (result, user) = await _userService.RegisterAsync(userRegisterDTO);

                if (result.Succeeded)
                {
                    return Accepted((result, user));
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(RegisterAsync));
            }

            return BadRequest(Constants.ErrorForUser);
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> LoginAsync([FromBody] UserLoginDTO userLoginDTO)
        {
            if (userLoginDTO is null)
                return BadRequest(Constants.InvalidArgument);

            try
            {
                if (!await _authManager.ValidateUserAsync(userLoginDTO))
                    return Unauthorized();

                return Accepted(new Token { JwtToken = await _authManager.CreateTokenAsync() });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(LoginAsync));
            }

            return BadRequest(Constants.ErrorForUser);
        }
    }
}
