namespace GuestBook.WebApi.Identity
{
    public class UserContext : IUserContext
    {
        public string UserId { get; set; }

        public string UserRole { get; set; }
    }
}
