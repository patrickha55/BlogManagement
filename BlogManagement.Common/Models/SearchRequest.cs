namespace BlogManagement.Common.Models
{
    /// <summary>
    /// This class stores a paging request contains of page number and page size
    /// </summary>
    public class SearchRequest
    {
        public SearchRequest()
        {
            
        }

        public SearchRequest(string keyword)
        {
            Keyword = keyword;
        }

        public SearchRequest(string keyword, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Keyword = keyword;
        }

        private const int MaxPageSize = 20;

        private int _pageSize = 10;
        public string Keyword { get; set; }
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;

            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : _pageSize;
        }
    }
}
