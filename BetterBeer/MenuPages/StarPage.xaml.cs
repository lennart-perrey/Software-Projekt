using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BetterBeer.MenuPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StarPage : ContentPage,ISwipeCallback
	{
        SwipeListener listener;
		public StarPage ()
		{
			InitializeComponent ();
            listener = new SwipeListener(stlout_Swipe, this);

            NavigationPage.SetHasNavigationBar(this, false);
        }

        public void OnLeftSwipe(View view)
        {
            btn_Dashboard.BackgroundColor = btn_Star.BackgroundColor;
            btn_Star.BackgroundColor = btn_Dashboard.BackgroundColor;

            App.Current.MainPage = new NavigationPage(new MenuPage());
        }

        public void OnNothingSwipe(View view)
        {
           
        }

        public void OnRightSwipe(View view)
        {
            
        }

        public void OnTopSwipe(View view)
        {
            
        }
    }
}