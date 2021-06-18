using Newtonsoft.Json.Linq;

namespace Util
{
    public static class JsonHelper
    {
        public static T TryGetDefaultValue<T>(this JObject jObject, string propertyName)
        {
            if (jObject == null) return default;
            if (!jObject.TryGetValue(propertyName, out JToken jToken)) return default;
            if (jToken == null) return default;
            return jToken.Value<T>();
        }
    }
}