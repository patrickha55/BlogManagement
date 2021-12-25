using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlogManagement.Application.Contracts;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using X.PagedList;

namespace BlogManagement.Application.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly BlogManagementContext _context;
        private readonly ILogger<Repository<TEntity>> _logger;

        public Repository(
            BlogManagementContext context,
            ILogger<Repository<TEntity>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IPagedList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, PagingRequest request, List<string> includes = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

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

                return await query.AsNoTracking().ToPagedListAsync(request.PageNumber, request.PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAllAsync));
                throw;
            }
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, List<string> includes = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            try
            {
                if (includes is not null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }

                var model = await query.AsNoTracking().FirstOrDefaultAsync(expression);

                if (model is null)
                    throw new ArgumentException(Constants.InvalidArgument);

                return model;
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, "{0} {1}", Constants.InvalidArgument, nameof(GetAsync));
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAsync));
                throw;
            }
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            try
            {
                var result = await _context.Set<TEntity>().AddAsync(entity);

                return result.State is not EntityState.Added;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(CreateAsync));
                throw;
            }
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            try
            {
                var result = _context.Set<TEntity>().Update(entity);

                return await Task.FromResult(result.State is not EntityState.Modified);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(UpdateAsync));
                throw;
            }
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            try
            {
                var result = _context.Set<TEntity>().Remove(entity);

                return await Task.FromResult(result.State is not EntityState.Deleted);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(DeleteAsync));
                throw;
            }
        }

        public async Task<bool> IsExists(long id)
        {
            try
            {
                var model = await _context.Set<TEntity>().FindAsync(id);
                return model is not null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(IsExists));
                throw;
            }
        }
    }
}
