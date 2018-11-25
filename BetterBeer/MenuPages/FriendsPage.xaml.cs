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

        public void OnLeftSwipe(View view)
        {
            Navigation.PushAsync(new CustomScanPage(), false);   
        }

        public void OnNothingSwipe(View view)
        {

        }

        public void OnRightSwipe(View view)
        {
            Navigation.PushAsync(new MenuPage(),false);
        }

        public void OnTopSwipe(View view)
        {

        }

        private void Options_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OptionsPage(),false);
        }

        private void Home_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuPage(),false);
        }

        private void Ranking_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StarPage(),false);
        }

        private void Scan_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CustomScanPage(), false);
        }


    }
}