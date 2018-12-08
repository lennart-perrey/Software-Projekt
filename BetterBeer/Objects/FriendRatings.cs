using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace BetterBeer.Objects
{
    public class FriendRating
    {
        [JsonProperty("UserID")]
        public int UserID { get; set; }
        [JsonProperty("BewertungID")]
        public int RatingId { get; set; }
        [JsonProperty("BierID")]
        public int BierId { get; set; }
        [JsonProperty("created_on")]
        public DateTime date { get; set; }

    }
}
