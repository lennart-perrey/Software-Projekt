using BetterBeer.MenuPages;
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
            if(Device.RuntimePlatform == Device.iOS)
            {
                SetStatusStyle.SetStyle();
            }
        }

        public void OnLeftSwipe(View view)
        {
            var scan = new ZXingScannerPage();
            Navigation.PushAsync(scan);

            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    await DisplayAlert("Achtung", result.Text, "Ok");
                });
            };
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

        private void Options_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OptionsPage());
        }

        private void Home_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }

        private void Ranking_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StarPage());
        }

        private void Scan_Tapped(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();
            Navigation.PushAsync(scan);

            scan.OnScanResult += (result) =>
            {
                scan.IsScanning = false;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    await DisplayAlert("Achtung", result.Text, "Ok");
                });
            };
        }
    }
}