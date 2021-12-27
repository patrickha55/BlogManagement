using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BlogManagement.Common.Models.AuthorVMs;
using BlogManagement.Common.Models.CategoryVMs;
using BlogManagement.Common.Models.PostCommentVMs;
using BlogManagement.Common.Models.PostMetaVMs;
using BlogManagement.Common.Models.TagVMs;

namespace BlogManagement.Common.Models.PostVMs
{
    public class PostDetailVM : PostVM
    {
        /// <summary>
        /// 
        /// </summary>
        public AuthorVM Author { get; set; }
        [Display(Name = "Parent post Title")]
        public string ParentTitle { get; set; }
        public string Summary { get; set; }

        [Display(Name = "Meta Title")]
        public string MetaTitle { get; set; }

        public string Slug { get; set; }
        public string Content { get; set; }
#nullable enable
        [Display(Name = "Date Published")]
        public DateTime? PublishedAt { get; set; }
        public double? Rating { get; set; }
#nullable disable
        public List<TagVM> Tags { get; set; } = new();
        public List<PostMetaDetailVM> PostMetas { get; set; } = new();
        public List<PostCommentDetailVM> PostComments { get; set; } = new();
        public List<CategoryVM> Categories { get; set; } = new();
    }
}
