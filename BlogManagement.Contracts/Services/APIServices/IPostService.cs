using BlogManagement.Common.Common;
using BlogManagement.Common.DTOs.PostDTOs;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.PostVMs;
using BlogManagement.Data.Entities;

namespace BlogManagement.Contracts.Services.APIServices
{
    /// <summary>
    /// Interface for storing Post related services for Web API
    /// </summary>
    public interface IPostService
    {
        /// <summary>
        /// This method gets all posts for index view model as well as an option to get all posts of an author.
        /// </summary>
        /// <param name="pagingRequest">PagingRequest object for paging</param>
        /// <param name="authorId">User's ID</param>
        /// <returns>List of PostForIndexVMs</returns>
        Task<List<PostForIndexVM>> GetPostsForIndexVMsAsync(PagingRequest pagingRequest, long? authorId = null);

        /// <summary>
        /// This method gets all posts for displaying on admin post index view.
        /// </summary>
        /// <param name="pageNumber">Page number of the current list</param>
        /// <param name="pageSize">Size of the page</param>
        /// <returns>List of PostForAdminIndexVM objects</returns>
        Task<List<PostForAdminIndexVM>> GetPostForAdminIndexVMsAsync(int pageNumber = 1, int pageSize = 10);

        /// <summary>
        /// This method gets a post base on its id.
        /// </summary>
        /// <param name="id">ID of a post</param>
        /// <returns>A Post object</returns>
        Task<Post> GetPostAsync(long id);

        /// <summary>
        /// This method gets a post view model base on its id.
        /// </summary>
        /// <param name="id">ID of a post</param>
        /// <returns>A PostVM object</returns>
        Task<PostVM> GetPostVMAsync(long id);

        /// <summary>
        /// This method gets a post along with its related information (author, categories, tags, comment, rating,...)
        /// </summary>
        /// <param name="id">ID of a post</param>
        /// <param name="userName">Username of the current logged in user to check if this person has rated the post</param>
        /// <returns>Return PostDetailVM object</returns>
        Task<PostDetailVM> GetPostDetailVMAsync(long id, string userName);

        /// <summary>
        /// This method gets a post view model base on its id.
        /// </summary>
        /// <param name="id">ID of a post</param>
        /// <returns>A PostEditVM object</returns>
        Task<PostEditVM> GetPostEditVMsAsync(long id);

        /// <summary>
        /// This method creates a new post for an author
        /// </summary>
        /// <param name="request">Post info to create</param>
        /// <param name="userName">Username of the author</param>
        /// <returns>A PostVM object</returns>
        Task<PostVM> CreatePostAsync(PostCreateVM request, string userName);

        /// <summary>
        /// This method updates an existing post.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request">Post to update against</param>
        /// <returns>Return true if success, else return false</returns>
        Task<bool> UpdatePostAsync(long id, PostEditVM request);

        /// <summary>
        /// This method change the publishing status of a post
        /// </summary>
        /// <param name="id">Id of a post</param>
        /// <param name="status">Status to update</param>
        /// <returns>Returns true if success, else returns false</returns>
        Task<bool> PublishPostAsync(long id, PostStatus status);

        /// <summary>
        /// This method deletes an existing post
        /// </summary>
        /// <param name="post">Post to delete</param>
        /// <returns>Returns true if success, else returns false</returns>
        Task<bool> DeletePostAsync(Post post);

        /// <summary>
        /// This method checks if a post exists in the Data store
        /// </summary>
        /// <param name="id">ID of a post to check</param>
        /// <returns>Returns true if success, else returns false</returns>
        Task<bool> IsPostExistAsync(long id);

        /// <summary>
        /// This method gets a post related list of objects for post creating or editing
        /// </summary>
        /// <returns>A PostRelatedListOfObjectsDTO</returns>
        Task<PostRelatedListOfObjectsDTO> GetSelectListsForPostCreationAsync();

        /// <summary>
        /// This method increase a post view count by one.
        /// </summary>
        /// <param name="post">A Post to increase view</param>
        Task UpdatePostViewCountAsync(Post post);
    }
}
