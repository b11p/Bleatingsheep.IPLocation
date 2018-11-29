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

        /// <summary>
        /// Gets original IP (if under known tunnel types).
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>If <c>address</c> is an IPv6 Mapped Address, or 6to4 address, or Teredo address, the IPv4 it represents; otherwise, <c>address</c> itself.</returns>
        public static IPAddress GetPureIP(IPAddress address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (address.AddressFamily == AddressFamily.InterNetwork)
            {
                return address;
            }
            if (address.IsIPv4MappedToIPv6)
            {
                return address.MapToIPv4();
            }
            var bytes = address.GetAddressBytes();
            if (address.IsIPv6Teredo)
            {
                return new IPAddress(bytes.Skip(12).Select(b => (byte)(255 - b)).ToArray());
            }
            if (bytes[0] == 0x20 && bytes[1] == 0x02)
            {
                return new IPAddress(bytes.Skip(2).Take(4).ToArray());
            }

            return address;
        }

        /// <exception cref="ArgumentNullException"></exception>
        public override async Task<(bool, IIPLocation)> GetLocationAsync(IPAddress address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            address = GetPureIP(address);
            return address.AddressFamily == AddressFamily.InterNetwork
                ? await IPv4FallbackLocator.GetLocationAsync(address)
                : await GetIPv6LocationAsync(address);
        }
    }
}
