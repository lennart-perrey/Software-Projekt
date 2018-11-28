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
            await Navigation.PushAsync(new CustomScanPage(), false);
        }
    }
}
