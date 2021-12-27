using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Common.Models.PostCommentVMs
{
    public class PostCommentCreateVM
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
        public long? ParentId { get; set; }
#nullable disable
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
