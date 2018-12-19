using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace BetterBeer.Objects
{
    public class Friend
    {
        [JsonProperty("UserID")]
        public int UserID { get; set; }
        [JsonProperty("UserName")]
        public string Name { get; set; }
        [JsonProperty("Email")]
        public string EMail { get; set; }
        [JsonProperty("Bild")]
        public string Image { get; set; }
        [JsonProperty("RangID")]
        public int Rang { get; set; }
        public bool actIndicator { get; set; }

        public Friend(string name, string email, string image)
        {
            Name = name;
            EMail = email;
            Image = "http://spbier.bplaced.net/images/userIcon.png";
            actIndicator = false;
        }

        public static List<Friend> friends;
    }
}
