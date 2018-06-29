using System.Collections.Generic;
using System.Linq;

namespace Bleatingsheep.IPLocation
{
    internal sealed class IpipLocation : IPLocation, IIPLocation
    {
        private readonly string _country;
        private readonly string _region;
        private readonly string _city;
        private readonly string _detail;
        private readonly IReadOnlyList<string> _raw;
        private readonly string _rawString;

        internal IpipLocation(IEnumerable<string> response)
        {
            _raw = response.ToArray();
            Provider = response.Last();
            _country = _raw[0];
            _region = _raw[1];
            _city = _raw[2];
            _detail = _raw[3];
            Location = RawString(_raw.Take(_raw.Count - 1));
            _rawString = RawString(_raw);
        }

        private string RawString(IEnumerable<string> raw) => string.Join(" ", raw.Where(s => !string.IsNullOrEmpty(s)).Distinct());

        public override string Provider { get; }

        public override string Location { get; }

        public override string ToString() => _rawString;
    }
}
