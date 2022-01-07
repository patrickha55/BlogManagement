using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlogManagement.Common.Common
{
    public class PaginatedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set;  }
        public int PageSize { get; set;  }
        public int TotalCount { get; set;  }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        [JsonConstructor]
        public PaginatedList()
        {
            
        }

        public PaginatedList(int totalCount, int currentPage, int pageSize, IEnumerable<T> items)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            AddRange(items);
        }

        public static async Task<PaginatedList<T>> ToPaginatedListAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = await source.Skip(
                (pageNumber - 1) * pageSize).Take(pageSize)
                .ToListAsync();

            return new PaginatedList<T>( count, pageNumber, pageSize, items);
        }
    }
}
