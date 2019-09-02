namespace ShareMe.BLL.Interfaces.Models
{
    public class PageInfo
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public void CheckPageInfo(int pageSize)
        {
            if (PageSize == 0)
            {
                PageSize = pageSize;
            }

            if (PageNumber == 0)
            {
                PageNumber = 1;
            }
        }
    }
}
