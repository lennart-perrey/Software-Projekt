﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public void OnLeftSwipe(View view)
        {

            App.Current.MainPage = new NavigationPage(new MenuPage());
        }

        public void OnNothingSwipe(View view)
        {

        }

        public void OnRightSwipe(View view)
        {
            App.Current.MainPage = new NavigationPage(new OptionsPage());
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

        private void Friends_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new FriendsPage());
        }
    }
}