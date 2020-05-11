using System;
using System.Net;
using System.Threading.Tasks;

namespace Alexhokl.Helpers
{
    public static class IPAddressHelper
    {
        /// <summary>
        /// Gets the network address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="subnetMask">The subnet mask.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">Length of specified address and subnet mask does not match.</exception>
        public static IPAddress GetNetworkAddress(IPAddress address, IPAddress subnetMask)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));
            if (subnetMask == null)
                throw new ArgumentNullException(nameof(subnetMask));

            byte[] addressBytes = address.GetAddressBytes();
            byte[] maskBytes = subnetMask.GetAddressBytes();

            if (addressBytes.Length != maskBytes.Length)
                throw new InvalidOperationException(
                    "Length of specified address and subnet mask does not match.");

            byte[] networkAddressBytes = new byte[addressBytes.Length];
            Parallel.For(
                0, networkAddressBytes.Length, i =>
                    networkAddressBytes[i] = (byte)(addressBytes[i] & maskBytes[i]));
            return new IPAddress(networkAddressBytes);
        }
    }
}
