using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.CategoryVMs;
using BlogManagement.Common.Models.PostCommentVMs;
using BlogManagement.Common.Models.PostVMs;
using BlogManagement.Common.Models.TagVMs;
using BlogManagement.Contracts.Services.ClientServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagement.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly ILogger<PostsController> _logger;
        private readonly IPostService _postService;

        public PostsController(
            ILogger<PostsController> logger,
            IPostService postService)
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

        [Authorize(Roles = Roles.Administrator)]
        // GET: PostsController
        public async Task<IActionResult> AdminIndex(int pageNumber = 1, int pageSize = 10)
        {
            var postVMs = new List<PostForAdminIndexVM>();

            try
            {
                postVMs = await _postService.GetPostForAdminIndexVMsAsync(new PagingRequest(pageNumber, pageSize));
            }
            catch (Exception e)
            {

                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(AdminIndex));
            }

            return View(postVMs);
        }

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

        // GET: PostsController/Create
        [Authorize(Roles = $"{Roles.Author}, {Roles.Administrator}")]
        public async Task<ActionResult> Create()
        {
            try
            {
                var token = HttpContext.Session.GetString(nameof(Token.JwtToken));
                var postRelatedList = await _postService.GetSelectListsForPostCreationAsync(token);

                ViewBag.Categories = new SelectList(postRelatedList.CategoryDTOs, nameof(CategoryVM.Id), nameof(CategoryVM.Title));
                ViewBag.Tags = new SelectList(postRelatedList.TagDTOs, nameof(TagVM.Id), nameof(TagVM.Title));
                ViewBag.Posts = new SelectList(postRelatedList.PostDTOs, nameof(PostVM.Id), nameof(PostVM.Title));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Create));
            }

            return View();
        }

        // POST: PostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Author}, {Roles.Administrator}")]
        public async Task<ActionResult> Create(PostCreateVM request)
        {
            var token = HttpContext.Session.GetString(nameof(Token.JwtToken));
            try
            {
                if (ModelState.IsValid)
                {
                    request.UserName = User.Identity?.Name;
                    var result = await _postService.CreatePostAsync(token, request);

                    if (result)
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

            var postRelatedList = await _postService.GetSelectListsForPostCreationAsync(token);

            ViewBag.Categories =
                new SelectList(postRelatedList.CategoryDTOs, nameof(CategoryVM.Id), nameof(CategoryVM.Title), request.CategoryId);

            ViewBag.Tags =
                new SelectList(postRelatedList.TagDTOs, nameof(TagVM.Id), nameof(TagVM.Title), request.TagIds);

            ViewBag.Posts =
                new SelectList(postRelatedList.PostDTOs, nameof(PostVM.Id), nameof(PostVM.Title), request.ParentId);

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

        /*[HttpPost]
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
