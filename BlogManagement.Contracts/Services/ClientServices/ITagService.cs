using BlogManagement.Common.Common;
using BlogManagement.Common.Models.TagVMs;

namespace BlogManagement.Contracts.Services.ClientServices
{
    public interface ITagService
    {
        Task<Paginated<TagVM>> GetTagVMsAsync(int pageNumber = 1, int pageSize = 10);
        Task<TagVM> GetTagVMAsync(string token, long id);
        Task<TagEditVM> GetTagEditVMsAsync(string token, long id);
        Task<bool> CreateTagAsync(string token, TagCreateVM request);
        Task UpdateTagAsync(string token, long id, TagEditVM request);
        Task DeleteTagAsync(string token, long id);
        Task<IEnumerable<TagVM>> GetAllIdAndNameWithoutPagingAsync();
    }
}
