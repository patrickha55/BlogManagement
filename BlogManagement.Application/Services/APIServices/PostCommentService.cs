using System;
using System.Threading.Tasks;
using AutoMapper;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models.PostCommentVMs;
using BlogManagement.Contracts;
using BlogManagement.Contracts.Services.APIServices;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace BlogManagement.Application.Services.APIServices
{
    public class PostCommentService : IPostCommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PostCommentService> _logger;

        public PostCommentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PostCommentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PostCommentVM> GetPostCommentVMAsync(long id)
        {
            try
            {
                var post = await _unitOfWork.PostCommentRepository.GetAsync(pc => pc.Id == id);

                return _mapper.Map<PostCommentVM>(post);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostCommentsForSelectListAsync));
                throw;
            }
        }

        public async Task<SelectList> GetPostCommentsForSelectListAsync(long? parentId = null)
        {
            try
            {
                var postComments =
                    await _unitOfWork.PostCommentRepository.GetAllIdAndNameWithoutPagingAsync();

                return new SelectList(postComments, "Id", "Title", parentId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostCommentsForSelectListAsync));
                throw;
            }
        }

        public async Task<PostCommentVM> CreatePostCommentAsync(PostCommentCreateVM request)
        {
            await using var transaction = await _unitOfWork.Context.Database.BeginTransactionAsync();

            try
            {
                var user = await _unitOfWork.UserRepository.GetAsync(u => u.UserName == request.UserName);

                var postComment = _mapper.Map<PostComment>(request);

                postComment.UserId = user.Id;
                postComment.CreatedAt = DateTime.Now;

                var result = await _unitOfWork.PostCommentRepository.CreateAsync(postComment);

                if (result)
                {
                    await _unitOfWork.SaveAsync();
                    await transaction.CommitAsync();
                    return _mapper.Map<PostCommentVM>(postComment);
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(CreatePostCommentAsync));
                await transaction.RollbackAsync();
                throw;
            }

            return null;
        }
    }
}
