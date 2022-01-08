using BlogManagement.Common.Models.PostRatingVMs;
using JetBrains.Annotations;

namespace BlogManagement.Contracts.Services.APIServices
{
    public interface IPostRatingService
    {
        [ItemCanBeNull] Task<PostRatingVM> GetPostRatingVMAsync(long id);

        Task<PostRatingVM> RateAPostAsync(PostRatingCreateVM request);
    }
}
