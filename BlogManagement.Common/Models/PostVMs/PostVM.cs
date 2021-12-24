using System;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Common.Models.PostVMs
{
    public class PostVM
    {
        public long Id { get; set; }
        /// <summary>
        /// Id of a author (User) that this post belongs to.
        /// </summary>
        [Display(Name = "Author's ID")]
        public long AuthorId { get; set; }
#nullable enable
        /// <summary>
        /// Id of a parent of a child post.
        /// </summary>
        [Display(Name = "Parent post ID")]
        public long? ParentId { get; set; }
#nullable disable

        public string Title { get; set; }

        [Display(Name = "Meta Title")]
        public string MetaTitle { get; set; }

        public string Slug { get; set; }

        public string Summary { get; set; }

        public byte Published { get; set; }

        [Display(Name = "Date Created")]
        public DateTime CreatedAt { get; set; }
#nullable enable
        [Display(Name = "Date Published")]
        public DateTime? PublishedAt { get; set; }
#nullable disable
    }
}
