using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Common.Models.PostVMs
{
    /// <summary>
    /// 
    /// </summary>
    public class PostCreateVM
    {
        /// <summary>
        /// Id of a author (User) that this post belongs to.
        /// </summary>
        public long? AuthorId { get; set; }

        public string UserName { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Meta Title")]
        public string MetaTitle { get; set; }

        [Required]
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        [Required]
        public string Summary { get; set; }

        [Required]
        public string Content { get; set; }

        public IFormFile Image { get; set; }
#nullable enable
        /// <summary>
        /// Id of a parent of a child post.
        /// </summary>
        [Display(Name = "Parent post")]
        public long? ParentId { get; set; }
#nullable disable
        [Display(Name = "Category")]
        public long CategoryId { get; set; }
        [Display(Name = "Tags")]
        public List<long> TagIds { get; set; }
    }
}
