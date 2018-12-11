using System;
using Xamarin.Forms;
using BetterBeer.MenuPages;
using ZXing.Net.Mobile.Forms;
using System.Collections.Generic;
using BetterBeer.Objects;
using BetterBeer.Views.MenuPages;
using BetterBeer.Views.MenuPages.FriendsPages;

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

                Friend friend = Database.ShowUser(rating.UserID);
                friendRatingName.Text = friend.Name + " hat "+beer.beerName+ "  bewertet.";

            }
            
            else{
                friendRatingName.Text = "Du hast keine Freunde :( ";
            }

            //Rating
            int userRatings = Database.countRatings(SpecificUser.UserID);
            RatingLabel.Text = "Du hast " + userRatings + " Biere bewertet.";

            //FriendRatings
            List<FriendRatingCount> friendRatingCount = Database.countFriendRatings(SpecificUser.UserID);
            if(friendRatingCount.Count >0){
                //foreach(FriendRatingCount frc in friendRatingCount){
                for (int i = 0; i < 3; i++)
                {
                    Friend friend = Database.ShowUser(friendRatingCount[i].UserID);
                    Label label = new Label
                    {
                        Text = friend.Name + ":\t " + friendRatingCount.Count.ToString()
                    };
                    RatingFriendCount.Children.Add(label);
                }

            }
            else{
                Label label = new Label
                {
                    Text = "Los füge schnell welche hinzu."
                };
                RatingFriendCount.Children.Add(label);
            }


            //Random
            Random r = new Random();
            int number =r.Next(allBeers.Count);

            Beer randomBeer = allBeers[number];
            randomImg1.Source = randomBeer.pic;
            randomName1.Text = randomBeer.beerName;

            //Newest
            Beer newestBeer = allBeers[0];
            foreach(Beer newBeer in allBeers){
                if(Convert.ToInt32(newBeer.beerId)> Convert.ToInt32(newestBeer.beerId))
                {
                    newestBeer = newBeer;
                }
            }
            randomImg2.Source = newestBeer.pic;
            randomName2.Text = newestBeer.beerName;

        }

        void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            throw new NotImplementedException();
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

        private async void Frame1_Tapped(object sender, EventArgs e)
        {
            List<Beer> beer = Database.getBeerByName(bestBierName.Text);
            await Navigation.PushAsync(new ShowBeerRating(beer[0]), false);
        }

        private async void Frame2_Tapped(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new FriendProfile(Database.GetFriends())
            await DisplayAlert("Fehler", "Noch nicht besetzt!", "Ok");
        }

        private async void Frame5_Tapped(object sender, EventArgs e)
        {
            List<Beer> beer = Database.getBeerByName(randomName1.Text);
            await Navigation.PushAsync(new ShowBeerRating(beer[0]), false);
        }

        private async void Frame6_Tapped(object sender, EventArgs e)
        {
            List<Beer> beer = Database.getBeerByName(randomName2.Text);
            await Navigation.PushAsync(new ShowBeerRating(beer[0]), false);
        }

    }
}
