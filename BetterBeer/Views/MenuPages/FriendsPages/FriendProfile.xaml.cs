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
		public FriendProfile (Friend friend)
		{
            InitializeComponent();

            friendImage.Source = friend.Image;
            this.Title = friend.Name;
		}

        private async void btn_cancelFriendship_Clicked(Object sender, EventArgs e)
        {
            await DisplayAlert("Fehler", "Freund entfernen geht noch nicht", "Ok");
        }
    }
}