using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Data.Entities
{
    /// <summary>
    /// This class represents Post meta information in a post.
    /// </summary>
    public class PostMeta
    {
        #region Properties

        public long Id { get; set; }

        /// <summary>
        /// Id of a post which this post belongs to.
        /// </summary>
        public long PostId { get; set; }
        [Required]
        public string Key { get; set; }
        public string Content { get; set; }

        #endregion

        #region Navigational properties

        /// <summary>
        /// A post which this post meta belongs to.
        /// </summary>
        public Post Post { get; set; }

        #endregion
    }
    }
