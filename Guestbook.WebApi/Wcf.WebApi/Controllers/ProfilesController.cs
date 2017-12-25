using GuestBook.WebApi.Contracts;
using GuestBook.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GuestBook.WebApi.Controllers
{
    /// <summary>
    /// Контроллер профайлов.
    /// </summary>
    [Route("profiles")]
    [Authorize]
    public class ProfilesController
    {
        private IProfilesService _service;

        public ProfilesController(IProfilesService service)
        {
            _service = service;
        }

        /// <summary>
        /// Возвращает профиль пользователя.
        /// </summary>
        /// <returns>Контракт профиля пользователя.</returns>
        [HttpGet]
        public async Task<ProfileContract> GetProfile()
        {
            var result = await _service.GetProfile();

            return result;
        }

        /// <summary>
        /// Обновляет профиль пользователя.
        /// </summary>
        /// <param name="model">Модель обновления пользователя.</param>
        /// <returns>Обновленный контракт профиля пользователя.</returns>
        [HttpPut]
        public async Task<ProfileContract> UpdateProfile([FromBody] EditProfileContract model)
        {
            var result = await _service.UpdateProfile(model);

            return result;
        }
    }
}
