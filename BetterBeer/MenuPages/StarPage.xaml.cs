using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace BetterBeer.MenuPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StarPage : ContentPage, ISwipeCallback
    {
        SwipeListener listener;
        public StarPage()
        {
            InitializeComponent();
            listener = new SwipeListener(stlout_Swipe, this);
            NavigationPage.SetHasNavigationBar(this, false);
            if (Device.RuntimePlatform == Device.iOS)
            {
                SetStatusStyle.SetStyle();
            }

            

            //string highscore= Database.Highscore();


            //highscoreLabel.Text = highscore;
        }

        /*Toolbar*/
        public void OnLeftSwipe(View view)
        {

            Navigation.PushAsync(new MenuPage(),false);
        }

        public void OnNothingSwipe(View view)
        {

        }

        public void OnRightSwipe(View view)
        {
            Navigation.PushAsync(new OptionsPage(),false);
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

        private void Friends_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FriendsPage(),false);
        }
        private async void Scan_Tapped(object sender, EventArgs e)
        {
            var scanPage = new ZXingScannerPage();

            //iOS
            if (Device.RuntimePlatform == Device.iOS)
            {
                scanPage.OnScanResult += (result) =>
                {

                    scanPage.IsScanning = false;

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Navigation.PopAsync();
                        await DisplayAlert("Scanned Barcode", result.Text, "OK");
                    });
                };

                await Navigation.PushAsync(scanPage);
            }
            //Android
            else if (Device.RuntimePlatform == Device.Android)
            {
                scanPage.OnScanResult += (result) =>
                {

                    scanPage.IsScanning = false;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Navigation.PopModalAsync();
                        DisplayAlert("Scanned Barcode", result.Text, "OK");
                    });
                };
                await Navigation.PushModalAsync(scanPage);
            }
        }

        private void searchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            string bier = searchBar.Text;

            if (bier.ToUpper() == "FLENSBURGER")
            {
                //string response = Database.apiCall("showBeer", bier)
                 Navigation.PushAsync(new BeerProfile());
            }
            else if (bier == "" || bier == "Suche")
            {
                DisplayAlert("Achtung!", "Bitte gib ein Bier ein!", "OK!");
            }
            else
            {
                DisplayAlert("Sorry", "Bier leider nicht gefunden", "Mist!");
            }
        }
    }
}