using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Common.Models.TagVMs
{
    /// <summary>
    /// 
    /// </summary>
    public class TagVM
    {
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
