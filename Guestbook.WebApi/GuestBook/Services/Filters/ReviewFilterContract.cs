namespace GuestBook.WebApi.Services.Filters
{
    public class ReviewFilterContract : BaseFilterContract
    {
        public string Search { get; set; }

        public string RestaurantIds { get; set; }
    }
}
