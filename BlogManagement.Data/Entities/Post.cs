using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Data.Entities
{
    /// <summary>
    /// This class stores post information.
    /// </summary>
    public class Post : BaseEntity
    {
        public Post()
        {
            ChildPosts = new List<Post>();
            PostMetas = new List<PostMeta>();
            PostComments = new List<PostComment>();
            CategoryPosts = new List<CategoryPost>();
            PostTags = new List<PostTag>();
        }

        #region Properties

        /// <summary>
        /// Id of a author (User) that this post belongs to.
        /// </summary>
        public long AuthorId { get; set; }
#nullable enable
        /// <summary>
        /// Id of a parent of a child post.
        /// </summary>
        public long? ParentId { get; set; }
#nullable disable

        [Required]
        public string Title { get; set; }

        public string MetaTitle { get; set; }

        [Required]
        public string Slug { get; set; }

        public string Summary { get; set; }
        
        [Range(1, maximum: 2)]
        public byte Published { get; set; }

#nullable enable
        public DateTime? PublishedAt { get; set; }
#nullable disable

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public int TotalViewed { get; set; }
        #endregion

        #region Navigational Properties

        /// <summary>
        /// A post belong to a User (Author of a post)
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// A post property represent a child post parent.
        /// </summary>
        public Post ParentPost { get; set; }

        /// <summary>
        /// A post can have many child posts.
        /// </summary>
        public List<Post> ChildPosts { get; set; }
        
        /// <summary>
        /// A post can have many post metas.
        /// </summary>
        public List<PostMeta> PostMetas { get; set; }

        /// <summary>
        /// A post can have many comments.
        /// </summary>
        public List<PostComment> PostComments { get; set; }

        /// <summary>
        /// A post belongs to one or many Categories.
        /// </summary>
        public List<CategoryPost> CategoryPosts { get; set; }

        /// <summary>
        /// A post can have many Tags.
        /// </summary>
        public List<PostTag> PostTags { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<PostRating> PostRatings { get; set; }

        #endregion
    }
}
