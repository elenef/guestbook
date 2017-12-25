using GuestBook.WebApi.Contracts;
using System.Threading.Tasks;

namespace GuestBook.WebApi.Services
{
    /// <summary>
    /// Сервис профилей.
    /// </summary>
    public interface IProfilesService
    {
        /// <summary>
        /// Возвращает профиль по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор профиля.</param>
        /// <returns>Контрак профиля.</returns>
        Task<ProfileContract> GetProfile();

        /// <summary>
        /// Обновляет профиль.
        /// </summary>
        /// <param name="id">Идентификатор профиля.</param>
        /// <param name="contract">Контракт профиля.</param>
        /// <returns>Контракт обновленного профиля.</returns>
        Task<ProfileContract> UpdateProfile(EditProfileContract contract);
    }
}
