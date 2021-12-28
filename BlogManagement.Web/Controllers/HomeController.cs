﻿using AutoMapper;
using BlogManagement.Application.Contracts;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.AuthorVMs;
using BlogManagement.Common.Models.CategoryVMs;
using BlogManagement.Common.Models.PostVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Exception = System.Exception;

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

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var postForIndexVMs = new List<PostForIndexVM>();

            try
            {
                var pagingRequest = new PagingRequest
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                var posts = await _unitOfWork.PostRepository.GetPostsForIndexAsync(p => p.Published != 0, pagingRequest);

                postForIndexVMs = _mapper.Map<List<PostForIndexVM>>(posts);

                foreach (var postVM in postForIndexVMs)
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
                _logger.LogError(e, "Error: {0} {1}", Constants.ErrorMessageLogging, nameof(Index));
            }

            return View(postForIndexVMs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchAnything(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                    throw new ArgumentNullException(Constants.InvalidArgument);

                var users = await _unitOfWork.UserRepository.FindUsersAsync(u => (
                    u.UserName.Contains(keyword) || u.FirstName.Contains(keyword) || u.Email.Contains(keyword)) && u.IsPublic == true);

                if (users == null)
                    return RedirectToAction("Index");

                ViewData["Users"] = _mapper.Map<IEnumerable<AuthorVM>>(users);
                return View("~/Views/Home/SearchAnything.cshtml");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(SearchAnything));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
