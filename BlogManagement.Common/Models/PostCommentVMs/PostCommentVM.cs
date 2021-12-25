using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BlogManagement.Common.Models.AuthorVMs;

namespace BlogManagement.Common.Models.PostCommentVMs
{
    /// <summary>
    /// TODO: add image url to user
    /// </summary>
    public class PostCommentVM : PostCommentForIndexVM
    { 
        /// <summary>
        /// 
        /// </summary>
        public long UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }

        #region Navigational properties

        public AuthorVM User { get; set; }

        /// <summary>
        /// A comment can have many child comments.
        /// </summary>
        public ICollection<PostCommentVM> ChildPostComments { get; set; }

        #endregion
    }
}
