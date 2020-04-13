using System.Net;
using Xunit;

namespace Alexhokl.Helpers.Tests
{
    public class IPAddressTest
    {
        [Theory]
        [InlineData("192.168.1.100", "255.255.255.0", "192.168.1.0")]
        public void GetNetworkAddress(string input, string mask, string expected)
        {
            IPAddress address = IPAddress.Parse(input);
            IPAddress maskAddress = IPAddress.Parse(mask);

            Assert.Equal(
                expected,
                IPAddressHelper.GetNetworkAddress(address, maskAddress).ToString());
        }
    }
}


