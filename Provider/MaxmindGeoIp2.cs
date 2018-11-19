using EPiServer.Personalization;
using Pixie.Extensions.Maxmind.GeoIp.Initialization;
using Pixie.Extensions.Maxmind.GeoIp.Models;
using Pixie.Extensions.Maxmind.GeoIp.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using EPiServer.ServiceLocation;

namespace Pixie.Extensions.Maxmind.GeoIp.Provider
{
    public class MaxmindGeoIp2 : GeolocationProviderBase
    {

        private readonly IGeolocationService geolocationService;
        private const string DatabaseParameterName = "databaseFileName";
        private string maxMindDatabaseFileName = "GeoLite2-City.mmdb";
        NameValueCollection baseConfig = new NameValueCollection();
        NameValueCollection extraConfig = new NameValueCollection();
        private List<string> extraParamsArray;
        private Capabilities capabilities;

        public MaxmindGeoIp2()
        {
            geolocationService = ServiceLocator.Current.GetInstance<IGeolocationService>();
        }

        public MaxmindGeoIp2(IGeolocationService geolocationService)
        {
            this.geolocationService = geolocationService;
        }

        public override Capabilities Capabilities
        {
            get
            {
                return capabilities;
            }
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            for (int i = 0; i < config.Count; i++)
            {
                string key = config.GetKey(i);
                switch (key)
                {
                    case "name":
                    case "type":
                        baseConfig.Add(key, config[i]);
                        break;
                    default:
                        extraConfig.Add(key, config[i]);
                        break;
                }
            }

            capabilities = (Capabilities.Location | Capabilities.ContinentCode | Capabilities.CountryCode | Capabilities.Region);

            base.Initialize(name, baseConfig);
        }

        public override IEnumerable<string> GetContinentCodes()
        {
            return Geographics.Continents.Keys;
        }

        public override IEnumerable<string> GetCountryCodes(string continentCode)
        {
            string foundContinentCode = string.Empty;
            IEnumerable<Country> countries = from country in Geographics.Countries
                                             where !country.Code.Equals("--", StringComparison.Ordinal) && !country.Code.Equals("EU", StringComparison.Ordinal) && !country.Code.Equals("AP", StringComparison.Ordinal) && Geographics.CountryToContinent.TryGetValue(country.Code, out foundContinentCode) && foundContinentCode.Equals(continentCode, StringComparison.OrdinalIgnoreCase)
                                             select country;

            return countries.Select(x => x.Code);
        }

        public override IEnumerable<string> GetRegions(string countryCode)
        {
            if (countryCode == null)
            {
                throw new ArgumentNullException("countryCode");
            }
            IDictionary<string, string> dictionary;
            if (Geographics.Regions.TryGetValue(countryCode, out dictionary))
            {
                return dictionary.Values;
            }
            return new List<string>();
        }

        public override IGeolocationResult Lookup(IPAddress address)
        {
            return geolocationService.GetGeoLocation(address, extraConfig);
        }
    }
}
