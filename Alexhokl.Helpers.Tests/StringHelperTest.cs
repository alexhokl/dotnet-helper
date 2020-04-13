using Xunit;

namespace Alexhokl.Helpers.Tests
{
    public class StringHelperTest
    {
        [Theory]
        [InlineData("abc", "H4sIAAAAAAAAA0tMSgYAwkEkNQMAAAA=")]
        public void Zip(string input, string expected)
        {
            var base64Str = StringHelper.Zip(input);
            Assert.Equal(expected, base64Str);
        }

        [Theory]
        [InlineData("H4sIAAAAAAAAA0tMSgYAwkEkNQMAAAA=", "abc")]
        public void UnZip(string input, string expected)
        {
            var unzippedStr = StringHelper.Unzip(input);
            Assert.Equal(expected, unzippedStr);
        }
    }
}

