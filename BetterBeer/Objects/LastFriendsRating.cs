using System;
using Newtonsoft.Json;

namespace BetterBeer.Objects
{
    public class LastFriendsRating
    {
        [JsonProperty("BewertungID")]
        public int BewertungID { get; set; }
        [JsonProperty("BierID")]
        public int BierID { get; set; }
        [JsonProperty("BierName")]
        public string BierName { get; set; }
        [JsonProperty("Bild")]
        public string Bild { get; set; }
        [JsonProperty("Bewertung")]
        public double Bewertung { get; set; }
        [JsonProperty("KriterienID")]
        public int KriterienID { get; set; }
    }
}
