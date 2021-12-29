using System;
using AutoMapper;
using BlogManagement.Application.Contracts;
using BlogManagement.Application.Contracts.Services;
using BlogManagement.Common.Models.PostCommentVMs;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using BlogManagement.Common.Common;
using BlogManagement.Data.Entities;

namespace BlogManagement.Application.Services
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

        public async Task<bool> CreatePostCommentAsync(PostCommentCreateVM request, string userName)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetAsync(u => u.UserName == userName);

                var postComment = _mapper.Map<PostComment>(request);

                postComment.UserId = user.Id;
                postComment.CreatedAt = DateTime.Now;

                var result = await _unitOfWork.PostCommentRepository.CreateAsync(postComment);

                if (result)
                {
                    await _unitOfWork.SaveAsync();
                    return true;
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(CreatePostCommentAsync));
                throw;
            }

            return false;
        }

        public async Task<SelectList> GetPostCommentsForSelectListAsync(long? parentId = null)
        {
            var postComments =
                await _unitOfWork.PostCommentRepository.GetAllIdAndNameWithoutPagingAsync();

            return new SelectList(postComments, "Id", "Title", parentId);
        }
    }
}
