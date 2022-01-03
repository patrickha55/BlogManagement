using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Common.Models.CategoryVMs
{
    public class CategoryCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Meta Title (Uppercase)")]
        public string MetaTitle { get; set; }
        [Required]
        [Display(Name = "Slug (exp: tag-name-sth)")]
        public string Slug { get; set; }

#nullable enable
        public string? Content { get; set; }

        [Display(Name = "Parent Category (Not required)")]
        public long? ParentId { get; set; }
#nullable disable
    }
}
