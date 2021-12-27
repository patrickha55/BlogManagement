﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BlogManagement.Data.Entities;

namespace BlogManagement.Application.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITagRepository : IRepository<Tag>
    {
        Task<IEnumerable<Tag>> GetAllTagIdsAndTitles();
    }
}
