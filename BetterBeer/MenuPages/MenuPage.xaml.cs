using System;


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
            App.Current.MainPage = new FriendsPage();
        }

        public void OnNothingSwipe(View view)
        {

        }

        public void OnRightSwipe(View view)
        {

            App.Current.MainPage = new StarPage();
        }

        public void OnTopSwipe(View view)
        {

        }

        private void Options_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new OptionsPage();
        }

        private void Home_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new MenuPage();
        }
        private void Ranking_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new StarPage();
        }
        private void Friends_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new FriendsPage();
        }

    }
}
