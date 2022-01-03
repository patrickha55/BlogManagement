using BlogManagement.Application.Contracts.Repositories;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Data;
using BlogManagement.Data.Entities;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlogManagement.Common.Models.PostVMs;
using X.PagedList;

namespace BlogManagement.Application.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(
            BlogManagementContext context,
            ILogger<PostRepository> logger) : base(context, logger)
        {
            Logger = logger;
        }

        public async Task<IEnumerable<Post>> GetByAuthorId(long authorId)
        {
            try
            {
                var posts = await Context.Posts
                    .Where(p => p.AuthorId == authorId)
                    .ToListAsync();

                if (posts is null)
                    throw new ArgumentException(Constants.InvalidArgument);

                return posts;
            }
            catch (ArgumentException e)
            {
                Logger.LogError(e, "{0} {1}", Constants.InvalidArgument, nameof(GetByAuthorId));
                throw;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetByAuthorId));
                throw;
            }
        }

        public async Task<IPagedList<Post>> GetPostsForIndexAsync(PagingRequest request, Expression<Func<Post, bool>> expression = null)
        {
            IQueryable<Post> query = Context.Posts;

            try
            {
                if (expression is not null)
                {
                    query = query.Where(expression);
                }

                query = query.Include(p => p.User)
                    .Include(p => p.PostComments)
                    .Include(p => p.PostRatings)
                    .Include(p => p.CategoryPosts)
                    .ThenInclude(cp => cp.Category);

                return await query.AsNoTracking().ToPagedListAsync(request.PageNumber, request.PageSize);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAllAsync));
                throw;
            }
        }

        public async Task<Post> GetPostDetailsAsync(long postId)
        {
            IQueryable<Post> query = Context.Posts;

            try
            {
                query = query.Include(p => p.User)
                    .Include(p => p.PostComments)
                    .ThenInclude(pc => pc.User)
                    .Include(p => p.PostComments)
                    .ThenInclude(pc => pc.ChildPostComments)
                    .ThenInclude(cp => cp.User)
                    .Include(p => p.PostRatings)
                    .ThenInclude(pr => pr.User)
                    .Include(p => p.PostMetas)
                    .Include(p => p.CategoryPosts)
                    .ThenInclude(cp => cp.Category)
                    .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag);

                return await query.SingleOrDefaultAsync(p => p.Id == postId);
            }
            catch (ArgumentException e)
            {
                Logger.LogError(e, "{0} {1}", Constants.InvalidArgument, nameof(GetPostDetailsAsync));
                throw;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostDetailsAsync));
                throw;
            }
        }

        public async Task<IEnumerable<Post>> GetAllPostIdsAndTitles()
        {
            try
            {
                return await Context.Posts
                    .AsNoTracking()
                    .Select(p => new Post
                    {
                        Id = p.Id,
                        Title = p.Title
                    })
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAllPostIdsAndTitles));
                throw;
            }
        }

        public async Task<IEnumerable<Post>> GetAllPostWithoutPaging(Expression<Func<Post, bool>> expression = null, List<string> includes = null)
        {
            IQueryable<Post> query = Context.Posts;
            try
            {
                if (expression is not null)
                {
                    query = query.Where(expression);
                }

                if (includes is not null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }

                return await query.AsNoTracking()
                    .Select(p => new Post { Id = p.Id, CategoryPosts = p.CategoryPosts, PostTags = p.PostTags })
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAllPostWithoutPaging));
                throw;
            }
        }

        public async Task<Post> CreatePostAsync(Post post, [CanBeNull] IFormFile formFile)
        {
            try
            {
                if (formFile is not null)
                {
                    post.ImageUrl = $@"images/{await HandleImageUpload(formFile)}";
                }

                var result = await Context.Posts.AddAsync(post);

                if (result.State is EntityState.Added)
                    return post;
            }
            catch (DirectoryNotFoundException e)
            {
                Logger.LogError(e, "{0} {1}", Constants.InvalidDirectory, nameof(CreatePostAsync));
                throw;
            }
            catch (FileNotFoundException e)
            {
                Logger.LogError(e, "{0} {1}", Constants.FileNotFound, nameof(CreatePostAsync));
                throw;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(CreatePostAsync));
                throw;
            }

            return null;
        }

        public async Task<bool> UpdatePostAsync(Post post, IFormFile formFile)
        {
            try
            {
                Context.Entry(post).State = EntityState.Modified;

                if (formFile is not null)
                {
                    post.ImageUrl = $@"images/{await HandleImageUpload(formFile)}";
                }
                
                return true;
            }
            catch (DirectoryNotFoundException e)
            {
                Logger.LogError(e, "{0} {1}", Constants.InvalidDirectory, nameof(CreatePostAsync));
                throw;
            }
            catch (FileNotFoundException e)
            {
                Logger.LogError(e, "{0} {1}", Constants.FileNotFound, nameof(CreatePostAsync));
                throw;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(CreatePostAsync));
                throw;
            }
        }

        private static async Task<string> HandleImageUpload(IFormFile formFile)
        {
            var uploadTime = DateTime.Now.ToString("MMddyyyHHmmss");
            var imgName = uploadTime + "_" + Path.GetFileName(formFile.FileName);
            var imgPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", imgName);

            await using var fileStream = new FileStream(imgPath, FileMode.Create);

            await formFile.CopyToAsync(fileStream);

            return imgName;
        }
    }
}
