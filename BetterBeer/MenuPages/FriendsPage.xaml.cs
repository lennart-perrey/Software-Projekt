using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BetterBeer
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FriendsPage : ContentPage,ISwipeCallback
	{
        SwipeListener listener;

		public FriendsPage()
		{
			InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            listener = new SwipeListener(stlout_Swipe, this);
        }

        public void OnLeftSwipe(View view)
        {
            btn_Options.BackgroundColor = btn_Friends.BackgroundColor;
            btn_Friends.BackgroundColor = btn_Options.BackgroundColor;

            App.Current.MainPage = new NavigationPage(new OptionsPage());
        }

        public void OnNothingSwipe(View view)
        {

        }

        public void OnRightSwipe(View view)
        {

            btn_Dashboard.BackgroundColor = btn_Friends.BackgroundColor;
            btn_Friends.BackgroundColor = btn_Dashboard.BackgroundColor;

            App.Current.MainPage = new NavigationPage(new MenuPage());
        }

        public void OnTopSwipe(View view)
        {

        }
    }
}