using System.Net;
using System.Threading.Tasks;

namespace Bleatingsheep.IPLocation
{
    internal sealed class IpipLocator : IPLocator
    {
        private const string V4Format = "https://freeapi.ipip.net/{0}";

        /// <exception cref="ArgumentNullException"></exception>
        public override async Task<(bool, IIPLocation)> GetLocationAsync(IPAddress address)
        {
            if (address == null)
            {
                throw new System.ArgumentNullException(nameof(address));
            }

            var (success, response) = await GetJsonAsync<string[]>(string.Format(System.Globalization.CultureInfo.InvariantCulture, V4Format, address.ToString()));
            if (!success) return (success, null);
            var result = new IpipLocation(response);
            return (success, result);
        }
    }
}
