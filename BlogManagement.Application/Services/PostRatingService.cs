using AutoMapper;
using BlogManagement.Application.Contracts;
using BlogManagement.Application.Contracts.Repositories;
using BlogManagement.Application.Contracts.Services;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models.PostRatingVMs;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BlogManagement.Application.Services
{
    public class PostRatingService : IPostRatingService
    {
        private readonly ILogger<PostRatingService> _logger;
        private readonly IRepository<PostRating> _postRatingRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public PostRatingService(IRepository<PostRating> postRatingRepository, ILogger<PostRatingService> logger, IMapper mapper, UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _postRatingRepository = postRatingRepository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> RateAPostAsync(PostRatingVM request, string userName)
        {
            try
            {
                if (request is null || request.Rating is < 0 or > 5)
                    throw new ArgumentException(Constants.InvalidArgument);

                var postRating = _mapper.Map<PostRating>(request);

                var user = await _userManager.FindByNameAsync(userName);

                postRating.UserId = user.Id;

                var result = await _postRatingRepository.CreateAsync(postRating);

                if (result)
                {
                    await _unitOfWork.SaveAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(RateAPostAsync));
                throw;
            }

            return false;
        }
    }
}
