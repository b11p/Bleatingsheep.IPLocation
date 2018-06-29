using Newtonsoft.Json;

namespace Bleatingsheep.IPLocation
{
    [JsonObject(MemberSerialization.OptIn)]
    internal sealed class ZxData : IPLocation, IIPLocation
    {
#pragma warning disable CS0649
        [JsonProperty("country")] private string _region;
        [JsonProperty("local")] private string _provider;
#pragma warning restore CS0649

        [JsonProperty("myip")] public string MyIP { get; private set; }
        [JsonProperty("ip")] public IPRange IPRange { get; private set; }
        [JsonProperty("location")] public string FullLocation { get; private set; }
        public override string Location => _region;
        public override string Provider => _provider;

        public override string ToString() => FullLocation;
    }
}
