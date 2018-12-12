using System;
using System.Collections.Generic;

namespace BetterBeer.Objects
{
    public static class DashBoard
    {
        //public static List<Beer> allBeers;
        public static List<FriendRating> friendsRating;
        public static int count;
        public static Friend friend;
        public static List<Friend> friendsRatingList;
        public static List<FriendRatingCount> friendRatingCount;

        public static Friend getFriends()
        {
            FriendRating rating = friendsRating[0];
            friend = Database.ShowUser(rating.UserID);
            return friend;
        }

        public static List<Friend> getFriendsRating()
        {
            List<Friend> tempFriends = new List<Friend>();
            for (int i = 0; i < 3; i++)
            {
                Friend tempFriend= Database.ShowUser(friendRatingCount[i].UserID);
                tempFriends.Add(tempFriend);
            }
            return tempFriends;
        }
    }
}
