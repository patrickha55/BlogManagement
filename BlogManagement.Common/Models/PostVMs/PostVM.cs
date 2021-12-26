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

        public string Title { get; set; }

        [Display(Name = "Date Created")]
        public DateTime CreatedAt { get; set; }

        public string ImageUrl { get; set; }
    }
}
