using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Common.DTOs.UserDTOs
{
    public class UserLoginDTO
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
