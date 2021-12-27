using System;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Common.Models.AuthorVMs
{
    public class AuthorAdminIndexVM : AuthorVM
    {
        [Display(Name = "Registered At")]
        public DateTime RegisteredAt { get; set; }

#nullable enable
        [Display(Name = "Last Login")]
        public DateTime? LastLogin { get; set; }
#nullable disable

        [Display(Name = "Account Visibility")]
        public bool IsPublic { get; set; }
    }
}
