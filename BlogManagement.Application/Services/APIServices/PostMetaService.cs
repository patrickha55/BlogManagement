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
    public class PostMetaService : IPostMetasService
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

        public Task<IEnumerable<PostMetaVM>> GetPostMetaVMsWithoutPagingAsync(Expression<Func<Post, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public Task<PostMetaVM> GetPostMetaVMAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<PostMetaEditVM> GetPostMetaEditVMsAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreatePostMetaVMAsync(PostMetaCreateVM request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePostMetaAsync(long id, PostMetaEditVM request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePostMetaAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}
