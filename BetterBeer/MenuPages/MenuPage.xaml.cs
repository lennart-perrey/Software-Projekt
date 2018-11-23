using System;
using Xamarin.Forms;
using BetterBeer.MenuPages;
using ZXing.Net.Mobile.Forms;

namespace BetterBeer
{
    public partial class MenuPage : ContentPage, ISwipeCallback
    {
        SwipeListener listener;

        public MenuPage()
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
            Navigation.PushModalAsync(new FriendsPage(),false);
        }

        public void OnNothingSwipe(View view)
        {

        }

        public void OnRightSwipe(View view)
        {

            Navigation.PushModalAsync(new StarPage(),false);
        }

        public void OnTopSwipe(View view)
        {

        }

        private void Options_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new OptionsPage(),false);
        }

        private void Ranking_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new StarPage(),false);
        }

        private void Friends_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new FriendsPage(),false);
        }

        private async void Scan_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CustomScanPage(), false);
        }
    }
}
