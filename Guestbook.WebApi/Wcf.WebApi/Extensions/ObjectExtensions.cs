using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GuestBook.WebApi.Extensions
{
    /// <summary>
    /// Расширения для работы с объектами.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Преобразовывает строку в CamelCase с малой буквы.
        /// </summary>
        /// <param name="value">Преобразуемая строка.</param>
        /// <returns>Преобразованная строка.</returns>
        public static string ToLowerCamelCaseString(this object value)
        {
            var val = value.ToString();
            if (val.Length > 0)
            {
                val = val[0].ToString().ToLower() + val.Substring(1);
            }
            return val;
        }

        /// <summary>
        /// Получает JObject из объекта.
        /// </summary>
        /// <param name="value">Преобразуемый объект.</param>
        /// <returns>JObject объекта.</returns>
        public static JObject ToContractObject(this object value)
        {
            if (value == null)
            {
                return null;
            }
            return JObject.FromObject(value, JsonSerializer());
        }

        /// <summary>
        /// Преобразует JObject в объект.
        /// </summary>
        /// <typeparam name="T">Тип объекта, в который необходимо преобразовать.</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T FromContractObject<T>(this JObject value)
            where T : new()
        {
            if (value == null)
            {
                return default(T);
            }
            return value.ToObject<T>(JsonSerializer());
        }

        /// <summary>
        /// Создаёт JSON-сериалайзер.
        /// </summary>
        /// <returns>JSON-сериалайзер</returns>
        private static JsonSerializer JsonSerializer()
        {
            return Newtonsoft.Json.JsonSerializer.CreateDefault(Config.JsonSerializerSettings);
        }
    }
}
