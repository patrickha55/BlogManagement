using BlogManagement.Common.Models.CategoryVMs;

namespace BlogManagement.Contracts.Services.ClientServices
{
    public interface ICategoryService
    {
        Task<List<CategoryVM>> GetCategoryVMsAsync(int pageNumber = 1, int pageSize = 10);
        Task<CategoryEditVM> GetCategoryEditVMsAsync(string token, long id);
        Task<CategoryVM> GetCategoryVMAsync(string token, long id);
        Task<bool> CreateCategoryAsync(string token, CategoryCreateVM request);
        Task<bool> UpdateCategoryAsync(string token, long id, CategoryEditVM request);
        Task<bool> DeleteCategoryAsync(string token, long id);
        Task<IEnumerable<CategoryVM>> GetCategoriesForSelectListAsync(string token, long? parentId = null);
    }
}
