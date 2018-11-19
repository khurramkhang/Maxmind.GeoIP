using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Pixie.Extensions.Maxmind.GeoIp.Services;

namespace Pixie.Extensions.Maxmind.GeoIp.Initialization
{
    [InitializableModule]
    public class GeoIpInitializationModule : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<IGeolocationService, GeolocationMaxmindService>();
        }

        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}
