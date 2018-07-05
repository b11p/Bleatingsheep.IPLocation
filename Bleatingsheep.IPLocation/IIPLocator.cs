using System;
using System.Net;
using System.Threading.Tasks;

namespace Bleatingsheep.IPLocation
{
    public interface IIPLocator
    {
        /// <exception cref="ArgumentNullException"></exception>
        Task<(bool, IIPLocation)> GetLocationAsync(IPAddress address);

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        Task<(bool, IIPLocation)> GetLocationAsync(string address);

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Task<(bool, IIPLocation)> GetLocationAsync(byte[] address);
    }
}