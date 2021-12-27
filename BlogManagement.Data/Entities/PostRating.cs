namespace BlogManagement.Data.Entities
{
    public class PostRating
    {
        #region Properties

        public long Id { get; set; }
        public long PostId { get; set; }
        public long UserId { get; set; }
        public double Rating { get; set; }

        #endregion

        #region Navigational properties

        public Post Post { get; set; }
        public User User { get; set; }

        #endregion
    }
}
