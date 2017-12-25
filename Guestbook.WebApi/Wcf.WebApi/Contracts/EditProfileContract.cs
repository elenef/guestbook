using Newtonsoft.Json.Linq;

namespace GuestBook.WebApi.Contracts
{
    /// <summary>
    /// Контракт обновления профиля.
    /// </summary>
    public class EditProfileContract
    {
        /// <summary>
        /// Роль пользователя может принимать следующие значения:
        /// admin - администратор системы
        /// creditor - кредитор в системе
        /// provider - поставщик в системе
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Содержание профиля. 
        /// Это поле может содержать любой объект в формате JSON. 
        /// Тип объета зависит от поля type.
        /// </summary>
        public JObject Value { get; set; }
    }
}
