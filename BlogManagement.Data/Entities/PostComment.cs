using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Data.Entities
{
    /// <summary>
    /// This class represents a post comment information.
    /// </summary>
    public class PostComment
    {
        public PostComment() => ChildPostComments = new List<PostComment>();

        #region Properties

        public long Id { get; set; }
        /// <summary>
        /// Id of a post which this comment belongs to.
        /// </summary>
        public long PostId { get; set; }
#nullable enable
        /// <summary>
        /// Id of a parent comment which this comment belongs to.
        /// </summary>
        public long? ParentId { get; set; }
#nullable disable
        [Required]
        public string Title { get; set; }
        public byte Published { get; set; }
        public DateTime CreatedAt { get; set; }
#nullable enable
        public DateTime? PublishedAt { get; set; }
#nullable disable
        public string Content { get; set; }

        #endregion

        #region Navigational properties

        /// <summary>
        /// A post which a comment belongs to. 
        /// </summary>
        public Post Post { get; set; }

        /// <summary>
        /// A parent comment of a comment.
        /// </summary>
        public PostComment ParentPostComment { get; set; }

        /// <summary>
        /// A comment can have many child comments.
        /// </summary>
        public ICollection<PostComment> ChildPostComments { get; set; }

        #endregion
    }
}
