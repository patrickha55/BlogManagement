using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Common.Models.AuthorVMs
{
    public class AuthorVM
    {
        public long Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Author's Full Name")]
        public string FullName => $"{FirstName} {LastName}";
        public string ImageUrl { get; set; }
    }
}
