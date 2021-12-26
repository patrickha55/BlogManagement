using AutoMapper;
using BlogManagement.Application.Contracts;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.PostVMs;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogManagement.Common.Models.AuthorVMs;
using BlogManagement.Common.Models.CategoryVMs;
using BlogManagement.Common.Models.PostCommentVMs;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace BlogManagement.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PostsController> _logger;
        private readonly UserManager<User> _userManager;

        public PostsController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PostsController> logger,
            UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
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

                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                var posts = await _unitOfWork.PostRepository.GetPostsForIndexAsync(p => p.AuthorId == user.Id, pagingRequest);

                postVMs = _mapper.Map<List<PostForIndexVM>>(posts);

                foreach (var postVM in postVMs)
                {
                    foreach (var post in posts)
                    {
                        if (postVM.AuthorId == post.AuthorId)
                            postVM.Author = _mapper.Map<AuthorForIndexVM>(post.User);

                        if (postVM.Id == post.PostComments
                                .Select(p => p.PostId)
                                .FirstOrDefault())
                        {
                            postVM.PostComments = _mapper.Map<List<PostCommentForIndexVM>>(post.PostComments);
                        }

                        if (postVM.Id == post.CategoryPosts
                                .Select(p => p.PostId)
                                .FirstOrDefault())
                        {
                            postVM.Categories = _mapper.Map<List<CategoryVM>>(post.CategoryPosts.Select(c => c.Category));
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
                        .GetAllAsync(null, pagingRequest, includes);

                postVMs = _mapper.Map<List<PostForAdminIndexVM>>(posts);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(AdminIndex));
            }

            return View(postVMs);
        }

        // GET: PostsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var postVM = new PostDetailVM();
            try
            {
                if (id <= 0 || !await _unitOfWork.PostRepository.IsExistsAsync(id))
                    throw new ArgumentException(Constants.InvalidArgument);


            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, "{0} {1}", Constants.InvalidArgument, nameof(Details));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Details));
            }
            return View();
        }

        // GET: PostsController/Create
        [Authorize(Roles = $"{Roles.Author}, {Roles.Administrator}")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Author}, {Roles.Administrator}")]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Author}, {Roles.Administrator}")]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostsController/Delete/5
        [Authorize(Roles = $"{Roles.Author}, {Roles.Administrator}")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Author}, {Roles.Administrator}")]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public IActionResult AuthorIndex()
        {
            throw new NotImplementedException();
        }
    }
}
