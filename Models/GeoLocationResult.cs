using EPiServer.Personalization;
using System;

namespace Pixie.Extensions.Maxmind.GeoIp.Models
{
    public class GeoLocationResult : IGeolocationResult
    {
        public string CountinentCode
        {
            get;
            set;
        }

        public string CountryCode
        {
            get;
            set;
        }

        public string CountryName
        {
            get;
            set;
        }

        public string Region
        {
            get;
            set;
        }

        public string City
        {
            get;
            set;
        }

        public string PostalCode
        {
            get;
            set;
        }

        public double Latitude
        {
            get;
            set;
        }

        public double Longitude
        {
            get;
            set;
        }

        public int DmaCode
        {
            get;
            set;
        }

        public int AreaCode
        {
            get;
            set;
        }

        public string RegionName
        {
            get;
            set;
        }

        public int MetroCode
        {
            get;
            set;
        }

        string IGeolocationResult.ContinentCode
        {
            get
            {
                return this.CountinentCode;
            }
        }

        string IGeolocationResult.CountryCode
        {
            get
            {
                return this.CountryCode;
            }
        }

        TimeZoneInfo IGeolocationResult.TimeZone
        {
            get
            {
                return null;
            }
        }

        GeoCoordinate IGeolocationResult.Location
        {
            get
            {
                return new GeoCoordinate(this.Latitude, this.Longitude);
            }
        }

        string IGeolocationResult.Region
        {
            get
            {
                return this.RegionName;
            }
        }
    }
}
