using System;
using System.Collections.Generic;

namespace BetterBeer.Objects
{
    public class StarPageModel
    {
        public List<Beer> Beers;

        public StarPageModel()
        {
            Beers = new List<Beer>();
            Beers = Database.Highscore();
        }

    }
}
