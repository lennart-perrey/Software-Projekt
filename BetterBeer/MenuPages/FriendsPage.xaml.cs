using BetterBeer.MenuPages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BetterBeer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendsPage : ContentPage, ISwipeCallback
    {
        SwipeListener listener;

        public FriendsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);


            listener = new SwipeListener(stlout_Swipe, this);
        }

        public void OnLeftSwipe(View view)
        {
        }

        public void OnNothingSwipe(View view)
        {

        }

        public void OnRightSwipe(View view)
        {
 

            App.Current.MainPage = new MenuPage();
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

    }
}