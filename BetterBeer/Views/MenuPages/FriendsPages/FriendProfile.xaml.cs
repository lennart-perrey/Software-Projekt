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

            friend1 = friend;
            friendImage.Source = friend.Image;
            this.Title = friend.Name;

            //Set Counter of Rating
            int anzahlBewertungen = Database.showFriendsRatingCounter(friend.UserID);
            lbl_Counter.Text = Convert.ToString(anzahlBewertungen);


            //Set Last Rating of Beer
            List<FriendRating> ratings = Database.getFriendRatingLastBeer(friend.UserID);
            List<Criteria> criterias = RatedBeer.criterias;
            FriendRating friendRating = null;
            List<LastFriendsRating> LastRatings = null;



            if(ratings.Count > 0)
            {
                friendRating = ratings[0];
                LastRatings = Database.getFriendRatingLastBeerByCrit(friendRating.RatingId);

                lbl_crit1.Text = LastRatings[0].Bewertung.ToString();
                lbl_crit2.Text = LastRatings[1].Bewertung.ToString();
                lbl_crit3.Text = LastRatings[2].Bewertung.ToString();
                lbl_crit4.Text = LastRatings[3].Bewertung.ToString();
                lbl_crit5.Text = LastRatings[4].Bewertung.ToString();

                lbl_attr1.Text = criterias[0].Kriterium;
                lbl_attr2.Text = criterias[1].Kriterium;
                lbl_attr3.Text = criterias[2].Kriterium;
                lbl_attr4.Text = criterias[3].Kriterium;
                lbl_attr5.Text = criterias[4].Kriterium;

                lastBierImg.Source = LastRatings[0].Bild;
                lastBierName.Text = LastRatings[0].BierName;
            }
            else
            {
                lastRating.IsVisible = false;
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