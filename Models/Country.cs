using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixie.Extensions.Maxmind.GeoIp.Models
{
    public class Country
    {
        private readonly string _code;

        private readonly string _name;

        public string Code
        {
            get
            {
                return this._code;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        public Country(string code, string name)
        {
            if (code.Length != 2)
            {
                throw new ArgumentException("Invalid country code, should be a two-letter code", "code");
            }
            this._code = code;
            this._name = name;
        }
    }
}
