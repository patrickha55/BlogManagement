using BlogManagement.Common.Common;
using BlogManagement.Common.DTOs.PostDTOs;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.CategoryVMs;
using BlogManagement.Common.Models.PostVMs;
using BlogManagement.Common.Models.TagVMs;
using BlogManagement.Data.Entities;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogManagement.Contracts.Services.ClientServices
{
    public interface IPostService
    {
        /// <summary>
        /// This method gets all posts for index view model as well as an option to get all posts of an author.
        /// </summary>
        /// <param name="pagingRequest">PagingRequest object for paging</param>
        /// <param name="authorId">User's ID</param>
        /// <returns>List of PostForIndexVMs</returns>
        Task<List<PostForIndexVM>> GetPostsForIndexVMsAsync(PagingRequest pagingRequest);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagingRequest"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<List<PostForIndexVM>> GetPostsOfAnAuthorAsync(PagingRequest pagingRequest, string userName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pagingRequest"></param>
        /// <returns></returns>
        Task<List<PostForIndexVM>> FindPostsAsync(string keyword, PagingRequest pagingRequest = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<PostForAdminIndexVM>> GetPostForAdminIndexVMsAsync(PagingRequest pagingRequest);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Post> GetPostAsync(long id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PostVM> GetPostVMAsync(long id);
        /// <summary>
        /// This method gets a post along with its related information (author, categories, tags, comment, rating,...)
        /// </summary>
        /// <param name="id">ID of a post</param>
        /// <param name="userName">Username of the current logged in user to check if this person has rated the post</param>
        /// <returns>Return PostDetailVM object</returns>
        Task<PostDetailVM> GetPostDetailVMAsync(long id, string userName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PostEditVM> GetPostEditVMsAsync(string token, long id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> CreatePostAsync(string token, PostCreateVM request);

        /// <summary>
        /// This method updates an existing post.
        /// </summary>
        /// <param name="token">Jwt token for authentication in API</param>
        /// <param name="id">Id of a post</param>
        /// <param name="request">Post to update against</param>
        /// <returns>Return true if success, else return false</returns>
        Task<bool> UpdatePostAsync(string token, long id, PostEditVM request);

        /// <summary>
        /// This method updates publishing status of a post
        /// </summary>
        /// <param name="token">Jwt token for authentication in API</param>
        /// <param name="id">Id of a post</param>
        /// <param name="status">Status enum of a post</param>
        /// <returns>Return true if success, else return false</returns>
        Task<bool> PublishPostAsync(string token, long id, PostDetailVM request);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postVM"></param>
        /// <returns></returns>
        Task<bool> DeletePostAsync(string token, long id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsPostExistAsync(long id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="tagIds"></param>
        /// <param name="postId"></param>
        /// <returns></returns>
        Task<PostRelatedListOfObjectsDTO> GetSelectListsForPostCreationAsync(string token);
    }
}
