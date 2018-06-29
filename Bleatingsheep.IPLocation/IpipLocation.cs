using System.Collections.Generic;
using System.Linq;

namespace Bleatingsheep.IPLocation
{
    internal sealed class IpipLocation : IIPLocation
    {
        private readonly string _country;
        private readonly string _region;
        private readonly string _city;
        private readonly string _detail;
        private readonly IReadOnlyList<string> _raw;

        internal IpipLocation(IEnumerable<string> response)
        {
            _raw = response.ToArray();
            Provider = response.Last();
            _country = _raw[0];
            _region = _raw[1];
            _city = _raw[2];
            _detail = _raw[3];
            Region = string.Join(" ", _raw.Take(_raw.Count - 1).Where(s => !string.IsNullOrEmpty(s)).Distinct());
        }

        public string Provider { get; }

        public string Region { get; }

    }
}
