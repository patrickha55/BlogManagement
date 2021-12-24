using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Data.Entities
{
    /// <summary>
    /// This class represents a tag information that may or may not belongs to a Post.
    /// </summary>
    public class Tag
    {
        public Tag()
        {
            PostTags = new List<PostTag>();
        }

        #region Properties

        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string MetaTitle { get; set; }
        [Required]
        public string Slug { get; set; }

        public string Context { get; set; }

        #endregion

        #region Navigational properties

        /// <summary>
        /// A tag can belongs to one or more Posts.
        /// </summary>
        public ICollection<PostTag> PostTags { get; set; }

        #endregion
    }
    }
