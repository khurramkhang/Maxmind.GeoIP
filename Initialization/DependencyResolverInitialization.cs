using Pixie.Extensions.Maxmind.GeoIp.Services;
using StructureMap.Configuration.DSL;

namespace Pixie.Extensions.Maxmind.GeoIp.Initialization
{
    public class DependencyResolverInitialization : Registry
    {
        public DependencyResolverInitialization()
        {
            For<IGeolocationService>().Use<GeolocationMaxmindService>();
        }
    }
}
