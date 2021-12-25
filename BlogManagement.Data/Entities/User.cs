using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BlogManagement.Data.Entities
{
    /// <summary>
    /// This class stores user's information. 
    /// </summary>
    public class User : IdentityUser<long>
    {
        public User() => Posts = new List<Post>();

        #region Properties

        public override long Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime RegisteredAt { get; set; }

#nullable enable
        public DateTime? LastLogin { get; set; }
#nullable disable

        public string Intro { get; set; }

        public string Profile { get; set; }

        #endregion

        #region Navigational properties

        /// <summary>
        /// Navigational property to Post entity.
        /// </summary>
        public ICollection<Post> Posts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<PostRating> PostUserRatings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<PostComment> PostComments { get; set; }

        #endregion
    }
}
