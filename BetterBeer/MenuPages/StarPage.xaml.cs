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

            Navigation.PushAsync(new MenuPage());
        }

        public void OnNothingSwipe(View view)
        {

        }

        public void OnRightSwipe(View view)
        {
            Navigation.PushAsync(new OptionsPage());
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

        private void Friends_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FriendsPage());
        }
        private async void Scan_Tapped(object sender, EventArgs e)
        {
            var scanPage = new ZXingScannerPage();

            scanPage.OnScanResult += (result) => {
                // Stop scanning
                scanPage.IsScanning = false;

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(async () => {
                    await Navigation.PopAsync();
                    await DisplayAlert("Scanned Barcode", result.Text, "OK");
                });
            };

            // Navigate to our scanner page
            await Navigation.PushAsync(scanPage);
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