using System.Collections.Generic;
using System.Threading.Tasks;
using BlogManagement.Common.Models.CategoryVMs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogManagement.Application.Contracts.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryVM>> GetCategoryVMsAsync(int pageNumber = 1, int pageSize = 10);
        Task<CategoryEditVM> GetCategoryEditVMsAsync(long id);
        Task<CategoryVM> GetCategoryVMAsync(long id);
        Task<bool> CreateCategoryAsync(CategoryCreateVM request);
        Task<bool> UpdateCategoryAsync(long id, CategoryEditVM request);
        Task<bool> DeleteCategoryAsync(CategoryVM categoryVM);
        Task<SelectList> GetCategoriesForSelectListAsync(long? parentId = null);

        Task<bool> IsCategoryExist(long id);
    }
}
