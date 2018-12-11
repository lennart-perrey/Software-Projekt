using System;
using Newtonsoft.Json;

namespace BetterBeer.Objects
{
    public class FriendRatingCount
    {

        [JsonProperty("UserID")]
        public int UserID { get; set; }
        [JsonProperty("Bewertungen")]
        public int RatingCount { get; set; }
    }
}
