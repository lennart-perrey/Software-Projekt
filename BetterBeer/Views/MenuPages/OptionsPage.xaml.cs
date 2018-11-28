﻿using BetterBeer.MenuPages;
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
            NavigationPage.SetHasNavigationBar(this, false);
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
            else if (ListViewGeneral.SelectedItem.ToString() == "Achievements")
            {
                await Navigation.PushAsync(new Achievements());
            }
            else if (ListViewSystem.SelectedItem.ToString() == "Logout")
            {
                throw new Exception();
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