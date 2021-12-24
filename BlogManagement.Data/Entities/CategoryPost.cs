namespace BlogManagement.Data.Entities
{
    /// <summary>
    /// This class represents the relationship between Category and Post.
    /// </summary>
    public class CategoryPost
    {
        #region Propeties

        public long CategoryId { get; set; }

        public long PostId { get; set; }

        #endregion

        #region Navigational properties

        public Category Category { get; set; }
        public Post Post { get; set; }

        #endregion
    }
}
