using Newtonsoft.Json;

namespace Bleatingsheep.IPLocation
{
    internal class ZxResponse
    {
        [JsonProperty("code")] public int Code { get; private set; }
        [JsonProperty("data")] public ZxData Data { get; private set; }
    }
}
