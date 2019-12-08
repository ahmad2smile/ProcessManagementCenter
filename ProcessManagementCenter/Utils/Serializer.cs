using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Text;

namespace ProcessManagementCenter.Utils
{
    public static class Serializer
    {
        public static string ToJsonString(object obj)
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });

        }

        public static StringContent ToStringContent(object obj)
        {
            return new StringContent(ToJsonString(obj), Encoding.UTF8, "application/json");
        }
    }
}
