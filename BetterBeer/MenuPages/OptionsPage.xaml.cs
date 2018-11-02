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
    public partial class OptionsPage : ContentPage, ISwipeCallback
    {
        SwipeListener listener;

        public OptionsPage()
        {
            InitializeComponent();
            listener = new SwipeListener(stlout_Swipe, this);
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public void OnLeftSwipe(View view)
        {
            App.Current.MainPage = new NavigationPage(new StarPage());
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