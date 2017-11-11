namespace GuestBook.Services.Filters
{
    public interface IFilterContract
    {
        int Page { get; set; }

        int PageSize { get; set; }

        string OrderBy { get; set; }

        bool OrderDesc { get; set; }
    }
}
