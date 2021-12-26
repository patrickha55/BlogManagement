using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogManagement.Data.Entities;

namespace BlogManagement.Application.Contracts
{
    public interface ICategoryRepository : IRepository<Category>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Category>> GetAllIdAndNameWithoutPagingAsync();
    }
}
