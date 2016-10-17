using Pixie.Extensions.Maxmind.GeoIp.Models;
using System.Collections.Specialized;
using System.Net;

namespace Pixie.Extensions.Maxmind.GeoIp.Services
{
    public interface IGeolocationService
    {
        GeoLocationResult GetGeoLocation(IPAddress address, NameValueCollection config);
    }
}
