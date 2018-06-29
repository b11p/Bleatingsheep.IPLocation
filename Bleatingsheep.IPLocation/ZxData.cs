using Newtonsoft.Json;

namespace Bleatingsheep.IPLocation
{
    internal sealed class ZxData : IIPLocation
    {
        [JsonProperty("myip")] public string MyIP { get; private set; }
        [JsonProperty("ip")] public IPRange IPRange { get; private set; }
        [JsonProperty("location")] public string Location { get; private set; }
        [JsonProperty("country")] public string Region { get; private set; }
        [JsonProperty("local")] public string Provider { get; private set; }
    }
}
