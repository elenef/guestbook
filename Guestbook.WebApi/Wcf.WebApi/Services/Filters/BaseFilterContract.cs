namespace GuestBook.WebApi.Services.Filters
{
    public class BaseFilterContract : IFilterContract
    {
        public const int DefaultPageSize = 25;

        public int Page { get; set; }

        public int PageSize { get; set; }

        public string OrderBy { get; set; }

        public bool OrderDesc { get; set; }

        public BaseFilterContract()
        {
            Page = 0;
            PageSize = DefaultPageSize;
        }
    }
}
