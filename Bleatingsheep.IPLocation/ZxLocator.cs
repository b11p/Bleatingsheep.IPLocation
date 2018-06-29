using System.Net;
using System.Threading.Tasks;

namespace Bleatingsheep.IPLocation
{
    internal sealed class ZxLocator : IPv6Locator
    {
        private const string V6Format = "http://ip.zxinc.org/api.php?type=json&ip={0}";

        public ZxLocator(IIPLocator fallback) => IPv4FallbackLocator = fallback;

        protected override IIPLocator IPv4FallbackLocator { get; }

        protected override async Task<(bool, IIPLocation)> GetIPv6LocationAsync(IPAddress address)
        {
            string add = address.ToString();
            int end = add.IndexOf('%');
            if (end != -1)
            {
                add = add.Substring(0, end);
            }
            var (success, response) = await GetJsonAsync<ZxResponse>(string.Format(System.Globalization.CultureInfo.InvariantCulture, V6Format, add));
            return (success, response?.Data);
        }
    }
}
