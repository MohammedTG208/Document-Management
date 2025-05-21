namespace DocumentManagementAPI.Paging
{
    public class Page
    {
        private const int MaxPageSize = 30;
        private int _pageSize = 10;
        private int _totalpageCount;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int TotalItemCount { get; set; }
        public int TotalPageCount { get; set; }

        public Page(int pageSize, int pageNumber, int totalItemCount)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)PageSize);
        }
    }
}
