using System;
using Xamarin.Forms;
using BetterBeer.MenuPages;
using ZXing.Net.Mobile.Forms;
using System.Collections.Generic;
using BetterBeer.Objects;
using BetterBeer.Views.MenuPages;
using BetterBeer.Views.MenuPages.FriendsPages;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace BetterBeer
{
    public partial class DashBoard : ContentPage, ISwipeCallback
    {
        SwipeListener listener;
        List<Beer> allBeers = RatedBeer.highscores;

        public DashBoard()
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
            {
                var safeInsets = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();
                safeInsets.Left = 0;
                safeInsets.Top = 40;
                this.Padding = safeInsets;
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                bestBierName.FontSize = 14;
                friendRatingName.FontSize = 14;
                RatingLabel.FontSize = 14;
                randomName1.FontSize = 14;
                randomName2.FontSize = 14;
            }

            listener = new SwipeListener(stlout_Swipe, this);

            //SpecificUser.UserID = Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["userID"]);
            //BestBier
            Beer bestBeer = allBeers[0];
            bestBierImg.Source = bestBeer.pic;
            bestBierName.Text = bestBeer.beerName;
            bestBierRating.Text = Convert.ToString(bestBeer.avgRating);


            //FriendRating
            List<FriendRating> friendRatings = BetterBeer.Objects.DashBoard.friendsRating;
            if(friendRatings.Count > 0)
            {
                FriendRating rating = friendRatings[friendRatings.Count -1];
                int bierId = rating.BierId;
                Beer beer = null;

                foreach (Beer beerdata in allBeers)
                {
                    if(Convert.ToInt32(beerdata.beerId) == bierId)
                    {
                        beer = beerdata;
                    }
                }
                friendRatingBeer.Source = beer.pic;

                Friend friend = BetterBeer.Objects.DashBoard.friend;
                friendRatingName.Text = friend.Name + " hat "+beer.beerName+ "  bewertet.";

            }
            else
            {
                friendRatingName.Text = "Du hast keine Freunde :( ";
            }


            //Rating
            int userRatings = BetterBeer.Objects.DashBoard.count;
            RatingLabel.Text = "Du hast " + userRatings + " Biere bewertet.";


            //FriendRatings
<<<<<<< HEAD
            List<FriendRatingCount> friendRatingCount = BetterBeer.Objects.DashBoard.friendRatingCount;
            if(friendRatingCount.Count >0)
            {
               List<Friend> friendsRatingList = BetterBeer.Objects.DashBoard.friendsRatingList;
               for(int i = 0; i < 3; i++)
=======
            List<FriendRatingCount> friendRatingCount = Database.countFriendRatings(SpecificUser.UserID);
            if(friendRatingCount.Count >0){
                //foreach(FriendRatingCount frc in friendRatingCount){
                for (int i = 0; i < friendRatingCount.Count && i <3; i++)
>>>>>>> ad7afb7... Bugfix, nun können auch Leute mit weniger als 3 Freunden, aber mehr als 1 Freund unsere App nutzen.
                {
                    Label label = new Label
                    {
                        Text = friendsRatingList[i].Name + ":\t " + friendRatingCount[i].RatingCount
                    };
                    RatingFriendCount.Children.Add(label);
                }
            }
            else
            {
                Label label = new Label
                {
                    Text = "Los füge schnell welche hinzu."
                };
                if (Device.RuntimePlatform == Device.Android)
                {
                    label.FontSize = 16;
                }
                RatingFriendCount.Children.Add(label);
            }


            //Random
            Random r = new Random();
            int number =r.Next(allBeers.Count);

            Beer randomBeer = allBeers[number];
            randomImg1.Source = randomBeer.pic;
            randomName1.Text = randomBeer.beerName;


            //Newest
            Beer newestBeer = allBeers[allBeers.Count -1];
            //foreach(Beer newBeer in allBeers){
                //if(Convert.ToInt32(newBeer.beerId)> Convert.ToInt32(newestBeer.beerId))
               //{
                //    newestBeer = newBeer;
                //}
            //}
            randomImg2.Source = newestBeer.pic;
            randomName2.Text = newestBeer.beerName;

        }

        void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnButtomSwipe(View view)
        {

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
            Objects.DashBoard.friendsRating = Database.showFriendLast(SpecificUser.UserID);
            BetterBeer.Objects.DashBoard.count = Database.countRatings(SpecificUser.UserID);
            BetterBeer.Objects.DashBoard.friendRatingCount = Database.countFriendRatings(SpecificUser.UserID);
            BetterBeer.Objects.DashBoard.friend = BetterBeer.Objects.DashBoard.getFriends();
            BetterBeer.Objects.DashBoard.friendsRatingList = BetterBeer.Objects.DashBoard.getFriendsRating();

            //Random
            Random r = new Random();
            int number = r.Next(allBeers.Count);

            Beer randomBeer = allBeers[number];
            randomImg1.Source = randomBeer.pic;
            randomName1.Text = randomBeer.beerName;

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
            await Navigation.PushAsync(new CustomScanPage());
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
