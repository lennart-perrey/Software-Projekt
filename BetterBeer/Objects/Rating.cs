using System;
using Newtonsoft.Json;

namespace BetterBeer.Objects
{
    public class Rating
    {
        [JsonProperty("BierId")]
        public int bierId { get; set; }

        [JsonProperty("KriterienID")]
        public int kriteriumId { get; set; }

        [JsonProperty("Kriterium")]
        public string Kriterium { get; set; }

        [JsonProperty("Bewertung")]
        public double Bewertung { get; set; }


        public Rating(int bierID, int kriteriumID, string kriterium, double bewertung)
        {
            this.bierId = bierID;
            this.kriteriumId = kriteriumID;
            this.Kriterium = kriterium;
            this.Bewertung = Math.Round(bewertung,1);
        }
    }


}
