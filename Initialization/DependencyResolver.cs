using Pixie.Extensions.Maxmind.GeoIp.Services;
using StructureMap;

namespace Pixie.Extensions.Maxmind.GeoIp.Initialization
{
    public class DependencyResolver
    {
        private static IContainer _container;

        private static IContainer Container
        {
            get
            {
                if (_container == null)
                    InitializeContainer();

                return _container;
            }
        }

        private static void InitializeContainer()
        {
            _container = new Container(config => { config.For<IGeolocationService>().Use<GeolocationMaxmindService>(); });
        }

        public static IGeolocationService GeolocationService()
        {
            return Container.GetInstance<IGeolocationService>();
        }
    }
}
