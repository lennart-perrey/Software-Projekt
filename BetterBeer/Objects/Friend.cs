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

        public Friend(  string name, string email, string image)
        {
            Name = name;
            EMail = email;
            Image = image;

            if (Image==null)
            {
                Image = "http://spbier.bplaced.net/images/classicLeon.png";
            }
            
        }
    }
}
