namespace GuestBook.WebApi.Contracts
{
    public class EditReviewContract
    {
        public string Comment { get; set; }

        public string RestaurantId { get; set; }

        public string UserId { get; set; }
    }
}
