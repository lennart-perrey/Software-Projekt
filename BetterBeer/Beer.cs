using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BetterBeer
{
    public class Beer
    {
        [JsonProperty("MarkenID")]
        public string brandId;

        [JsonProperty("BierId")]
        public string beerId;

        [JsonProperty("Biername")]
        public string beerName;

        [JsonProperty("Marke")]
        public string brand;

        [JsonProperty("Bild")]
        public string pic;

        [JsonProperty("Bewertung")]
        public double avgRating;


        public Beer(string brandId,string beerId, string beerName, string brand,string pic, double avgRating)
        {
            this.brandId = brandId;
            this.beerId = beerId;
            this.beerName = beerName;
            this.brand = brand;
            this.pic = pic;
            this.avgRating = avgRating;

            if(this.pic == null)
            {
                this.pic = "http://spbier.bplaced.net/images/beerExample2.png";
            }
        }
    }
}
