namespace Entities.RequestFeatures
{
    public abstract class RequestParameters
    {
        private const int maxPageSize = 50;
        private int pageSize = 10;
        public int PageNumber { get; set; } = 1;
        public string OrderBy { get; set; }

        public string Fields { get; set; }

        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
