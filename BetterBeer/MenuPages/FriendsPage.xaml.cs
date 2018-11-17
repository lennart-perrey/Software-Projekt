using AVFoundation;
using BetterBeer.MenuPages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;
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
            var scanPage = new ZXingScannerPage();

            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopModalAsync();
                    Beer beer = Database.getBeerByEAN(result.Text);
                    if (beer != null)
                    {
                        Navigation.PushAsync(new BeerProfile(beer));
                    }
                    else if (beer == null)
                    {
                        Navigation.PushAsync(new AddBeer(result.Text));
                    }
                });
            };
            await Navigation.PushAsync(scanPage);
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
            Navigation.PushAsync(new OptionsPage(), false);
        }

        private void Home_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuPage(),false);
        }

        private void Ranking_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StarPage(),false);
        }

        private async void Scan_Tapped(object sender, EventArgs e)
        {
            var scanPage = new ZXingScannerPage();

            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopModalAsync();
                    Beer beer = Database.getBeerByEAN(result.Text);
                    if (beer != null)
                    {
                        Navigation.PushAsync(new BeerProfile(beer));
                    }
                    else if (beer == null)
                    {
                        Navigation.PushAsync(new AddBeer(result.Text));
                    }
                });
            };
            await Navigation.PushAsync(scanPage);
        }
    }
}