using System;
using Xamarin.Forms;
using BetterBeer.MenuPages;
using ZXing.Net.Mobile.Forms;
using System.Collections.Generic;
using BetterBeer.Objects;

namespace BetterBeer
{
    public partial class DashBoard : ContentPage, ISwipeCallback
    {
        SwipeListener listener;

        public DashBoard()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            
            listener = new SwipeListener(stlout_Swipe, this);
            if (Device.RuntimePlatform == Device.iOS)
            {
                SetStatusStyle.SetStyle();
            }
            SpecificUser.UserID = Convert.ToInt32(Application.Current.Properties["userID"]);

            //BestBier
            List<Beer> allBeers = Database.Highscore();
            Beer bestBeer = allBeers[0];
            bestBierImg.Source = bestBeer.pic;
            bestBierName.Text = bestBeer.beerName;
            bestBierRating.Text = Convert.ToString(bestBeer.avgRating);


            //FriendRating
            List<FriendRating>friendRatings = Database.showFriendLast(SpecificUser.UserID);
            if(friendRatings.Count > 0){
                FriendRating rating = friendRatings[0];
                int bierId = rating.BierId;
                Beer beer=null;
                foreach (Beer beerdata in allBeers ){
                    if(Convert.ToInt32(beerdata.beerId) == bierId){
                        beer = beerdata;
                    }
                }
                friendRatingBeer.Source = beer.pic;

                Friend friend = Database.ShowUser(SpecificUser.UserID);
                friendRatingName.Text = "Dein Freund "+friend.Name + " hat "+beer.beerName+" bewertet.";

            }
            else{

            }



        }

        void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnAppearing()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                SetStatusStyle.SetStyle();
            }
        }

        public async void OnLeftSwipe(View view)
        {
            await Navigation.PushAsync(new FriendsPage(),false);
        }

        public async void OnNothingSwipe(View view)
        {

        }

        public async void OnRightSwipe(View view)
        {

            await Navigation.PushAsync(new StarPage(),false);
        }

        public void OnTopSwipe(View view)
        {

        }

        private async void Options_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OptionsPage(),false);
        }

        private async void Ranking_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StarPage(),false);
        }

        private async void Friends_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FriendsPage(),false);
        }

        private async void Scan_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CustomScanPage(), false);
        }

    }
}
