namespace GuestBook.WebApi.Contracts
{
    public class ReviewContract : EditReviewContract
    {
        public string Id { get; set; }

        public long? Created { get; set; }

        public string RatingRestaurant { get; set; }

        public string RestaurantName { get; set; }
    }
}
