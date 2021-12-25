using BlogManagement.Common.Models.AuthorVMs;
using BlogManagement.Common.Models.CategoryVMs;
using BlogManagement.Common.Models.PostCommentVMs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Common.Models.PostVMs
{
    /// <summary>
    /// 
    /// </summary>
    public class PostForIndexVM : PostVM
    {
        /// <summary>
        /// 
        /// </summary>
        public AuthorVM Author { get; set; }
        [Display(Name = "Parent post Title")]
        public string ParentTitle { get; set; }
        public string Summary { get; set; }
        public List<PostCommentForIndexVM> PostComments { get; set; }
        public List<CategoryVM> Categories { get; set; }
    }
}
