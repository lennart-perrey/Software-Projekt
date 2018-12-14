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