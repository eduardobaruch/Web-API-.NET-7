using System.Text.Json.Serialization;

namespace Web_API_.NET_7.Models
{
    //Converting the number to name, when showing in swagger. Ex: 1 = Knight
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Knight = 1,
        Paladin = 2,
        Sorcerer = 3,
        Druid = 4
    }
}
