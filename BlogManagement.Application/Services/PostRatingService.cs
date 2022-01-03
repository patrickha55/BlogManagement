using AutoMapper;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models.PostRatingVMs;
using BlogManagement.Contracts;
using BlogManagement.Contracts.Services;
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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public PostRatingService(
            ILogger<PostRatingService> logger,
            IMapper mapper,
            UserManager<User> userManager,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<PostRatingVM> GetPostRatingVMAsync(long id)
        {
            try
            {
                var postRating = await _unitOfWork.PostRatingRepository.GetAsync(pr => pr.Id == id);

                return _mapper.Map<PostRatingVM>(postRating);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostRatingVMAsync));
                throw;
            }
        }

        public async Task<PostRatingVM> RateAPostAsync(PostRatingCreateVM request)
        {
            try
            {
                var postRating = _mapper.Map<PostRating>(request);

                var user = await _userManager.FindByNameAsync(request.UserName);

                postRating.UserId = user.Id;

                var result = await _unitOfWork.PostRatingRepository.CreateAsync(postRating);

                if (result)
                {
                    await _unitOfWork.SaveAsync();
                    return _mapper.Map<PostRatingVM>(postRating);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(RateAPostAsync));
                throw;
            }

            return null;
        }
    }
}
