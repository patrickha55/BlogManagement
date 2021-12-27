using System;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BlogManagement.Common.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Constants = BlogManagement.Common.Common.Constants;

namespace BlogManagement.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole<long>> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole<long>> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Display(Name = "First Name")]
            [StringLength(maximumLength: 50, MinimumLength = 1)]
            public string FirstName { get; set; }

            [Display(Name = "Middle Name")]
            [StringLength(maximumLength: 50, MinimumLength = 1)]
            public string MiddleName { get; set; }

            [Display(Name = "Last Name")]
            [StringLength(maximumLength: 50, MinimumLength = 1)]
            public string LastName { get; set; }
            
            [Required]
            [Display(Name = "Username")]
            [StringLength(maximumLength: 100, MinimumLength = 1)]
            public string UserName { get; set; }

            [Required]
            [Display(Name = "Register be become an Author in this blog?")]
            public bool IsAuthor { get; set; }
            
            [Required]
            [Display(Name = "Do you want your account to be public?")]
            public bool IsPublic { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = new User { UserName = Input.UserName, Email = Input.Email };

                #region Fill in the customized user's info

                if(!string.IsNullOrWhiteSpace(Input.FirstName))
                    user.FirstName = Input.FirstName;

                if(!string.IsNullOrWhiteSpace(Input.MiddleName))
                    user.MiddleName = Input.MiddleName;

                if(!string.IsNullOrWhiteSpace(Input.LastName))
                    user.LastName = Input.LastName;

                user.IsPublic = Input.IsPublic;
                #endregion

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    #region Add new user to a user or author role.

                    var isRoleUserExists = await _roleManager.Roles
                        .AnyAsync(r => r.Name == Roles.User);
                    var isRoleAuthorExists = await _roleManager.Roles
                        .AnyAsync(r => r.Name == Roles.Author);

                    if (!isRoleUserExists || !isRoleAuthorExists)
                        throw new KeyNotFoundException(Constants.NoRolesFound + nameof(OnPostAsync));

                    await _userManager.AddToRoleAsync(user, Input.IsAuthor ? Roles.Author : Roles.User);

                    #endregion

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
