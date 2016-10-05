using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using EPiServer.Personalization;
using System.Collections.Specialized;
using EPiServer.Web;
using System.IO;
using System.Net.Sockets;
using MaxMind.GeoIP2;
using EPiServer.Extensions.Maxmind.GeoIp.Models;

namespace EPiServer.Extensions.Maxmind.GeoIp.Provider
{
    public class MaxmindGeoIp2 : GeolocationProviderBase
    {
        private const string DatabaseParameterName = "databaseFileName";

        private string _maxMindDatabaseFileName = "GeoLite2-City.mmdb";

        private Capabilities _capabilities;

        public override Capabilities Capabilities
        {
            get
            {
                return this._capabilities;
            }
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            string text = config["databaseFileName"];
            if (!string.IsNullOrEmpty(text))
            {
                this._maxMindDatabaseFileName = VirtualPathUtilityEx.RebasePhysicalPath(text);
                config.Remove("databaseFileName");
            }
            if (!File.Exists(this._maxMindDatabaseFileName))
            {
                base.Initialize(name, config);
                return;
            }

            this._capabilities = (Capabilities.Location | Capabilities.ContinentCode | Capabilities.CountryCode | Capabilities.Region);

            base.Initialize(name, config);
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
            if (address.AddressFamily != AddressFamily.InterNetwork)
            {
                return null;
            }
            try
            {
                using (var reader = new DatabaseReader(this._maxMindDatabaseFileName))
                {
                    var dbResult = reader.City(address);

                    if (dbResult == null)
                    {
                        return null;
                    }

                    GeoLocationResult result = new GeoLocationResult();
                    result.CountryCode = dbResult.Country.IsoCode;
                    result.CountryName = dbResult.Country.Name;
                    result.Latitude = (dbResult.Location.Latitude != null) ? dbResult.Location.Latitude.Value : 0;
                    result.Longitude = (dbResult.Location.Longitude != null) ? dbResult.Location.Longitude.Value : 0;
                    result.MetroCode = (dbResult.Location.MetroCode != null) ? dbResult.Location.MetroCode.Value : 0;
                    result.City = dbResult.City.Name;
                    result.PostalCode = dbResult.Postal.Code;
                    result.CountinentCode = dbResult.Continent.Code;

                    return result;

                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}
