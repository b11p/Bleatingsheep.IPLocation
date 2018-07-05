using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Bleatingsheep.IPLocation
{
    public abstract class IPv6Locator : IPLocator, IIPLocator
    {
        protected abstract IIPLocator IPv4FallbackLocator { get; }

        protected abstract Task<(bool, IIPLocation)> GetIPv6LocationAsync(IPAddress address);

        /// <exception cref="ArgumentNullException"></exception>
        public override async Task<(bool, IIPLocation)> GetLocationAsync(IPAddress address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (address.AddressFamily == AddressFamily.InterNetwork)
            {
                return await IPv4FallbackLocator.GetLocationAsync(address);
            }
            if (address.IsIPv4MappedToIPv6)
            {
                return await IPv4FallbackLocator.GetLocationAsync(address.MapToIPv4());
            }
            var bytes = address.GetAddressBytes();
            if (address.IsIPv6Teredo)
            {
                return await IPv4FallbackLocator.GetLocationAsync(new IPAddress(bytes.Skip(12).Select(b => (byte)(255 - b)).ToArray()));
            }
            if (bytes[0] == 0x20 && bytes[1] == 0x02)
            {
                return await IPv4FallbackLocator.GetLocationAsync(new IPAddress(bytes.Skip(2).Take(4).ToArray()));
            }
            return await GetIPv6LocationAsync(address);
        }
    }
}
