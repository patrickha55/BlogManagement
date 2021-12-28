namespace BlogManagement.Common.Models
{
    /// <summary>
    /// This class stores a paging request contains of page number and page size
    /// </summary>
    public class PagingRequest
    {
        private const int MaxPageSize = 20;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;

            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : _pageSize;
        }
    }
}
