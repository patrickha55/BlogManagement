using AutoMapper;
using BlogManagement.Application.Contracts;
using BlogManagement.Application.Contracts.Repositories;
using BlogManagement.Application.Contracts.Services;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.CategoryVMs;
using BlogManagement.Common.Models.PostVMs;
using BlogManagement.Common.Models.TagVMs;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using Constants = BlogManagement.Common.Common.Constants;

namespace BlogManagement.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PostService> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IRepository<CategoryPost> _categoryPostRepository;
        private readonly IRepository<PostTag> _postTagRepository;

        public PostService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PostService> logger,
            UserManager<User> userManager,
            IRepository<CategoryPost> categoryPostRepository,
            IRepository<PostTag> postTagRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _categoryPostRepository = categoryPostRepository;
            _postTagRepository = postTagRepository;
        }
        public async Task<List<PostForIndexVM>> GetPostsForIndexVMsAsync(string userName = null, int pageNumber = 1, int pageSize = 10)
        {
            var postVMs = new List<PostForIndexVM>();

            try
            {
                var pagingRequest = new PagingRequest
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                IPagedList<Post> posts;

                if (userName is null)
                {
                    posts = await _unitOfWork.PostRepository.GetPostsForIndexAsync(pagingRequest);
                }
                else
                {
                    var user = await _userManager.FindByNameAsync(userName);

                    posts = await _unitOfWork.PostRepository.GetPostsForIndexAsync(pagingRequest, p => p.AuthorId == user.Id);
                }

                postVMs = _mapper.Map<List<PostForIndexVM>>(posts);

                foreach (var postVM in postVMs)
                {
                    foreach (var post in posts)
                    {
                        if (postVM.Id != post.Id)
                            continue;

                        postVM.Categories =
                            _mapper.Map<List<CategoryVM>>(
                                post.CategoryPosts
                                    .Select(c => c.Category)
                                );

                        if (post.PostRatings.Any())
                        {
                            postVM.Rating = post.PostRatings
                                .Average(pr => pr.Rating);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostsForIndexVMsAsync));
                throw;
            }

            return postVMs;
        }

        public async Task<List<PostForAdminIndexVM>> GetPostForAdminIndexVMsAsync(int pageNumber = 1, int pageSize = 10)
        {
            var postVMs = new List<PostForAdminIndexVM>();

            try
            {
                var pagingRequest = new PagingRequest
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                var includes = new List<string> { Constants.ChildPosts, Constants.PostRatings };

                var posts =
                    await _unitOfWork.PostRepository
                        .GetAllAsync(pagingRequest, null, includes);

                postVMs = _mapper.Map<List<PostForAdminIndexVM>>(posts);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostForAdminIndexVMsAsync));
                throw;
            }

            return postVMs;
        }

        public async Task<PostDetailVM> GetPostDetailVMAsync(long id, string userName)
        {
            var postDetailVM = new PostDetailVM();
            try
            {
                if (id <= 0 || !await _unitOfWork.PostRepository.IsExistsAsync(id))
                    throw new ArgumentException(Constants.InvalidArgument);

                var post = await _unitOfWork.PostRepository.GetPostDetailsAsync(id);

                postDetailVM = _mapper.Map<PostDetailVM>(post);

                if (post.CategoryPosts.Any())
                {
                    foreach (var categoryPost in post.CategoryPosts)
                    {
                        postDetailVM.Categories.Add(_mapper.Map<CategoryVM>(categoryPost.Category));
                    }
                }

                if (post.PostTags.Any())
                {
                    foreach (var postTag in post.PostTags)
                    {
                        postDetailVM.Tags.Add(_mapper.Map<TagVM>(postTag.Tag));
                    }
                }

                if (post.PostRatings.Any())
                {
                    postDetailVM.Rating = post.PostRatings.Average(pr => pr.Rating);
                }

                postDetailVM.CurrentLoggedInUserRating =
                    postDetailVM.PostRatings
                        .Where(p => p.PostId == postDetailVM.Id && p.User.UserName == userName)
                        .Select(p => p.Rating)
                        .FirstOrDefault();

                await UpdatePostViewCountAsync(post);
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, "{0} {1}", Constants.InvalidArgument, nameof(GetPostDetailVMAsync));
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostDetailVMAsync));
                throw;
            }

            return postDetailVM;
        }

        public Task<PostEditVM> GetPostEditVMsAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreatePostAsync(PostCreateVM request, string userName)
        {
            await using var transaction = await _unitOfWork.Context.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByNameAsync(userName);

                request.AuthorId = user.Id;

                var post = _mapper.Map<Post>(request);

                var createPostResult =
                    await _unitOfWork.PostRepository
                        .CreatePostAsync(post, request.Image);

                if (createPostResult)
                    await _unitOfWork.SaveAsync();

                var createCategoryResult =
                    await _categoryPostRepository
                        .CreateAsync(
                            new CategoryPost
                            {
                                CategoryId = request.CategoryId,
                                PostId = post.Id
                            });

                var createPostTagResult = false;

                foreach (var tagId in request.TagIds)
                {
                    createPostTagResult =
                        await _postTagRepository.CreateAsync(new PostTag { PostId = post.Id, TagId = tagId });
                }

                if (createPostResult && createPostTagResult && createCategoryResult)
                {
                    await _unitOfWork.SaveAsync();
                    await transaction.CommitAsync();
                    return true;
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(CreatePostAsync));
                await transaction.RollbackAsync();
                throw;
            }

            return false;
        }

        public Task<bool> UpdatePostAsync(long id, PostEditVM request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePostAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsPostExistAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<(SelectList categories, SelectList tags, SelectList posts)> GetSelectListsForPostCreationAsync(long? categoryId = null, IEnumerable<long> tagIds = null, long? postId = null)
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllIdAndNameWithoutPagingAsync();
            var tags = await _unitOfWork.TagRepository.GetAllTagIdsAndTitles();
            var posts = await _unitOfWork.PostRepository.GetAllPostIdsAndTitles();

            return
            (
                new SelectList(categories, "Id", "Title", categoryId),
                new SelectList(tags, "Id", "Title", tagIds),
                new SelectList(posts, "Id", "Title", postId)
            );
        }

        public async Task UpdatePostViewCountAsync(Post post)
        {
            post.TotalViewed += 1;
            var result = await _unitOfWork.PostRepository.UpdateAsync(post);

            if (result)
                await _unitOfWork.SaveAsync();
        }
    }
}
