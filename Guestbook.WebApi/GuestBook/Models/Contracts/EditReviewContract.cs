namespace GuestBook.Models.Contracts
{
    public class EditReviewContract
    {
        public string Comment { get; set; }

        public RestaurantContract Restaurant { get; set; }

        public UserContract User { get; set; }
    }
}
