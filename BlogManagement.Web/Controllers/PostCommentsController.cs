using BlogManagement.Application.Contracts.Services;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models.PostCommentVMs;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using AutoMapper;
using BlogManagement.Application.Contracts;

namespace BlogManagement.Web.Controllers
{
    public class PostCommentsController : Controller
    {
        private readonly ILogger<PostCommentsController> _logger;
        private readonly IPostCommentService _postCommentService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostCommentsController(
            ILogger<PostCommentsController> logger,
            IPostCommentService postCommentService,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _postCommentService = postCommentService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize(Roles = Roles.Administrator)]
        // GET: PostCommentsController
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = Roles.Administrator)]
        // GET: PostCommentsController/Details/5
        public ActionResult Details(long id)
        {
            return View();
        }

        // GET: PostCommentsController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.PostComments = await _postCommentService.GetPostCommentsForSelectListAsync();
            return View();
        }

        // POST: PostCommentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("PostId, Title, Content")] PostCommentCreateVM request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _postCommentService.CreatePostCommentAsync(request, User.Identity?.Name);

                    if (result)
                    {
                        TempData[Constants.Success] = Constants.SuccessMessage;
                        return RedirectToRoute($"Posts/{request.PostId}");
                    }
                }

            }
            catch (Exception e)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Create));
            }

            ViewBag.PostComments = await _postCommentService.GetPostCommentsForSelectListAsync(request.ParentId);
            return RedirectToRoute($"Posts/{request.PostId}");
        }

        // GET: PostCommentsController/Edit/5
        public async Task<ActionResult> Edit(long id)
        {
            var postCommentVM = new PostCommentEditVM();
            try
            {
                if (id <= 0)
                    throw new ArgumentException(Constants.InvalidArgument);

                var postComment = await _unitOfWork.PostCommentRepository.GetAsync(pc => pc.Id == id);

                if (postComment is null)
                    throw new ArgumentException(Constants.InvalidArgument);

                ViewBag.PostComments = await _postCommentService.GetPostCommentsForSelectListAsync(postComment.ParentId);

                postCommentVM = _mapper.Map<PostCommentEditVM>(postComment);
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, "{0} {1}", Constants.InvalidArgument, nameof(Edit));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Edit));
            }
            return View(postCommentVM);
        }

        // POST: PostCommentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(long id, PostCommentEditVM request)
        {
            try
            {
                if (id != request.Id)
                    return NotFound();

                if (ModelState.IsValid)
                {
                    var result = await _unitOfWork.PostCommentRepository.UpdateAsync(_mapper.Map<PostComment>(request));

                    if (result)
                    {
                        await _unitOfWork.SaveAsync();
                        return RedirectToRoute($"Posts/{request.PostId}");
                    }
                }
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!await _unitOfWork.CategoryRepository.IsExistsAsync(id))
                    return NotFound();

                _logger.LogInformation(e, "{0} {1}", Constants.ObjectAlreadyExists, nameof(Edit));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Edit));
            }

            ViewBag.PostComments = await _postCommentService.GetPostCommentsForSelectListAsync(request.ParentId);

            return View(request);
        }

        // POST: PostCommentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(long id)
        {
            long postId = 0L;
            try
            {
                var user = await _unitOfWork.UserRepository.GetAsync(u => u.UserName == User.Identity.Name);

                if (id <= 0)
                    return NotFound();

                var postComment = await _unitOfWork.PostCommentRepository.GetAsync(pc => pc.Id == id);

                if (postComment is null || postComment.UserId != user.Id) return NotFound();

                postId = postComment.PostId;

                var result = await _unitOfWork.PostCommentRepository.DeleteAsync(postComment);

                if (result) await _unitOfWork.SaveAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Delete));
            }

            return RedirectToRoute($"Posts/{postId}");
        }
    }
}
