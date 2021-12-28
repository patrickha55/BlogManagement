using AutoMapper;
using BlogManagement.Application.Contracts;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.CategoryVMs;
using BlogManagement.Common.Models.PostCommentVMs;
using BlogManagement.Common.Models.PostRatingVMs;
using BlogManagement.Common.Models.PostVMs;
using BlogManagement.Common.Models.TagVMs;
using BlogManagement.Data;
using BlogManagement.Data.Entities;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PostsController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IRepository<CategoryPost> _categoryPostRepository;
        private readonly IRepository<PostTag> _postTagRepository;
        private readonly IRepository<PostRating> _postRatingRepository;
        private readonly BlogManagementContext _context;

        public PostsController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PostsController> logger,
            UserManager<User> userManager,
            IRepository<CategoryPost> categoryPostRepository,
            IRepository<PostTag> postTagRepository,
            BlogManagementContext context,
            IRepository<PostRating> postRatingRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _categoryPostRepository = categoryPostRepository;
            _postTagRepository = postTagRepository;
            _context = context;
            _postRatingRepository = postRatingRepository;
        }


        // GET: PostsController
        [Authorize(Roles = $"{Roles.Author}, {Roles.Administrator}")]
        public async Task<ActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var postVMs = new List<PostForIndexVM>();

            try
            {
                var pagingRequest = new PagingRequest
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                var user = await _userManager.FindByNameAsync(User.Identity?.Name);

                var posts = await _unitOfWork.PostRepository.GetPostsForIndexAsync(p => p.AuthorId == user.Id, pagingRequest);

                postVMs = _mapper.Map<List<PostForIndexVM>>(posts);

                foreach (var postVM in postVMs)
                {
                    foreach (var post in posts)
                    {
                        if (postVM.Id != post.Id)
                            continue;

                        postVM.Categories = _mapper.Map<List<CategoryVM>>(post.CategoryPosts.Select(c => c.Category));

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
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Index));
            }

            return View(postVMs);
        }

        [Authorize(Roles = Roles.Administrator)]
        // GET: PostsController
        public async Task<IActionResult> AdminIndex(int pageNumber = 1, int pageSize = 10)
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
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(AdminIndex));
            }

            return View(postVMs);
        }

        // GET: PostsController/Details/5
        public async Task<ActionResult> Details(long id)
        {
            try
            {
                if (id <= 0 || !await _unitOfWork.PostRepository.IsExistsAsync(id))
                    throw new ArgumentException(Constants.InvalidArgument);

                var post = await _unitOfWork.PostRepository.GetPostDetailsAsync(id);

                var postDetailVM = _mapper.Map<PostDetailVM>(post);

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
                    //postDetailVM.PostUserRatings = _mapper.Map<PostRatingDetailVM>(post.PostRatings);
                }

                postDetailVM.CurrentLoggedInUserRating =
                    postDetailVM.PostRatings
                        .Where(p => p.PostId == postDetailVM.Id && p.User.UserName == User.Identity.Name)
                        .Select(p => p.Rating)
                        .FirstOrDefault();

                await UpdatePostViewCount(post);

                return View(new Tuple<PostDetailVM, PostCommentCreateVM>(postDetailVM, new PostCommentCreateVM()));
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, "{0} {1}", Constants.InvalidArgument, nameof(Details));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Details));
            }

            return View("~/Views/Home/Index.cshtml");
        }

        /// <summary>
        /// This method increase a post view count by one.
        /// </summary>
        /// <param name="post">A Post to increase view</param>
        private async Task UpdatePostViewCount(Post post)
        {
            post.TotalViewed += 1;
            var result = await _unitOfWork.PostRepository.UpdateAsync(post);

            if (result)
                await _unitOfWork.SaveAsync();
        }

        // GET: PostsController/Create
        [Authorize(Roles = $"{Roles.Author}, {Roles.Administrator}")]
        public async Task<ActionResult> Create()
        {
            var selectLists = await GetSelectListsForPostCreation();

            ViewBag.Categories = selectLists.categories;
            ViewBag.Tags = selectLists.tags;
            ViewBag.Posts = selectLists.posts;

            return View();
        }

        // POST: PostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Author}, {Roles.Administrator}")]
        public async Task<ActionResult> Create(PostCreateVM request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByNameAsync(User.Identity?.Name);

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

                    if (createPostTagResult && createCategoryResult)
                    {
                        await _unitOfWork.SaveAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Create));
            }

            var selectLists = await GetSelectListsForPostCreation(request.CategoryId, request.TagIds, request.ParentId);

            ViewBag.Categories = selectLists.categories;
            ViewBag.Tags = selectLists.tags;
            ViewBag.Posts = selectLists.posts;

            return View(request);
        }

        // GET: PostsController/Edit/5
        public ActionResult Edit(long id)
        {
            throw new NotImplementedException();
        }

        // POST: PostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Author}, {Roles.Administrator}")]
        public ActionResult Edit(long id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        // GET: PostsController/Delete/5
        [Authorize(Roles = $"{Roles.Author}, {Roles.Administrator}")]
        public ActionResult Delete(long id)
        {
            throw new NotImplementedException();
        }

        // POST: PostsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Author}, {Roles.Administrator}")]
        public ActionResult Delete(long id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw new NotImplementedException();
            }
        }


        public IActionResult AdminIndex()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RateAPost([Bind("PostId, Rating")] PostRatingVM request)
        {
            try
            {
                if (request is null || request.Rating is < 0 or > 5)
                    throw new ArgumentException(Constants.InvalidArgument);

                var postRating = _mapper.Map<PostRating>(request);

                var user = await _userManager.FindByNameAsync(User.Identity?.Name);

                postRating.UserId = user.Id;

                var result = await _postRatingRepository.CreateAsync(postRating);

                if (result)
                {
                    TempData["Status"] = "Post rated successfully!";
                    await _unitOfWork.SaveAsync();
                    return RedirectToAction(nameof(Details), new { id = request.PostId });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(RateAPost));
            }

            TempData["Status"] = "Post rated fail!";

            return RedirectToAction(nameof(Details), new { id = request.PostId });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<(SelectList categories, SelectList tags, SelectList posts)> GetSelectListsForPostCreation(long? categoryId = null, [CanBeNull] IEnumerable<long> tagIds = null, long? postId = null)
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
    }
}
