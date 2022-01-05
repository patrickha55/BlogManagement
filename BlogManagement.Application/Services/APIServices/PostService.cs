using AutoMapper;
using BlogManagement.Common.DTOs.PostDTOs;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.CategoryVMs;
using BlogManagement.Common.Models.PostVMs;
using BlogManagement.Common.Models.TagVMs;
using BlogManagement.Contracts;
using BlogManagement.Contracts.Repositories;
using BlogManagement.Contracts.Services.APIServices;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using Constants = BlogManagement.Common.Common.Constants;

namespace BlogManagement.Application.Services.APIServices
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
        public async Task<List<PostForIndexVM>> GetPostsForIndexVMsAsync(PagingRequest pagingRequest, long? authorId = null)
        {
            List<PostForIndexVM> postVMs;

            try
            {
                IPagedList<Post> posts;

                if (authorId is null)
                {
                    posts = await _unitOfWork.PostRepository.GetPostsForIndexAsync(pagingRequest);
                }
                else
                {
                    posts = await _unitOfWork.PostRepository.GetPostsForIndexAsync(pagingRequest, p => p.AuthorId == authorId);
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
                var pagingRequest = new PagingRequest(pageNumber, pageSize);

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

        public async Task<Post> GetPostAsync(long id)
        {
            try
            {
                var post = await _unitOfWork.PostRepository.GetAsync(p => p.Id == id);

                return post;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostAsync));
                throw;
            }
        }

        public async Task<PostVM> GetPostVMAsync(long id)
        {
            PostVM postVM;

            try
            {
                var post = await _unitOfWork.PostRepository.GetAsync(p => p.Id == id);

                postVM = _mapper.Map<PostVM>(post);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostVMAsync));
                throw;
            }

            return postVM;
        }

        public async Task<PostDetailVM> GetPostDetailVMAsync(long id, string userName)
        {
            var postDetailVM = new PostDetailVM();
            try
            {
                var post = await _unitOfWork.PostRepository.GetPostDetailsAsync(id);

                if (post is null) return null;

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

        public async Task<PostVM> CreatePostAsync(PostCreateVM request, string userName)
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

                if (createPostResult is null)
                    return null;

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

                if (createPostResult is not null && createPostTagResult && createCategoryResult)
                {
                    await _unitOfWork.SaveAsync();
                    await transaction.CommitAsync();
                    return _mapper.Map<PostVM>(createPostResult);
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(CreatePostAsync));
                await transaction.RollbackAsync();
                throw;
            }

            return null;
        }

        public async Task<bool> UpdatePostAsync(long id, PostEditVM request)
        {
            await using var transaction = await _unitOfWork.Context.Database.BeginTransactionAsync();
            try
            {
                var post = _mapper.Map<Post>(request);

                var postResult =
                    await _unitOfWork.PostRepository
                        .UpdatePostAsync(post, request.Image);

                if (postResult is false)
                    return false;

                await _unitOfWork.SaveAsync();

                var posts = await _unitOfWork.PostRepository.GetAllPostWithoutPaging(p => p.Id == request.Id,
                    new List<string>() { "CategoryPosts", "PostTags" });

                var categoryPostResult = false;
                var postTagResult = false;

                foreach (var postDetail in posts)
                {
                    foreach (var categoryPost in postDetail.CategoryPosts)
                    {
                        if (categoryPost.CategoryId != request.CategoryId)
                        {
                            await _unitOfWork.CategoryPostRepository.DeleteAsync(categoryPost);

                            categoryPostResult =
                                await _unitOfWork.CategoryPostRepository
                                    .CreateAsync(new CategoryPost
                                    {
                                        CategoryId = request.CategoryId,
                                        PostId = postDetail.Id
                                    });
                        }
                    }

                    foreach (var postTag in postDetail.PostTags)
                    {
                        if (!request.TagIds.Contains(postTag.TagId))
                            await _unitOfWork.PostTagRepository.DeleteAsync(postTag);

                        for (var index = 0; index < request.TagIds.Count; index++)
                        {
                            var tagId = request.TagIds[index];

                            if (tagId == postTag.TagId)
                            {
                                postTagResult = true;
                                continue;
                            }


                            postTagResult =
                                await _postTagRepository.CreateAsync(new PostTag { PostId = post.Id, TagId = tagId });

                            request.TagIds.Remove(tagId);
                        }
                    }
                }

                if (postResult && postTagResult && postTagResult)
                {
                    await _unitOfWork.SaveAsync();
                    await transaction.CommitAsync();
                    return true;
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(UpdatePostAsync));
                await transaction.RollbackAsync();
                throw;
            }

            return false;
        }

        public async Task<bool> DeletePostAsync(Post post)
        {
            await using var transaction = await _unitOfWork.Context.Database.BeginTransactionAsync();
            try
            {
                var result = await _unitOfWork.PostRepository.DeleteAsync(post);

                if (result)
                {
                    await _unitOfWork.SaveAsync();
                    await transaction.CommitAsync();
                    return true;
                }
                //await _unitOfWork.CategoryPostRepository
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(DeletePostAsync));
                await transaction.RollbackAsync();
                throw;
            }

            return false;
        }

        public async Task<bool> IsPostExistAsync(long id)
        {
            return await _unitOfWork.PostRepository.IsExistsAsync(id);
        }

        public async Task<PostRelatedListOfObjectsDTO> GetSelectListsForPostCreationAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllIdAndNameWithoutPagingAsync();
            var tags = await _unitOfWork.TagRepository.GetAllTagIdsAndTitles();
            var posts = await _unitOfWork.PostRepository.GetAllPostIdsAndTitles();

            return
            new PostRelatedListOfObjectsDTO
            {
                CategoryDTOs = _mapper.Map<IEnumerable<CategoryVM>>(categories),
                TagDTOs = _mapper.Map<IEnumerable<TagVM>>(tags),
                PostDTOs = _mapper.Map<IEnumerable<PostVM>>(posts)
            };
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
