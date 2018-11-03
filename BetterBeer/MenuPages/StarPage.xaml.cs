using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BetterBeer.MenuPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StarPage : ContentPage, ISwipeCallback
    {
        SwipeListener listener;
        public StarPage()
        {
            InitializeComponent();
            listener = new SwipeListener(stlout_Swipe, this);
            NavigationPage.SetHasNavigationBar(this, false);
            if (Device.RuntimePlatform == Device.iOS)
            {
                UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;
            }
        }

        public void OnLeftSwipe(View view)
        {

            App.Current.MainPage = new MenuPage();
        }

        public void OnNothingSwipe(View view)
        {

        }

        public void OnRightSwipe(View view)
        {
            App.Current.MainPage = new OptionsPage();
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

        private void Friends_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new FriendsPage();
        }
    }
}