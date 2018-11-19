using System.Collections.Specialized;
using System.Net;
using NUnit.Framework;
using Pixie.Extensions.Maxmind.GeoIp.Services;

namespace GeoIpTests
{
    [TestFixture]
    public class MaxmindGeoIp2Tests
    {
        private GeolocationMaxmindService service = new GeolocationMaxmindService();

        [TestCase]
        public void MaxmindServiceDBTest()
        {
            var config = GetConfig();
            var result = service.GetGeoLocation(IPAddress.Parse("213.205.251.152"), config);
            Assert.AreEqual("GB", result.CountryCode);
        }

        [TestCase]
        public void TestLocalIPReturnsNullWithoutException()
        {
            var config = GetConfig();
            var result = service.GetGeoLocation(IPAddress.Parse("127.0.0.1"), config);
            Assert.AreEqual(null, result);
        }
        private NameValueCollection GetConfig() 
            => new NameValueCollection
            {
                {"databaseFileName", @"C:\Pixie\Research\EPiServer\Maxmind.GeoIP\GeoIpTests\db\GeoLite2-City.mmdb"}
            };

    }
}
