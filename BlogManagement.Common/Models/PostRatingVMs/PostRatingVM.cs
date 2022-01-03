namespace BlogManagement.Common.Models.PostRatingVMs
{
    public class PostRatingVM
    {
        public long Id { get; set; }
        public long PostId { get; set; }
        public double Rating { get; set; }
    }
}
