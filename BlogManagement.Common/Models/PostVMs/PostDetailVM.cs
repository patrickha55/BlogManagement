using BlogManagement.Common.Models.AuthorVMs;
using BlogManagement.Common.Models.CategoryVMs;
using BlogManagement.Common.Models.PostCommentVMs;
using BlogManagement.Common.Models.PostMetaVMs;
using BlogManagement.Common.Models.PostRatingVMs;
using BlogManagement.Common.Models.TagVMs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Common.Models.PostVMs
{
    /// <summary>
    /// 
    /// </summary>
    public class PostDetailVM : PostVM
    {
        #region Properties
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
        public double? CurrentLoggedInUserRating { get; set; }
#nullable disable

        #endregion
        /// <summary>
        /// 
        /// </summary>
        public AuthorVM User { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<TagVM> Tags { get; set; } = new();
        /// <summary>
        /// 
        /// </summary>
        public List<PostMetaDetailVM> PostMetas { get; set; } = new();
        /// <summary>
        /// 
        /// </summary>
        public List<PostCommentDetailVM> PostComments { get; set; } = new();
        /// <summary>
        /// 
        /// </summary>
        public List<CategoryVM> Categories { get; set; } = new();
        /// <summary>
        /// 
        /// </summary>
        public List<PostRatingDetailVM> PostRatings { get; set; } = new();
    }
}
