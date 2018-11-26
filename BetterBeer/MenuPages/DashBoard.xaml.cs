using System;
using Xamarin.Forms;
using BetterBeer.MenuPages;
using ZXing.Net.Mobile.Forms;

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
            if (Device.RuntimePlatform == Device.iOS)
            {
                SetStatusStyle.SetStyle();
            }
        }

        public void OnLeftSwipe(View view)
        {
           Navigation.PushAsync(new FriendsPage(),false);
        }

        public void OnNothingSwipe(View view)
        {

        }

        public void OnRightSwipe(View view)
        {

            Navigation.PushAsync(new StarPage(),false);
        }

        public void OnTopSwipe(View view)
        {

        }

        private void Options_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OptionsPage(),false);
        }

        private void Ranking_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StarPage(),false);
        }

        private void Friends_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FriendsPage(),false);
        }

        private void Scan_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CustomScanPage(), false);
        }
    }
}
