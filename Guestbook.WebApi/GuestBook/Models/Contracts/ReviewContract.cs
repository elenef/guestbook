namespace GuestBook.Models.Contracts
{
    public class ReviewContract : EditReviewContract
    {
        public string Id { get; set; }

        public long? Created { get; set; }
    }
}
