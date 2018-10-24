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
	public partial class OptionsPage : ContentPage,ISwipeCallback
	{
        SwipeListener listener;

		public OptionsPage()
		{
			InitializeComponent ();
            listener = new SwipeListener(stlout_Swipe, this);
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public void OnLeftSwipe(View view)
        {

        }

        public void OnNothingSwipe(View view)
        {

        }

        public void OnRightSwipe(View view)
        {

            btn_Friends.BackgroundColor = btn_Options.BackgroundColor;
            btn_Options.BackgroundColor = btn_Friends.BackgroundColor;

            App.Current.MainPage = new NavigationPage(new FriendsPage());
        }

        public void OnTopSwipe(View view)
        {

        }
    }
}