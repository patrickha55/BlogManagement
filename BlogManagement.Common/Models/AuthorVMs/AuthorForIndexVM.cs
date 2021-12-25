using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Common.Models.AuthorVMs
{
    public class AuthorForIndexVM
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Display(Name = "Author's Full Name")]
        public string FullName => $"{FirstName} {LastName}";
    }
}
