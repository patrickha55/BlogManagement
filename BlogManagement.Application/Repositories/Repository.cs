﻿using BlogManagement.Application.Contracts.Repositories;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models;
using BlogManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace BlogManagement.Application.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected BlogManagementContext Context;
        protected ILogger<Repository<TEntity>> Logger;

        public Repository(
            BlogManagementContext context,
            ILogger<Repository<TEntity>> logger)
        {
            Context = context;
            Logger = logger;
        }

        public async Task<IPagedList<TEntity>> GetAllAsync(PagingRequest request, Expression<Func<TEntity, bool>> expression = null, List<string> includes = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

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
                Logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAllAsync));
                throw;
            }
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, List<string> includes = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

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
                Logger.LogError(e, "{0} {1}", Constants.InvalidArgument, nameof(GetAsync));
                throw;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAsync));
                throw;
            }
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            await using var transaction = await Context.Database.BeginTransactionAsync();

            try
            {
                var result = await Context.Set<TEntity>().AddAsync(entity);

                await transaction.CommitAsync();
                return result.State is EntityState.Added;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(CreateAsync));
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            await using var transaction = await Context.Database.BeginTransactionAsync();

            try
            {
                var result = Context.Set<TEntity>().Update(entity);

                await transaction.CommitAsync();
                return await Task.FromResult(result.State is EntityState.Modified);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(UpdateAsync));
                throw;
            }
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            await using var transaction = await Context.Database.BeginTransactionAsync();

            try
            {
                var result = Context.Set<TEntity>().Remove(entity);

                await transaction.CommitAsync();
                return await Task.FromResult(result.State is EntityState.Deleted);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(DeleteAsync));
                throw;
            }
        }

        public async Task<bool> IsExistsAsync(long id)
        {
            try
            {
                var model = await Context.Set<TEntity>().FindAsync(id);
                return model is not null;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(IsExistsAsync));
                throw;
            }
        }
    }
}
