using BlogManagement.Common.Common;
using BlogManagement.Common.Models.CategoryVMs;

namespace BlogManagement.Contracts.Services.APIServices
{
    public interface ICategoryService
    {
        Task<PaginatedList<CategoryVM>> GetCategoryVMsAsync(int pageNumber = 1, int pageSize = 10);
        Task<CategoryEditVM> GetCategoryEditVMsAsync(long id);
        Task<CategoryVM> GetCategoryVMAsync(long id);
        Task<bool> CreateCategoryAsync(CategoryCreateVM request);
        Task<bool> UpdateCategoryAsync(long id, CategoryEditVM request);
        Task<bool> DeleteCategoryAsync(CategoryVM categoryVM);
        Task<IEnumerable<CategoryVM>> GetCategoriesForSelectListAsync(long? parentId = null);

        Task<bool> IsCategoryExist(long id);
        Task<IEnumerable<CategoryVM>> GetAllIdAndNameWithoutPagingAsync();
    }
}
