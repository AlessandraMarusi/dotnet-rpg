using System.Text.Json.Serialization;

namespace dotnet_rpg.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))] //To show "Barbarian" instead of "0" in Swagger
    public enum RpgClass
    {
        Barbarian = 0,
        Fighter = 1,
        Mage = 2,
        Cleric = 3,
    }
}
