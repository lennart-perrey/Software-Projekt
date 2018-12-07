using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BetterBeer
{
    public class Beer
    {
        [JsonProperty("MarkenID")]
        public string brandId { get; set; }

        [JsonProperty("BierId")]
        public string beerId { get; set; }

        [JsonProperty("Biername")]
        public string beerName { get; set; }

        [JsonProperty("Marke")]
        public string brand { get; set; }

        [JsonProperty("Bild")]
        public string pic { get; set; }

        [JsonProperty("Bewertung")]
        public double avgRating { get; set; }

        [JsonProperty("BierBeschreibung")]
        public string info { get; set; }


        public Beer(string brandId,string beerId, string beerName, string brand,string pic, double avgRating, string info)
        {
            this.brandId = brandId;
            this.beerId = beerId;
            this.beerName = beerName;
            this.brand = brand;
            this.pic = pic;
            this.info = info;
            this.avgRating = Math.Round(avgRating, 1);

            if (this.pic == null)
            {
                this.pic = "http://spbier.bplaced.net/images/beerExample2.png";
            }
        }
    }
}
