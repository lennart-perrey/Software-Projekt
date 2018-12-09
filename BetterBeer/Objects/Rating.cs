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
    }
}
