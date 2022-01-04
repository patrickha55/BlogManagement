using BlogManagement.Common.Models.TagVMs;

namespace BlogManagement.Contracts.Services
{
    public interface ITagService
    {
        Task<List<TagVM>> GetTagVMsAsync(int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<TagVM>> GetAllIdAndNameWithoutPagingAsync();
        Task<TagVM> GetTagVMAsync(long id);
        Task<TagEditVM> GetTagEditVMsAsync(long id);
        Task<bool> CreateTagAsync(TagCreateVM request);
        Task<bool> UpdateTagAsync(long id, TagEditVM request);
        Task<bool> DeleteTagAsync(TagVM tagVM);
        Task<bool> IsTagExist(long id);
    }
}
