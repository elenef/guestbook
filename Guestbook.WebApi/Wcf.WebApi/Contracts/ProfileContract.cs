namespace GuestBook.WebApi.Contracts
{
    /// <summary>
    /// Контракт получения профиля.
    /// </summary>
    public class ProfileContract : EditProfileContract
    {
        /// <summary>
        /// Идентификатор выбранного объекта, чей профиль вы просматриваете.
        /// </summary>
        public string Id { get; set; }
    }
}
