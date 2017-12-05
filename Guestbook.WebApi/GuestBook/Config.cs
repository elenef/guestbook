using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GuestBook
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
