using Newtonsoft.Json;

namespace Bleatingsheep.IPLocation
{
    internal sealed class IPRange
    {
        [JsonProperty("start")] public string Start { get; private set; }
        [JsonProperty("end")] public string End { get; private set; }

        public override string ToString() => string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0} - {1}", Start, End);
    }
}
