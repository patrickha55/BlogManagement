using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.CategoryVMs;
using BlogManagement.Contracts;
using BlogManagement.Contracts.Services.APIServices;
using BlogManagement.Data.Entities;
using Microsoft.Extensions.Logging;

namespace BlogManagement.Application.Services.APIServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CategoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginatedList<CategoryVM>> GetCategoryVMsAsync(int pageNumber = 1, int pageSize = 10)
        {
            PaginatedList<CategoryVM> categoryVMs;

            try
            {
                var pagingRequest = new PagingRequest(pageNumber, pageSize);

                var includes = new List<string> { "ParentCategory" };

                var categories =
                    await _unitOfWork.CategoryRepository
                        .GetAllAsync(pagingRequest, null, includes);

                categoryVMs = _mapper.Map<PaginatedList<CategoryVM>>(categories);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetCategoryVMsAsync));
                throw;
            }

            return categoryVMs;
        }

        public async Task<CategoryEditVM> GetCategoryEditVMsAsync(long id)
        {
            var categoryVM = new CategoryEditVM();

            try
            {
                if (id <= 0)
                    throw new ArgumentException(Constants.InvalidArgument);

                var category = await _unitOfWork.CategoryRepository.GetAsync(t => t.Id == id);

                if (category is null)
                    throw new ArgumentException(Constants.InvalidArgument);

                categoryVM = _mapper.Map<CategoryEditVM>(category);
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, "{0} {1}", Constants.InvalidArgument, nameof(GetCategoryEditVMsAsync));
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetCategoryEditVMsAsync));
                throw;
            }

            return categoryVM;
        }

        public async Task<CategoryVM> GetCategoryVMAsync(long id)
        {
            CategoryVM categoryVM;

            try
            {
                if (id <= 0)
                    throw new ArgumentException(Constants.InvalidArgument);

                var category = await _unitOfWork.CategoryRepository.GetAsync(t => t.Id == id);

                if (category is null)
                    throw new ArgumentException(Constants.InvalidArgument);

                categoryVM = _mapper.Map<CategoryVM>(category);
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, "{0} {1}", Constants.InvalidArgument, nameof(GetCategoryEditVMsAsync));
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetCategoryEditVMsAsync));
                throw;
            }

            return categoryVM;
        }

        public async Task<bool> CreateCategoryAsync(CategoryCreateVM request)
        {
            await using var transaction = await _unitOfWork.Context.Database.BeginTransactionAsync();

            try
            {
                var category = _mapper.Map<Category>(request);
                var result = await _unitOfWork.CategoryRepository.CreateAsync(category);

                if (result)
                {
                    await _unitOfWork.SaveAsync();
                    await transaction.CommitAsync();
                    return true;
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(CreateCategoryAsync));
                await transaction.RollbackAsync();
            }

            return false;
        }

        public async Task<bool> UpdateCategoryAsync(long id, CategoryEditVM request)
        {
            try
            {
                if (id != request.Id)
                    return false;

                var catogory = _mapper.Map<Category>(request);

                var result = await _unitOfWork.CategoryRepository.UpdateAsync(catogory);

                if (result)
                {
                    await _unitOfWork.SaveAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(UpdateCategoryAsync));
                throw;
            }

            return false;
        }

        public async Task<bool> DeleteCategoryAsync(CategoryVM categoryVM)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryVM);

                var result = await _unitOfWork.CategoryRepository.DeleteAsync(category);

                if (result) await _unitOfWork.SaveAsync();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(DeleteCategoryAsync));
            }

            return false;
        }

        public async Task<IEnumerable<CategoryVM>> GetCategoriesForSelectListAsync(long? parentId = null)
        {
            var categories =
                await _unitOfWork.CategoryRepository.GetAllIdAndNameWithoutPagingAsync();

            return _mapper.Map<IEnumerable<CategoryVM>>(categories);
        }

        public async Task<bool> IsCategoryExist(long id)
        {
            try
            {
                return await _unitOfWork.CategoryRepository.IsExistsAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(IsCategoryExist));
                throw;
            }
        }

        public async Task<IEnumerable<CategoryVM>> GetAllIdAndNameWithoutPagingAsync()
        {
            var categoryVMs = new List<CategoryVM>();

            try
            {
                var categories =
                    await _unitOfWork.CategoryRepository.GetAllIdAndNameWithoutPagingAsync();

                categoryVMs = _mapper.Map<List<CategoryVM>>(categories);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAllIdAndNameWithoutPagingAsync));
                throw;
            }

            return categoryVMs;
        }
    }
}
