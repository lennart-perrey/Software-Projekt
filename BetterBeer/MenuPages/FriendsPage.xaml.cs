using BetterBeer.MenuPages;
using Plugin.Media;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace BetterBeer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendsPage : ContentPage, ISwipeCallback
    {
        SwipeListener listener;

        public FriendsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            listener = new SwipeListener(stlout_Swipe, this);
            if (Device.RuntimePlatform == Device.iOS)
            {               
               SetStatusStyle.SetStyle();
            }
        }

        public async void OnLeftSwipe(View view)
        {
            await Navigation.PushAsync(new CustomScanPage(), false);   
        }

        public async void OnNothingSwipe(View view)
        {

        }

        public async void OnRightSwipe(View view)
        {
            await Navigation.PushAsync(new DashBoard(),false);
        }

        public async void OnTopSwipe(View view)
        {

        }

        private async void Options_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OptionsPage(),false);
        }

        private async void Home_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DashBoard(),false);
        }

        private async void Ranking_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StarPage(),false);
        }

        private async void Scan_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CustomScanPage(), false);
        }


    }
}