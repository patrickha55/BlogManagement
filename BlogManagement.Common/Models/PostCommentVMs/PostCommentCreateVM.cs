using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Common.Models.PostCommentVMs
{
    /// <summary>
    /// 
    /// </summary>
    public class PostCommentCreateVM
    {
        /// <summary>
        /// 
        /// </summary>
        public long PostId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Id of a parent comment which this comment belongs to.
        /// </summary>
#nullable enable
        public long? ParentId { get; set; }
#nullable disable
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Content { get; set; }
    }
}
