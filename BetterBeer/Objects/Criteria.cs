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
        [JsonProperty("Kriterium")]
        public string Kriterium;
        [JsonProperty("deleted_On")]
        public string Deleted_On;
    }
}
