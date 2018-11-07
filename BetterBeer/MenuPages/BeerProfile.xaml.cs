using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace BetterBeer.MenuPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BeerProfile : ContentPage, ISwipeCallback
	{
        SwipeListener listener;

		public BeerProfile ()
		{
			InitializeComponent ();
            listener = new SwipeListener(stlout_Swipe, this);
            NavigationPage.SetHasNavigationBar(this, false);
            if (Device.RuntimePlatform == Device.iOS)
            {
                SetStatusStyle.SetStyle();
            }
        }

        private void TextChanged(object sender, EventArgs e)
        {
            
        }

        private async void entry1_TextChanged(object sender, TextChangedEventArgs e)
        {
            try 
            {
                int wert = Convert.ToInt32(this.Content);
                   
                
            }
            catch
            {
                await DisplayAlert("Fehler", "Bitte eine Zahl eingeben", "OK!");
            }

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

        public void OnLeftSwipe(View view)
        {
            Navigation.PushAsync(new MenuPage());
        }

        public void OnNothingSwipe(View view)
        {
           
        }
    }
}