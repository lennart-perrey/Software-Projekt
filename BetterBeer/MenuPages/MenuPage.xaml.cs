using System;
using System.Collections.Generic;


using Xamarin.Forms;
using BetterBeer.MenuPages;

namespace BetterBeer
{
    public partial class MenuPage : ContentPage, ISwipeCallback
    {
        SwipeListener listener;

        public MenuPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            listener = new SwipeListener(stlout_Swipe, this);
        }

        public void OnLeftSwipe(View view)
        {
            btn_Dashboard.BackgroundColor = btn_Friends.BackgroundColor;
            btn_Friends.BackgroundColor = btn_Dashboard.BackgroundColor;

            App.Current.MainPage = new NavigationPage(new FriendsPage());
        }

        public void OnNothingSwipe(View view)
        {

        }

        public void OnRightSwipe(View view)
        {
            btn_Star.BackgroundColor = btn_Dashboard.BackgroundColor;
            btn_Dashboard.BackgroundColor = btn_Star.BackgroundColor;

            App.Current.MainPage = new NavigationPage(new StarPage());
        }

        public void OnTopSwipe(View view)
        {

        }

        public void btn_scan_Clicked(object sender, EventArgs e)
        {

        }

        public void btn_Star_Clicked(object sender, EventArgs e)
        {

        }

        private void btn_Dashboard_Clicked(object sender, EventArgs e)
        {

            App.Current.MainPage = new NavigationPage(new MenuPage());
        }

        private void btn_Friends_Clicked(object sender, EventArgs e)
        {
            btn_Dashboard.BackgroundColor = btn_Friends.BackgroundColor;
            btn_Friends.BackgroundColor = btn_Dashboard.BackgroundColor;
            App.Current.MainPage = new NavigationPage(new FriendsPage());
        }

        private void btn_Options_Clicked(object sender, EventArgs e)
        {
            btn_Dashboard.BackgroundColor = btn_Options.BackgroundColor;
            btn_Options.BackgroundColor = btn_Dashboard.BackgroundColor;
            App.Current.MainPage = new NavigationPage(new OptionsPage());
        }
    }
}
