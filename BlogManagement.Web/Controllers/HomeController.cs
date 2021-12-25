using System;
using System.Collections.Generic;
using BlogManagement.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogManagement.Application.Contracts;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models.AuthorVMs;
using BlogManagement.Common.Models.CategoryVMs;
using BlogManagement.Common.Models.PostCommentVMs;
using BlogManagement.Common.Models.PostVMs;

namespace BlogManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            ILogger<HomeController> logger,
            IMapper mapper, 
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = default)
        {
            var postForIndexVMs = new List<PostForIndexVM>();

            try
            {
                var posts = await _unitOfWork.PostRepository.GetAllAsync(new PagingRequest
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                }, new List<string>
                {
                    "PostComments",
                    "User"
                });

                postForIndexVMs = _mapper.Map<List<PostForIndexVM>>(posts);

                foreach (var postVM in postForIndexVMs)
                {
                    foreach (var post in posts)
                    {
                        if (postVM.AuthorId == post.AuthorId)
                            postVM.Author = _mapper.Map<AuthorForIndexVM>(post.User);
                            
                        if (postVM.Id == post.PostComments
                                .Select(p => p.PostId)
                                .FirstOrDefault())
                            postVM.PostComments = _mapper.Map<List<PostCommentForIndexVM>>(post.PostComments);

                        if (postVM.Id == post.CategoryPosts.Select(p => p.PostId).FirstOrDefault())
                        {
                            postVM.Categories = _mapper.Map<List<CategoryForIndexVM>>(post.CategoryPosts.Select(c => c.Category));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(Index));
            }

            return View(postForIndexVMs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
