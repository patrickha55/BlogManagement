using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Common.Models.CategoryVMs
{
    public class CategoryVM
    {
        public long Id { get; set; }
        public string Title { get; set; }

#nullable enable
        /// <summary>
        /// A parent category which a category belongs to.
        /// </summary>
        [Display(Name = "Parent Category")]
        public CategoryVM? ParentCategory { get; set; }
#nullable disable
    }
}
