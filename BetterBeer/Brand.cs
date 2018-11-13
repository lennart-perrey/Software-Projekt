using System;
using Newtonsoft.Json;

namespace BetterBeer
{
    public class Brand
    {
        [JsonProperty("MarkenID")]
        public string brandId;

        [JsonProperty("Marke")]
        public string brand;


        public Brand(string brandId, string brand)
        {
            this.brandId = brandId;
            this.brand = brand;
        }
    }
}
