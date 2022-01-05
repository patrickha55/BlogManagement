using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.PostMetaVMs;
using BlogManagement.Contracts;
using BlogManagement.Contracts.Services.APIServices;
using BlogManagement.Data.Entities;
using Microsoft.Extensions.Logging;
using X.PagedList;

namespace BlogManagement.Application.Services.APIServices
{
    public class PostMetaService : IPostMetaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PostMetaService> _logger;
        private readonly IMapper _mapper;

        public PostMetaService(IUnitOfWork unitOfWork, ILogger<PostMetaService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IPagedList<PostMetaVM>> GetPostMetaVMsAsync(PagingRequest pagingRequest, Expression<Func<PostMeta, bool>> expression = null)
        {
            try
            {
                if (pagingRequest is null)
                    throw new ArgumentNullException(Constants.InvalidArgument);

                var postMetas =
                    await _unitOfWork.PostMetaRepository
                        .GetAllAsync(pagingRequest, expression, null);

                var postMetaVMs = _mapper.Map<IPagedList<PostMetaVM>>(postMetas);

                return postMetaVMs;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostMetaVMsAsync));
                throw;
            }
        }

        public async Task<IEnumerable<PostMetaVM>> GetPostMetaVMsWithoutPagingAsync(Expression<Func<PostMeta, bool>> expression = null)
        {
            try
            {
                var postMetas =
                    await _unitOfWork.PostMetaRepository
                        .GetPostMetasWithoutPagingAsync(expression);

                var postMetaVMs = _mapper.Map<IEnumerable<PostMetaVM>>(postMetas);

                return postMetaVMs;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostMetaVMsWithoutPagingAsync));
                throw;
            }
        }

        public async Task<PostMeta> GetPostMetaAsync(long id)
        {
            try
            {
                return await _unitOfWork.PostMetaRepository
                        .GetAsync(p => p.Id == id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostMetaAsync));
                throw;
            }
        }

        public async Task<PostMetaVM> GetPostMetaVMAsync(long id)
        {
            try
            {
                var postMeta =
                    await _unitOfWork.PostMetaRepository
                        .GetAsync(p => p.Id == id);

                var postMetaVM = _mapper.Map<PostMetaVM>(postMeta);

                return postMetaVM;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostMetaVMAsync));
                throw;
            }
        }

        public async Task<PostMetaEditVM> GetPostMetaEditVMsAsync(long id)
        {
            try
            {
                var postMeta =
                    await _unitOfWork.PostMetaRepository
                        .GetAsync(p => p.Id == id);

                var postMetaVM = _mapper.Map<PostMetaEditVM>(postMeta);

                return postMetaVM;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostMetaEditVMsAsync));
                throw;
            }
        }

        public async Task<PostMetaVM> CreatePostMetaVMAsync(PostMetaCreateVM request)
        {
            await using var transaction = await _unitOfWork.Context.Database.BeginTransactionAsync();
            try
            {
                var postMeta = _mapper.Map<PostMeta>(request);

                var result = await _unitOfWork.PostMetaRepository.CreateAsync(postMeta);

                if (result)
                {
                    await _unitOfWork.SaveAsync();
                    await transaction.CommitAsync();
                    return _mapper.Map<PostMetaVM>(postMeta);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(CreatePostMetaVMAsync));
                await transaction.RollbackAsync();
                throw;
            }

            return null;
        }

        public async Task<bool> UpdatePostMetaAsync(long id, PostMetaEditVM request)
        {
            await using var transaction = await _unitOfWork.Context.Database.BeginTransactionAsync();
            try
            {
                if (id != request.Id)
                    throw new ArgumentException(Constants.InvalidArgument);

                var postMeta = _mapper.Map<PostMeta>(request);

                var result = await _unitOfWork.PostMetaRepository.UpdateAsync(postMeta);

                if (result)
                {
                    await _unitOfWork.SaveAsync();
                    await transaction.CommitAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(UpdatePostMetaAsync));
                await transaction.RollbackAsync();
                throw;
            }

            return false;
        }

        public async Task<bool> DeletePostMetaAsync(PostMeta postMeta)
        {
            await using var transaction = await _unitOfWork.Context.Database.BeginTransactionAsync();
            try
            {

                var result = await _unitOfWork.PostMetaRepository.DeleteAsync(postMeta);

                if (result)
                {
                    await _unitOfWork.SaveAsync();
                    await transaction.CommitAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(DeletePostMetaAsync));
                await transaction.RollbackAsync();
                throw;
            }

            return false;
        }
    }
}
