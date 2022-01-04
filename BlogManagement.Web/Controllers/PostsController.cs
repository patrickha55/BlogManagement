using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.PostCommentVMs;
using BlogManagement.Common.Models.PostVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagement.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly ILogger<PostsController> _logger;
        private readonly Contracts.Services.ClientServices.IPostService _postService;

        public PostsController(
            ILogger<PostsController> logger,
            Contracts.Services.ClientServices.IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }


        // GET: PostsController
        [Authorize(Roles = $"{Roles.Author}, {Roles.Administrator}")]
        public async Task<ActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var postVMs = new List<PostForIndexVM>();

            try
            {
                postVMs = await _postService.GetPostsOfAnAuthorAsync(new PagingRequest(pageNumber, pageSize), User.Identity?.Name);
            }
            catch (Exception e)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Index));
            }

            return View(postVMs);
        }

        /*[Authorize(Roles = Roles.Administrator)]
        // GET: PostsController
        public async Task<IActionResult> AdminIndex(int pageNumber = 1, int pageSize = 10)
        {
            var postVMs = new List<PostForAdminIndexVM>();

            try
            {
                postVMs = await _postService.GetPostForAdminIndexVMsAsync(pageNumber, pageSize);
            }
            catch (Exception e)
            {

                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(AdminIndex));
            }

            return View(postVMs);
        }*/

        // GET: PostsController/Details/5
        public async Task<ActionResult> Details(long id)
        {
            try
            {
                var postDetailVM = await _postService.GetPostDetailVMAsync(id, User.Identity?.Name);

                return View(new Tuple<PostDetailVM, PostCommentCreateVM>(postDetailVM, new PostCommentCreateVM()));
            }
            catch (Exception e)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Details));
            }

            TempData[Constants.Error] = Constants.ErrorMessage;
            return View("~/Views/Home/Index.cshtml");
        }

        /*// GET: PostsController/Create
        [Authorize(Roles = $"{Roles.Author}, {Roles.Administrator}")]
        public async Task<ActionResult> Create()
        {
            var (categories, tags, posts) = await _postService.GetSelectListsForPostCreationAsync();

            ViewBag.Categories = categories;
            ViewBag.Tags = tags;
            ViewBag.Posts = posts;

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
                    var result = await _postService.CreatePostAsync(request, User.Identity?.Name);

                    if (result is not null)
                    {
                        TempData[Constants.Success] = Constants.SuccessMessage;
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            catch (Exception e)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Create));
            }

            var (categories, tags, posts) =
                await _postService.GetSelectListsForPostCreationAsync(
                request.CategoryId,
                request.TagIds,
                request.ParentId);

            ViewBag.Categories = categories;
            ViewBag.Tags = tags;
            ViewBag.Posts = posts;

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

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RateAPost([Bind("PostId, Rating")] PostRatingCreateVM request)
        {
            try
            {
                var result = await _postRatingService.RateAPostAsync(request);

                if (result is not null)
                {
                    TempData[Constants.Success] = Constants.RatingSuccessMessage;
                    return RedirectToAction(nameof(Details), new { id = request.PostId });
                }
            }
            catch (Exception e)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(RateAPost));
            }

            TempData[Constants.Error] = Constants.ErrorMessage;

            return RedirectToAction(nameof(Details), new { id = request.PostId });
        }*/
    }
}
