using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlogManagement.Common.Common;
using BlogManagement.Common.DTOs.UserDTOs;
using BlogManagement.Contracts.AuthWithJwt;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BlogManagement.Application.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthManager> _logger;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private User _user;

        public AuthManager(
            UserManager<User> userManager, 
            IConfiguration configuration,
            ILogger<AuthManager> logger,
            JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }

        public async Task<bool> ValidateUserAsync(UserLoginDTO request)
        {
            bool validPassword;
            try
            {
                if (request is null)
                    throw new ArgumentNullException(nameof(request),
                        $"Invalid signin attempt. Please check in {nameof(ValidateUserAsync)}");

                _user = await _userManager.FindByNameAsync(request.UserName);

                validPassword = await _userManager.CheckPasswordAsync(_user, request.Password);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(ValidateUserAsync));
                throw;
            }

            return (_user is not null && validPassword);
        }

        public async Task<string> CreateTokenAsync()
        {
            try
            {
                var signingCredentials = GetSigningCredentials();
                var claims = await GetClaimsAsync();
                var token = GenerateTokenOptions(signingCredentials, claims);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(CreateTokenAsync));
                throw;
            }
        }

        public Task<bool> VerifyToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return Task.FromResult(false);

            SecurityToken securityToken;

            try
            {
                _jwtSecurityTokenHandler.ValidateToken(
                    token.Replace("\"", string.Empty), new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetKey())),
                        ValidateLifetime = true,
                        ValidateAudience = false,
                        ValidateIssuer = true,
                        ClockSkew = TimeSpan.Zero
                    },
                    out securityToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(VerifyToken));
                throw;
            }

            return Task.FromResult(securityToken is not null);
        }

        private SigningCredentials GetSigningCredentials()
        {
            try
            {
                var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetKey()));

                return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetSigningCredentials));
                throw;
            }
        }

        private string GetKey()
        {
            var key = Environment.GetEnvironmentVariable("KEY", EnvironmentVariableTarget.Machine);

            if (key == null)
                throw new NullReferenceException(
                    $"Environment variable KEY is null in {nameof(GetSigningCredentials)}.");

            return key;
        }

        private async Task<List<Claim>> GetClaimsAsync()
        {
            try
            {
                if (_user is null) throw new NullReferenceException($"User object is null in {nameof(GetClaimsAsync)}.");

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, _user.UserName)
                };

                var roles = await _userManager.GetRolesAsync(_user);

                claims.AddRange(
                    roles.Select(r => new Claim(ClaimTypes.Role, r))
                );

                return claims;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetClaimsAsync));
                throw;
            }
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
        {
            try
            {
                var jwtSettings = _configuration.GetSection("Jwt");

                int.TryParse(jwtSettings.GetSection("Lifetime").Value, out var minutes);

                var expiration = DateTime.Now.AddMinutes(minutes);

                var token = new JwtSecurityToken(
                    issuer: jwtSettings.GetSection("Issuer").Value,
                    claims: claims,
                    expires: expiration,
                    signingCredentials: signingCredentials
                );

                return token;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetClaimsAsync));
                throw;
            }
        }
    }
}
