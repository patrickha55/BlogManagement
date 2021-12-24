namespace BlogManagement.Data.Entities
{
    /// <summary>
    /// This class represents the relationships between a tag and a post.
    /// </summary>
    public class PostTag
    {
        #region Properties

        /// <summary>
        /// Post id which a tag belongs to.
        /// </summary>
        public long PostId { get; set; }
        /// <summary>
        /// Tag id which a tag belongs to.
        /// </summary>
        public long TagId { get; set; }

        #endregion

        #region Navigational properties

        public Post Post { get; set; }
        public Tag Tag { get; set; }

        #endregion
    }
}
