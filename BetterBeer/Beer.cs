using System;
using System.Collections.Generic;
using System.Text;

namespace BetterBeer
{
    public class Beer
    {
        public int brandId;
        public int beerId;
        public string beerName;
        public string brand;
        public string pic;
        public double avgRating;

        public Beer(int brandId,int beerId, string beerName, string brand,string pic, double avgRating)
        {
            this.brandId = brandId;
            this.beerId = beerId;
            this.beerName = beerName;
            this.brand = brand;
            this.pic = pic;
            this.avgRating = avgRating;
        }
    }
}
