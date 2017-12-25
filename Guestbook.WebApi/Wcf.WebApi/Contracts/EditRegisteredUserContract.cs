namespace GuestBook.WebApi.Contracts
{
    public class EditRegisteredUserContract
    {
        public string Name { get; set; }

        public string Login { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
