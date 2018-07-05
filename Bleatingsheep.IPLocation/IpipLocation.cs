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
        private readonly string _rawString;

        internal IpipLocation(IEnumerable<string> response)
        {
            IReadOnlyList<string> raw = response.ToArray();
            Provider = raw.Last();
            _country = raw[0];
            _region = raw[1];
            _city = raw[2];
            _detail = raw[3];
            Location = RawString(raw.Take(raw.Count - 1));
            _rawString = RawString(raw);
        }

        private string RawString(IEnumerable<string> raw) => string.Join(" ", raw.Where(s => !string.IsNullOrEmpty(s)).Distinct());

        public override string Provider { get; }

        public override string Location { get; }

        public override string ToString() => _rawString;
    }
}
