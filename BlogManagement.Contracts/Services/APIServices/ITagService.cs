using BlogManagement.Common.Common;
using BlogManagement.Common.Models.TagVMs;

namespace BlogManagement.Contracts.Services.APIServices
{
    public interface ITagService
    {
        Task<PaginatedList<TagVM>> GetTagVMsAsync(int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<TagVM>> GetAllIdAndNameWithoutPagingAsync();
        Task<TagVM> GetTagVMAsync(long id);
        Task<TagEditVM> GetTagEditVMsAsync(long id);
        Task<bool> CreateTagAsync(TagCreateVM request);
        Task<bool> UpdateTagAsync(long id, TagEditVM request);
        Task<bool> DeleteTagAsync(TagVM tagVM);
        Task<bool> IsTagExist(long id);
    }
}
