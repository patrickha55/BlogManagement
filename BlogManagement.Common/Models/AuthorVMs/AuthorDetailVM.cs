using System;
using System.Collections.Generic;
using BlogManagement.Common.Models.PostCommentVMs;
using BlogManagement.Common.Models.PostRatingVMs;
using BlogManagement.Common.Models.PostVMs;

namespace BlogManagement.Common.Models.AuthorVMs
{
    public class AuthorDetailVM : AuthorVM
    {
        public AuthorDetailVM()
        {
            Posts = new List<PostVM>();
            PostUserRatings = new List<PostRatingVM>();
            PostComments = new List<PostCommentVM>();
        } 

        #region Properties

        public string MiddleName { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime RegisteredAt { get; set; }

#nullable enable
        public DateTime? LastLogin { get; set; }
#nullable disable

        public string Intro { get; set; }

        public string Profile { get; set; }

        public bool IsPublic { get; set; }
        public bool IsEnabled { get; set; }

        #endregion

        #region Navigational properties

        /// <summary>
        /// Navigational property to Post entity.
        /// </summary>
        public ICollection<PostVM> Posts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<PostRatingVM> PostUserRatings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<PostCommentVM> PostComments { get; set; }

        #endregion
    }
}
