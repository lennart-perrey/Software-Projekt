using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BetterBeer.Objects
{
    class LastRatingCarouselView
    {
        public string BierName { get; set; }
        public string Bild { get; set; }
        public string Criteria1 { get; set; }
        public string Criteria2 { get; set; }
        public string Criteria3 { get; set; }
        public string Criteria4 { get; set; }
        public string Criteria5 { get; set; }

        public string Rating1 { get; set; }
        public string Rating2 { get; set; }
        public string Rating3 { get; set; }
        public string Rating4 { get; set; }
        public string Rating5 { get; set; }

        public LastRatingCarouselView(string BierName, string Bild,string Criteria1, string Criteria2, string Criteria3, string Criteria4, string Criteria5, string Rating1,
            string Rating2, string Rating3, string Rating4, string Rating5)
        {
            this.BierName = BierName;
            this.Bild = Bild;
            this.Criteria1 = Criteria1;
            this.Criteria2 = Criteria2;
            this.Criteria3 = Criteria3;
            this.Criteria4 = Criteria4;
            this.Criteria5 = Criteria5;

            this.Rating1 = Rating1;
            this.Rating2 = Rating2;
            this.Rating3 = Rating3;
            this.Rating4 = Rating4;
            this.Rating5 = Rating5;

            if (this.Bild == null)
            {
                this.Bild = "http://spbier.bplaced.net/images/beerExample2.png";
            }

        }
    }
}
