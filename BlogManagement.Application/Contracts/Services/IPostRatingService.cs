using System.Threading.Tasks;
using BlogManagement.Common.Models.PostRatingVMs;

namespace BlogManagement.Application.Contracts.Services
{
    public interface IPostRatingService
    {
        Task<bool> RateAPostAsync(PostRatingVM request, string userName);
    }
}
