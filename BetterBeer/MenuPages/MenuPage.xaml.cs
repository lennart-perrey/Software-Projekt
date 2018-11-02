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
            img_Ranking.BackgroundColor = img_Friends.BackgroundColor;
            img_Friends.BackgroundColor = img_Ranking.BackgroundColor;

            App.Current.MainPage = new NavigationPage(new FriendsPage());
        }

        public void OnNothingSwipe(View view)
        {

        }

        public void OnRightSwipe(View view)
        {
            img_Ranking.BackgroundColor = img_Friends.BackgroundColor;
            img_Friends.BackgroundColor = img_Ranking.BackgroundColor;

            App.Current.MainPage = new NavigationPage(new StarPage());
        }

        public void OnTopSwipe(View view)
        {

        }

        private void Options_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new OptionsPage());
        }

        private void Home_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new MenuPage());
        }
        private void Ranking_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new StarPage());
        }
        private void Friends_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new FriendsPage());
        }

    }
}
