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

            List<string> itemsGeneral = new List<string>();
            itemsGeneral.Add("Meine Daten");
            itemsGeneral.Add("Einstellungen");
            itemsGeneral.Add("Achievements");

            List<string> itemsSystem = new List<string>();
            itemsSystem.Add("Logout");

            listviewGeneral.ItemsSource = itemsGeneral;
            listviewSystem.ItemsSource = itemsSystem;
        }
    

        private async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (listviewGeneral.SelectedItem.ToString() == "Meine Daten")
            {
               await Navigation.PushAsync(new MyData());
            }
            else if (listviewGeneral.SelectedItem.ToString() == "Einstellungen")
            {
                await Navigation.PushAsync(new Options());
            }
            else if (listviewGeneral.SelectedItem.ToString() == "Achievements")
            {
                await Navigation.PushAsync(new Achievements());
            }
        }

        public async void OnLeftSwipe(View view)
        {

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
    }
}