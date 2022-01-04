using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Common.DTOs.UserDTOs
{
    public class UserRegisterDTO
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
    }
}
