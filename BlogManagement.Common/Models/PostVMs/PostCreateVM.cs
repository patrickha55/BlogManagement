using BlogManagement.Common.Models.CategoryVMs;
using BlogManagement.Common.Models.PostMetaVMs;
using BlogManagement.Common.Models.TagVMs;
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
        public long AuthorId { get; set; }
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
        [Required]
        public string Title { get; set; }

        [Display(Name = "Meta Title (Uppercase)")]
        [Required]
        public string MetaTitle { get; set; }

        [Required]
        [Display(Name = "Slug (Ex: slug-name-for-url")]
        public string Slug { get; set; }

        [Required]
        public string Summary { get; set; }

        [Required]
        public string Content { get; set; }

        public IFormFile Image { get; set; }
    }
}
