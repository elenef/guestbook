namespace GuestBook.WebApi.Services.Filters
{
    public class ReviewFilterContract : BaseFilterContract
    {
        public string Search { get; set; }

        public string RestaurantName { get; set; }

        public long DateFrom { get; set; }

        public long DateTo { get; set; }
    }
}
