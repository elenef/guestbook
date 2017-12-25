using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GuestBook.WebApi
{
    public static class Config
    {
        public static JsonSerializerSettings JsonSerializerSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
            }
        }
    }
}
