using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Common.Models.PostCommentVMs
{
    /// <summary>
    /// 
    /// </summary>
    public class PostCommentEditVM : PostCommentVM
    {
        public long PostId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// Id of a parent comment which this comment belongs to.
        /// </summary>
#nullable enable
        [Display(Name = "Post of this comment")]
        public long? ParentId { get; set; }
#nullable disable
        public string Content { get; set; }
    }
}
