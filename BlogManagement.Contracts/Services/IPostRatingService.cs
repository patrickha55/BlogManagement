using BlogManagement.Common.Models.PostRatingVMs;

namespace BlogManagement.Contracts.Services
{
    public interface IPostRatingService
    {
        Task<PostRatingVM> GetPostRatingVMAsync(long id);

        Task<PostRatingVM> RateAPostAsync(PostRatingCreateVM request);
    }
}
