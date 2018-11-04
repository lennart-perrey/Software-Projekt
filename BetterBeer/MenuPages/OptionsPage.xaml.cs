using BetterBeer.MenuPages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BetterBeer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionsPage : ContentPage, ISwipeCallback
    {
        SwipeListener listener;

        public OptionsPage()
        {
            InitializeComponent();
            listener = new SwipeListener(stlout_Swipe, this);
            NavigationPage.SetHasNavigationBar(this, false);
            if (Device.RuntimePlatform == Device.iOS)
            {
                UIKit.UIApplication.SharedApplication.StatusBarStyle = UIKit.UIStatusBarStyle.LightContent;
            }
        }

        public void OnLeftSwipe(View view)
        {
            App.Current.MainPage = new StarPage();
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