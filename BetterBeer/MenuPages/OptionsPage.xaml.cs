using BetterBeer.MenuPages;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace BetterBeer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionsPage : ContentPage, ISwipeCallback
    {
        SwipeListener listener;

        public OptionsPage()
        {
            NavigationPage.SetHasBackButton(this, true);
            InitializeComponent(); 
            listener = new SwipeListener(stlout_Swipe, this);
            if (Device.RuntimePlatform == Device.iOS)
            {
                SetStatusStyle.SetStyle();
            }

            List<string> items = new List<string>();
            items.Add("Meine Daten");
            items.Add("Einstellungen");
            items.Add("Logout");

            listviewGeneral.ItemsSource = items;
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (listviewGeneral.SelectedItem.ToString() == "Meine Daten")
            {
                Navigation.PushAsync(new MyData());
            }
            else if (listviewGeneral.SelectedItem.ToString() == "Einstellungen")
            {
                Navigation.PushAsync(new Options());
            }
        }

        public void OnLeftSwipe(View view)
        {
            Navigation.PushAsync(new StarPage(),false);
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

        private void Home_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuPage(),false);
        }

        private void Ranking_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StarPage(),false);
        }

        private void Friends_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FriendsPage(),false);
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