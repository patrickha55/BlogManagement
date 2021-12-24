using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Data.Entities
{
    /// <summary>
    /// This class represents a category information and its relationships.
    /// </summary>
    public class Category
    {
        public Category()
        {
            ChildCategories = new List<Category>();
            CategoryPosts = new List<CategoryPost>();
        }

        #region Properties

        public long Id { get; set; }
        /// <summary>
        /// Id of a parent category which a category belongs to.
        /// </summary>
        public long ParentId { get; set; }
        [Required]
        public string Title { get; set; }
        public string MetaTitle { get; set; }

        [Required]
        public string Slug { get; set; }
        public string Content { get; set; }

        #endregion

        #region Navigational properties

        /// <summary>
        /// A parent category which a category belongs to.
        /// </summary>
        public Category ParentCategory { get; set; }

        /// <summary>
        /// A category can have many child categories.
        /// </summary>
        public ICollection<Category> ChildCategories { get; set; }

        /// <summary>
        /// A category have one or many Posts.
        /// </summary>
        public ICollection<CategoryPost> CategoryPosts { get; set; }

        #endregion
    }
}
