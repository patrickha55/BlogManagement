using System;
using System.Collections.Generic;

namespace BlogManagement.Common.Common
{
    public class Paginated<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set;  }
        public int PageSize { get; set;  }
        public int TotalCount { get; set;  }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public List<T> Objects { get; set; } = new();
    }
}
