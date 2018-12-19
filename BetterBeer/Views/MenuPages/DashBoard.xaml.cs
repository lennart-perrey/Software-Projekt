using System;
using Xamarin.Forms;
using BetterBeer.MenuPages;
using ZXing.Net.Mobile.Forms;
using System.Collections.Generic;
using BetterBeer.Objects;
using BetterBeer.Views.MenuPages;
using BetterBeer.Views.MenuPages.FriendsPages;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using System.Threading.Tasks;

namespace BetterBeer
{
    public partial class DashBoard : ContentPage, ISwipeCallback
    {
        SwipeListener listener;
        SwipeListener listener1;
        SwipeListener listener2;
        SwipeListener listener3;
        SwipeListener listener4;
        SwipeListener listener5;
        SwipeListener listener6;

        List<Beer> allBeers = RatedBeer.highscores;
        Friend LastFriendsRating;

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
            listener1 = new SwipeListener(frame1, this);
            listener2 = new SwipeListener(frame2, this);
            listener3 = new SwipeListener(frame3, this);
            listener4 = new SwipeListener(frame4, this);
            listener5 = new SwipeListener(frame5, this);
            listener6 = new SwipeListener(frame6, this);


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
                FriendRating rating = friendRatings[0];
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
                LastFriendsRating = friend;
                friendRatingName.Text = friend.Name + " hat "+beer.beerName+ " bewertet.";

            }
            else
            {
                friendRatingName.Text = "Du hast keine Freunde :( ";
            }


            //Rating
            int userRatings = BetterBeer.Objects.DashBoard.count;
            RatingLabel.Text = "Du hast " + userRatings + " Biere bewertet.";


            //FriendRatings
            List<FriendRatingCount> friendRatingCount = BetterBeer.Objects.DashBoard.friendRatingCount;
            if(friendRatingCount.Count >0)
            {
               List<Friend> friendsRatingList = BetterBeer.Objects.DashBoard.friendsRatingList;
               for(int i = 0;i< friendRatingCount.Count &&  i < 3; i++)
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

        public async void OnTopSwipe(View view)
        {
            bestBierImg.IsVisible = false;
            friendRatingBeer.IsVisible = false;
            randomImg1.IsVisible = false;
            randomImg2.IsVisible = false;
            act_Indicator1.IsVisible = true;
            act_Indicator2.IsVisible = true;
            act_Indicator3.IsVisible = true;
            act_Indicator4.IsVisible = true;

            await Task.Run(async () =>
            {

                await Task.Delay(500);
            });

            try
            {
                BetterBeer.Objects.DashBoard.friendsRating = Database.showFriendLast(SpecificUser.UserID);
                BetterBeer.Objects.DashBoard.count = Database.countRatings(SpecificUser.UserID);
                BetterBeer.Objects.DashBoard.friendRatingCount = Database.countFriendRatings(SpecificUser.UserID);
                BetterBeer.Objects.DashBoard.friend = BetterBeer.Objects.DashBoard.getFriends();
                BetterBeer.Objects.DashBoard.friendsRatingList = BetterBeer.Objects.DashBoard.getFriendsRating();

                act_Indicator1.IsVisible = false;
                act_Indicator2.IsVisible = false;
                act_Indicator3.IsVisible = false;
                act_Indicator4.IsVisible = false;

                bestBierImg.IsVisible = true;
                friendRatingBeer.IsVisible = true;
                randomImg1.IsVisible = true;
                randomImg2.IsVisible = true;

                //Random
                Random r = new Random();
                int number = r.Next(allBeers.Count);

                Beer randomBeer = allBeers[number];
                randomImg1.Source = randomBeer.pic;
                randomName1.Text = randomBeer.beerName;
            }
            catch(Exception)
            {
                await DisplayAlert("Fehler", "Ups, hier ist etwas schiefgegangen", "Ok");
            }
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
            try
            {
                List<Beer> beer = Database.getBeerByName(bestBierName.Text);
                await Navigation.PushAsync(new ShowBeerRating(beer[0]), false);
            }
            catch (Exception)
            {
                await DisplayAlert("Fehler", "Ups, hier ist etwas schiefgegangen", "Ok");
            }
        }

        private async void Frame2_Tapped(object sender, EventArgs e)
        {
            friendRatingBeer.IsVisible = false;
            await Task.Run(async () =>
            {
                act_Indicator2.IsVisible = true;
                await Task.Delay(500);
            });

            try
            {
                List<Friend> friends = Database.ShowUser(LastFriendsRating.Name);
                await Navigation.PushAsync(new FriendProfile(friends[0]));
                act_Indicator2.IsVisible = false;
                friendRatingBeer.IsVisible = true;
            }
            catch(Exception ex)
            {
                await DisplayAlert("Fehler", ex.Message, "Ok");
            }

        }

        private async void Frame5_Tapped(object sender, EventArgs e)
        {
            try
            {
                List<Beer> beer = Database.getBeerByName(randomName1.Text);
                await Navigation.PushAsync(new ShowBeerRating(beer[0]), false);
            }
            catch (Exception)
            {
                await DisplayAlert("Fehler", "Ups, hier ist etwas schiefgegangen", "Ok");
            }
        }

        private async void Frame6_Tapped(object sender, EventArgs e)
        {
            try
            {
                List<Beer> beer = Database.getBeerByName(randomName2.Text);
                await Navigation.PushAsync(new ShowBeerRating(beer[0]), false);
            }
            catch (Exception)
            {
                await DisplayAlert("Fehler", "Ups, hier ist etwas schiefgegangen", "Ok");
            }
        }

    }
}
