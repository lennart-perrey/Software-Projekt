﻿using System;
using BetterBeer.Objects;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BetterBeer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            bool isLoggedIn = Current.Properties.ContainsKey("IsLoggedIn") ? Convert.ToBoolean(Current.Properties["IsLoggedIn"]) : false;
          //  int userID = SpecificUser.UserID;
            if (!isLoggedIn)
            {
                //Load if Not Logged In
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                //Load if logged in
                SpecificUser.UserID = Current.Properties.ContainsKey("userID") ? Convert.ToInt32(Current.Properties["userID"]) : 0;
                RatedBeer.highscores = Database.Highscore();
                RatedBeer.criterias = Database.ShowCriteria();
                Objects.DashBoard.friendsRating = Database.showFriendLast(SpecificUser.UserID);
                BetterBeer.Objects.DashBoard.count = Database.countRatings(SpecificUser.UserID);
                BetterBeer.Objects.DashBoard.friendRatingCount = Database.countFriendRatings(SpecificUser.UserID);
                BetterBeer.Objects.DashBoard.friend = BetterBeer.Objects.DashBoard.getFriends();
                BetterBeer.Objects.DashBoard.friendsRatingList = BetterBeer.Objects.DashBoard.getFriendsRating();
                Friend.friends = Database.GetFriends();

                MainPage = new NavigationPage(new DashBoard());
            }

        }

        protected override void OnStart()
        {
            SpecificUser.UserID = Current.Properties.ContainsKey("userID") ? Convert.ToInt32(Current.Properties["userID"]) : 0;
            RatedBeer.highscores = Database.Highscore();
            RatedBeer.criterias = Database.ShowCriteria();
            Objects.DashBoard.friendsRating = Database.showFriendLast(SpecificUser.UserID);
            BetterBeer.Objects.DashBoard.count = Database.countRatings(SpecificUser.UserID);
            BetterBeer.Objects.DashBoard.friendRatingCount = Database.countFriendRatings(SpecificUser.UserID);
            BetterBeer.Objects.DashBoard.friend = BetterBeer.Objects.DashBoard.getFriends();
            BetterBeer.Objects.DashBoard.friendsRatingList = BetterBeer.Objects.DashBoard.getFriendsRating();
            Friend.friends = Database.GetFriends();

            MainPage = new NavigationPage(new DashBoard());

            // Handle when your app starts

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            //Handle when you App resumes
            RatedBeer.highscores = Database.Highscore();
            RatedBeer.criterias = Database.ShowCriteria();
            Objects.DashBoard.friendsRating = Database.showFriendLast(SpecificUser.UserID);
            BetterBeer.Objects.DashBoard.count = Database.countRatings(SpecificUser.UserID);
            BetterBeer.Objects.DashBoard.friendRatingCount = Database.countFriendRatings(SpecificUser.UserID);
            BetterBeer.Objects.DashBoard.friend = BetterBeer.Objects.DashBoard.getFriends();
            BetterBeer.Objects.DashBoard.friendsRatingList = BetterBeer.Objects.DashBoard.getFriendsRating();
            Friend.friends = Database.GetFriends();
        }
    }
}
