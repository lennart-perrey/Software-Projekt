using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BetterBeer
{
    public class Beer
    {
        [JsonProperty("MarkenID")]
        public int brandId;

        [JsonProperty("BierId")]
        public int beerId;

        [JsonProperty("Biername")]
        public string beerName;

        [JsonProperty("Marke")]
        public string brand;

        [JsonProperty("Bild")]
        public string pic;

        [JsonProperty("Bewertung")]
        public double avgRating;


        public Beer(int brandId,int beerId, string beerName, string brand,string pic, double avgRating)
        {
            this.brandId = brandId;
            this.beerId = beerId;
            this.beerName = beerName;
            this.brand = brand;
            this.pic = pic;
            this.avgRating = avgRating;
        }
    }
}
