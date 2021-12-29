using BlogManagement.Application.Contracts.Services;
using BlogManagement.Common.Models.CategoryVMs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlogManagement.Application.Contracts;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.TagVMs;
using BlogManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlogManagement.Application.Services
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

        public async Task<List<TagVM>> GetTagVMsAsync(int pageNumber = 1, int pageSize = 10)
        {
            var tagVMs = new List<TagVM>();

            try
            {
                var pagingRequest = new PagingRequest
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                var tags = await _unitOfWork.TagRepository.GetAllAsync(pagingRequest);

                tagVMs = _mapper.Map<List<TagVM>>(tags);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetTagVMsAsync));
                throw;
            }

            return tagVMs;
        }

        public Task<TagVM> GetTagVMAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<TagEditVM> GetTagEditVMsAsync(long id)
        {
            var tagVM = new TagEditVM();
            try
            {
                if (id <= 0)
                    throw new ArgumentException(Constants.InvalidArgument);

                var tag = await _unitOfWork.TagRepository.GetAsync(t => t.Id == id);

                if (tag is null)
                    throw new ArgumentException(Constants.InvalidArgument);

                tagVM = _mapper.Map<TagEditVM>(tag);
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, "{0} {1}", Constants.InvalidArgument, nameof(GetTagEditVMsAsync));
                throw;
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
            try
            {
                var tag = _mapper.Map<Tag>(request);
                var result = await _unitOfWork.TagRepository.CreateAsync(tag);

                if (result)
                {
                    await _unitOfWork.SaveAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(CreateTagAsync));
                throw;
            }

            return false;
        }

        public async Task<bool> UpdateTagAsync(long id, TagEditVM request)
        {
            try
            {
                if (id != request.Id)
                    return false;

                var result = await _unitOfWork.TagRepository.UpdateAsync(_mapper.Map<Tag>(request));

                if (result)
                {
                    await _unitOfWork.SaveAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(UpdateTagAsync));
                throw;
            }

            return false;
        }

        public async Task<bool> DeleteTagAsync(long id)
        {
            try
            {
                if (id <= 0)
                    return false;

                var tag = await _unitOfWork.TagRepository.GetAsync(t => t.Id == id);

                if (tag is null) return false;

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
