using BetterBeer.MenuPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            img_Scan.BackgroundColor = img_Friends.BackgroundColor;
            img_Friends.BackgroundColor = img_Options.BackgroundColor;

            App.Current.MainPage = new NavigationPage(new MenuPage());
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

    }
}