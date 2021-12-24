using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogManagement.Application.Contracts;
using BlogManagement.Common.Common;
using BlogManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                var entities = await _context.Set<TEntity>().ToListAsync();

                return entities;
            }
            catch (Exception e)
            {
                _logger.LogError(e,  "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAllAsync));
                throw;
            }
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            try
            {
                var model = await _context.Set<TEntity>().FindAsync(id);

                if (model is null)
                    throw new ArgumentException(Constants.InvalidArgument);

                return model;
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, "{0} {1}", Constants.InvalidArgument, nameof(GetByIdAsync));
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetByIdAsync));
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
