namespace GuestBook.WebApi.Identity
{
    public interface IUserContext
    {
        string UserId { get; }

        string UserRole { get; }
    }
}
