using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.TagVMs;
using BlogManagement.Contracts;
using BlogManagement.Contracts.Services.APIServices;
using BlogManagement.Data.Entities;
using Microsoft.Extensions.Logging;

namespace BlogManagement.Application.Services.APIServices
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TagService> _logger;

        public TagService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<TagService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginatedList<TagVM>> GetTagVMsAsync(int pageNumber = 1, int pageSize = 10)
        {
            PaginatedList<TagVM> tagVMs;

            try
            {
                var pagingRequest = new PagingRequest(pageNumber, pageSize);

                var tags = await _unitOfWork.TagRepository.GetAllAsync(pagingRequest);

                tagVMs = _mapper.Map<PaginatedList<Tag>, PaginatedList<TagVM>>(tags);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetTagVMsAsync));
                throw;
            }

            return tagVMs;
        }

        public async Task<IEnumerable<TagVM>> GetAllIdAndNameWithoutPagingAsync()
        {
            var tagVMs = new List<TagVM>();

            try
            {
                var tags =
                    await _unitOfWork.TagRepository.GetAllTagIdsAndTitles();

                tagVMs = _mapper.Map<List<TagVM>>(tags);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAllIdAndNameWithoutPagingAsync));
                throw;
            }

            return tagVMs;
        }

        public async Task<TagVM> GetTagVMAsync(long id)
        {
            TagVM tagVM;

            try
            {
                var tag = await _unitOfWork.TagRepository.GetAsync(t => t.Id == id);

                tagVM = _mapper.Map<TagVM>(tag);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetTagEditVMsAsync));
                throw;
            }

            return tagVM;
        }

        public async Task<TagEditVM> GetTagEditVMsAsync(long id)
        {
            TagEditVM tagVM;

            try
            {
                var tag = await _unitOfWork.TagRepository.GetAsync(t => t.Id == id);

                tagVM = _mapper.Map<TagEditVM>(tag);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetTagEditVMsAsync));
                throw;
            }

            return tagVM;
        }

        public async Task<bool> CreateTagAsync(TagCreateVM request)
        {
            await using var transaction = await _unitOfWork.Context.Database.BeginTransactionAsync();

            try
            {
                var tag = _mapper.Map<Tag>(request);

                var result = await _unitOfWork.TagRepository.CreateAsync(tag);

                if (result)
                {
                    await _unitOfWork.SaveAsync();
                    await transaction.CommitAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(CreateTagAsync));
                await transaction.RollbackAsync();
                throw;
            }

            return false;
        }

        public async Task<bool> UpdateTagAsync(long id, TagEditVM request)
        {
            await using var transaction = await _unitOfWork.Context.Database.BeginTransactionAsync();

            try
            {
                if (id != request.Id)
                    return false;

                var result = await _unitOfWork.TagRepository.UpdateAsync(_mapper.Map<Tag>(request));

                if (result)
                {
                    await _unitOfWork.SaveAsync();
                    await transaction.CommitAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(UpdateTagAsync));
                await transaction.RollbackAsync();
                throw;
            }

            return false;
        }

        public async Task<bool> DeleteTagAsync(TagVM tagVM)
        {
            try
            {
                var tag = _mapper.Map<Tag>(tagVM);

                var result = await _unitOfWork.TagRepository.DeleteAsync(tag);

                if (result)
                {
                    await _unitOfWork.SaveAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(DeleteTagAsync));
                throw;
            }

            return false;
        }

        public async Task<bool> IsTagExist(long id)
        {
            try
            {
                return await _unitOfWork.TagRepository.IsExistsAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(IsTagExist));
                throw;
            }
        }
    }
}
