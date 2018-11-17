using System;
using Newtonsoft.Json;

namespace BetterBeer
{
    public class Brand
    {
        [JsonProperty("MarkenID")]
        public int brandId;

        [JsonProperty("Marke")]
        public string brand;


        public Brand(int brandId, string brand)
        {
            this.brandId = brandId;
            this.brand = brand;
        }
    }
}
