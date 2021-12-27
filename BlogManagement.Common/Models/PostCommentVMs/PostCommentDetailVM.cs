using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BlogManagement.Common.Models.AuthorVMs;

namespace BlogManagement.Common.Models.PostCommentVMs
{
    public class PostCommentDetailVM : PostCommentVM
    {
        public PostCommentDetailVM() => ChildPostComments = new List<PostCommentDetailVM>();

        #region Properties

        /// <summary>
        /// Id of a post which this comment belongs to.
        /// </summary>
        public long PostId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// Id of a parent comment which this comment belongs to.
        /// </summary>
#nullable enable
        public long? ParentId { get; set; }
#nullable disable

        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }

        #endregion

        #region Navigational properties

        /// <summary>
        /// 
        /// </summary>
        public AuthorVM User { get; set; }

        /// <summary>
        /// A comment can have many child comments.
        /// </summary>
        public ICollection<PostCommentDetailVM> ChildPostComments { get; set; }
        #endregion
    }
}
