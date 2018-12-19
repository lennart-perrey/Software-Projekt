using BetterBeer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BetterBeer.Views.MenuPages.FriendsPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FriendProfile : ContentPage
	{
        Friend friend1;
        public FriendProfile (Friend friend)
		{
            InitializeComponent();

            if (Device.RuntimePlatform == Device.Android)
            {
                frame_counter.HeightRequest = 50;
                frame_counter.WidthRequest = 50;
                frame_counter.CornerRadius = 25;
                lbl_Counter.FontSize = 18;
                lbl_Anzahl.FontSize = 18;
            }

            friend1 = friend;
            friendImage.Source = friend.Image;
            this.Title = friend.Name;

            //Set Counter of Rating
            int anzahlBewertungen = Database.showFriendsRatingCounter(friend.UserID);
            lbl_Counter.Text = Convert.ToString(anzahlBewertungen);


            //Set Last Rating of Beer
            List<FriendRating> ratings = Database.getFriendRatingLastBeer(friend.UserID);
            List<Criteria> criterias = RatedBeer.criterias;
            List<LastRatingCarouselView> lastRatingCarouselViews = new List<LastRatingCarouselView>();

            if (ratings != null)
            {
                if(ratings.Count > 10)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        List<LastFriendsRating> LastRatings = Database.getFriendRatingLastBeerByCrit(ratings[i].RatingId);
                        if (LastRatings.Count > 0)
                        {
                            LastRatingCarouselView lastRatingCarouselView = new LastRatingCarouselView(LastRatings[0].BierName, LastRatings[0].Bild, criterias[0].Kriterium, criterias[1].Kriterium,
                            criterias[2].Kriterium, criterias[3].Kriterium, criterias[4].Kriterium, LastRatings[0].Bewertung.ToString(), LastRatings[1].Bewertung.ToString(),
                            LastRatings[2].Bewertung.ToString(), LastRatings[3].Bewertung.ToString(), LastRatings[4].Bewertung.ToString());

                            lastRatingCarouselViews.Add(lastRatingCarouselView);
                        }
                    }
                }
                else
                {
                    foreach (FriendRating rating in ratings)
                    {
                        List<LastFriendsRating> LastRatings = Database.getFriendRatingLastBeerByCrit(rating.RatingId);
                        if (LastRatings.Count > 0)
                        {
                            LastRatingCarouselView lastRatingCarouselView = new LastRatingCarouselView(LastRatings[0].BierName, LastRatings[0].Bild, criterias[0].Kriterium, criterias[1].Kriterium,
                            criterias[2].Kriterium, criterias[3].Kriterium, criterias[4].Kriterium, LastRatings[0].Bewertung.ToString(), LastRatings[1].Bewertung.ToString(),
                            LastRatings[2].Bewertung.ToString(), LastRatings[3].Bewertung.ToString(), LastRatings[4].Bewertung.ToString());

                            lastRatingCarouselViews.Add(lastRatingCarouselView);
                        }
                    }
                }
               
                MainCarouselView.ItemsSource = lastRatingCarouselViews;
            }
            else
            {
                MainCarouselView.IsVisible = false;
                lbl_swipe.IsVisible = false;
                lbl_swipeLine.IsVisible = false;
            }
        }

        private async void btn_cancelFriendship_Clicked(Object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Freundschaft beenden", "Möchtest du die Freundschaft mit " + friend1.Name + " wirklich beenden?", "Ja", "Nein");
            if (answer == false)
            {
                await DisplayAlert("Super", "Na dann Prost!", "OK");
            }
            else if (answer == true)
            {
                answer = await DisplayAlert("Freundschaft beenden", "Bist du dir wirklich sicher?", "Ja", "Nein");
                if (answer == false)
                {
                    await DisplayAlert("Super", "Na dann Prost!", "OK");
                }
                else if (answer == true)
                {
                    if (Database.CancelFriendship(friend1.UserID))
                    {
                        await DisplayAlert("Auf Wiedersehen", "Die Freundschaft mit" + friend1.Name + " wurde beendet :(", "Ok");
                        Friend.friends = Database.GetFriends();
                        BetterBeer.Objects.DashBoard.friendRatingCount = Database.countFriendRatings(SpecificUser.UserID);
                        App.Current.MainPage = new NavigationPage(new FriendsPage());
                    }
                    else
                    {
                        await DisplayAlert("Fehlgeschlagen", "Die Aktion wurde wegen eines Fehlers nicht beendet", "Ok");
                        App.Current.MainPage = new NavigationPage(new FriendsPage());
                    }
                }
            }
        }
    }
}