using BetterBeer.MenuPages;
using BetterBeer.Views.MenuPages.OptionPages;
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
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent(); 
            listener = new SwipeListener(stlout_Swipe, this);

            List<string> itemsGeneral = new List<string>();
            itemsGeneral.Add("Meine Daten");
            itemsGeneral.Add("Einstellungen");

            List<string> itemsSystem = new List<string>();
            itemsSystem.Add("Impressum");
            itemsSystem.Add("Logout");

            ListViewGeneral.ItemsSource = itemsGeneral;
            ListViewSystem.ItemsSource = itemsSystem;
        }

        private async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (ListViewGeneral.SelectedItem.ToString() == "Meine Daten")
            {
               await Navigation.PushAsync(new MyData());
            }
            else if (ListViewGeneral.SelectedItem.ToString() == "Einstellungen")
            {
                await Navigation.PushAsync(new Options());
            }
        }

        private async void HandleSystem_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (ListViewSystem.SelectedItem.ToString() == "Logout")
            {
                Application.Current.Properties["IsLoggedIn"] = Boolean.FalseString;
                SpecificUser.UserID = 0;
                await Navigation.PushAsync(new MainPage(), false);
            }
            else if(ListViewSystem.SelectedItem.ToString() == "Impressum")
            {
                await Navigation.PushAsync(new Impressum());
            }
        }

        public async void OnLeftSwipe(View view)
        {
            await Navigation.PushAsync(new StarPage(), false);
        }

        public async void OnNothingSwipe(View view)
        {

        }

        public async void OnRightSwipe(View view)
        {

        }

        public async void OnTopSwipe(View view)
        {

        }

        private async void Ranking_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StarPage(), false);
        }

        private async void Home_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DashBoard(), false);
        }

        private async void Friends_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FriendsPage(), false);
        }
        private async void Scan_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CustomScanPage(), false);
        }
    }
}