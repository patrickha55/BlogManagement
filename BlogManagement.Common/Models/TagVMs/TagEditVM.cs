using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Common.Models.TagVMs
{
    /// <summary>
    /// 
    /// </summary>
    public class TagEditVM : TagVM
    {
        [Required]
        [Display(Name = "Meta Title (Uppercase)")]
        public string MetaTitle { get; set; }
        [Required]
        [Display(Name = "Slug (exp: tag-name-sth)")]
        public string Slug { get; set; }

        public string Context { get; set; }
    }
}
