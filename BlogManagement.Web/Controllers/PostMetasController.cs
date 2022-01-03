using System;
using System.Threading.Tasks;
using AutoMapper;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models.PostMetaVMs;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlogManagement.Web.Controllers
{

    [Authorize]
    public class PostMetasController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PostMetasController> _logger;

        public PostMetasController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PostMetasController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: PostMetasController
        [Route("Admins/PostMetas")]
        [Authorize(Roles = Roles.Administrator)]
        public ActionResult Index()
        {
            return View();
        }

        // GET: PostMetasController/Details/5
        [Authorize(Roles = $"{Roles.Administrator}, {Roles.Author}")]
        public ActionResult Details(long id)
        {
            return View();
        }

        // GET: PostMetasController/Create
        [Authorize(Roles = $"{Roles.Administrator}")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostMetasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Administrator}, {Roles.Author}")]
        public async Task<ActionResult> Create(PostMetaCreateVM request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var postMeta = _mapper.Map<PostMeta>(request);
                    var result = await _unitOfWork.PostMetaRepository.CreateAsync(postMeta);

                    if (result)
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
            return View();
        }

        // GET: PostMetasController/Edit/5
        [Authorize(Roles = $"{Roles.Administrator}")]
        public async Task<ActionResult> Edit(long id)
        {
            var postMetaVM = new PostMetaVM();
            try
            {
                if (id <= 0)
                    throw new ArgumentException(Constants.InvalidArgument);

                var postMeta = await _unitOfWork.PostMetaRepository.GetAsync(p => p.Id == id);

                if (postMeta is null)
                    throw new ArgumentException(Constants.InvalidArgument);

                postMetaVM = _mapper.Map<PostMetaVM>(postMeta);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Edit));
            }

            return View(postMetaVM);
        }

        // POST: PostMetasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Administrator}, {Roles.Author}")]
        public async Task<ActionResult> Edit(long id, PostMetaVM request)
        {
            try
            {
                if (id != request.Id)
                    return NotFound();

                if (ModelState.IsValid)
                {
                    var result = await _unitOfWork.PostMetaRepository.UpdateAsync(_mapper.Map<PostMeta>(request));

                    if (result)
                    {
                        await _unitOfWork.SaveAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Edit));
            }
            return View();
        }

        // POST: PostMetasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Administrator}, {Roles.Author}")]
        public ActionResult Delete(long id)
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
    }
}
