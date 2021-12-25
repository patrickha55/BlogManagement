using BlogManagement.Common.Models.AuthorVMs;
using BlogManagement.Common.Models.CategoryVMs;
using BlogManagement.Common.Models.PostCommentVMs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Common.Models.PostVMs
{
    public class PostForIndexVM
    {
        public long Id { get; set; }
        /// <summary>
        /// Id of a author (User) that this post belongs to.
        /// </summary>
        [Display(Name = "Author's ID")]
        public long AuthorId { get; set; }

        public AuthorForIndexVM Author { get; set; }
#nullable enable
        /// <summary>
        /// Id of a parent of a child post.
        /// </summary>
        [Display(Name = "Parent post ID")]
        public long? ParentId { get; set; }


        [Display(Name = "Parent post Title")]
        public string ParentTitle { get; set; }
#nullable disable

        public string Title { get; set; }

        public string Summary { get; set; }

        public string ImageUrl { get; set; }
#nullable enable
        [Display(Name = "Date Published")]
        public DateTime? PublishedAt { get; set; }
#nullable disable
        public List<PostCommentForIndexVM> PostComments { get; set; }
        public List<CategoryForIndexVM> Categories { get; set; }
    }
}
