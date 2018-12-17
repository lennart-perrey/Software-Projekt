using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BetterBeer.Objects
{
    public class Criteria
    {
        [JsonProperty("KriterienID")]
        public int KriterienID;
        [JsonProperty("BewertungID")]
        public int BewertungID;
        [JsonProperty("Kriterium")]
        public string Kriterium;
        [JsonProperty("Bewertung")]
        public int Bewertung;
        [JsonProperty("deleted_On")]
        public string Deleted_On;
    }
}
