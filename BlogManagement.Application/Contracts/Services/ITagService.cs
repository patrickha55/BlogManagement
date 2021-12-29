using BlogManagement.Common.Models.TagVMs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagement.Application.Contracts.Services
{
    public interface ITagService
    {
        Task<List<TagVM>> GetTagVMsAsync(int pageNumber = 1, int pageSize = 10);
        Task<TagVM> GetTagVMAsync(long id);
        Task<TagEditVM> GetTagEditVMsAsync(long id);
        Task<bool> CreateTagAsync(TagCreateVM request);
        Task<bool> UpdateTagAsync(long id, TagEditVM request);
        Task<bool> DeleteTagAsync(long id);
        Task<bool> IsTagExist(long id);
    }
}
