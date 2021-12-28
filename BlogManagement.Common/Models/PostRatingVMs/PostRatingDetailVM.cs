using BlogManagement.Common.Models.AuthorVMs;

namespace BlogManagement.Common.Models.PostRatingVMs
{
    public class PostRatingDetailVM : PostRatingVM
    {
        public AuthorVM User { get; set; }
    }
}
