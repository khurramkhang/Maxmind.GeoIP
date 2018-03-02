using NUnit.Framework;
using Pixie.Extensions.Maxmind.GeoIp.Services;
using System.Collections.Specialized;
using System.Net;

namespace GeoIpTests
{
    [TestFixture]
    public class MaxmindGeoIp2Tests
    {
        private GeolocationMaxmindService service = new GeolocationMaxmindService();

        [TestCase]
        public void MaxmindServiceDBTest()
        {
            NameValueCollection config = new NameValueCollection();
            config.Add("databaseFileName", @"C:\Pixie\Research\EPiServer\Maxmind.GeoIP\GeoIpTests\db\GeoLite2-City.mmdb");
            var result = service.GetGeoLocation(IPAddress.Parse("213.205.251.152"), config);
            Assert.AreEqual("GB", result.CountryCode);
        }

    }
}
