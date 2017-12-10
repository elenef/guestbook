namespace GuestBook.Models.Contracts
{
    public class ReviewContract
    {
        public string Id { get; set; }

        public long? Created { get; set; }

        public string RestaurantName { get; set; }

        public string UserName { get; set; }

        public string Comment { get; set; }
    }
}
