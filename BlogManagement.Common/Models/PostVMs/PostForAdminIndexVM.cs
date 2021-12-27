using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BlogManagement.Common.Models.PostRatingVMs;

namespace BlogManagement.Common.Models.PostVMs
{
    public class PostForAdminIndexVM : PostVM
    {

        [Display(Name = "Is Published?")]
        public byte Published { get; set; }
#nullable enable
        [Display(Name = "Date Published")]
        public DateTime? PublishedAt { get; set; }
#nullable disable
        [Display(Name = "Parent post Title")]
        public string ParentTitle { get; set; }
        /// <summary>
        /// A property represent a child post parent ID.
        /// </summary>
        [Display(Name = "Parent's ID")]
        public long ParentId { get; set; }
        /// <summary>
        /// A post can have many child posts.
        /// </summary>
        [Display(Name = "Number of child posts")]
        public IEnumerable<PostVM> ChildPosts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Rating")]
        public IEnumerable<PostRatingVM> PostRatings { get; set; }
    }
}
