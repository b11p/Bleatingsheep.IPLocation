using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Bleatingsheep.IPLocation
{
    public abstract class IPLocator : IIPLocator
    {
        public static IIPLocator Default { get; } = new ZxLocator(new IpipLocator());

        protected async Task<(bool, T)> GetJsonAsync<T>(string url)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync(url);
                    while ((int)response.StatusCode == 429)
                    {
                        await Task.Delay(110);
                        response = await httpClient.GetAsync(url);
                    }
                    response = response.EnsureSuccessStatusCode();
                    string sResult = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(sResult);
                    return (true, result);
                }
                catch (Exception)
                {
                    return (false, default(T));
                }
            }
        }

        /// <exception cref="ArgumentNullException"></exception>
        public abstract Task<(bool, IIPLocation)> GetLocationAsync(IPAddress address);

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        public virtual async Task<(bool, IIPLocation)> GetLocationAsync(string address) => await GetLocationAsync(IPAddress.Parse(address ?? throw new ArgumentNullException(nameof(address))));

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public virtual async Task<(bool, IIPLocation)> GetLocationAsync(byte[] address) => await GetLocationAsync(new IPAddress(address ?? throw new ArgumentNullException(nameof(address))));
    }
}
