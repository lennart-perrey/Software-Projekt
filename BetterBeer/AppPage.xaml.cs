using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BetterBeer
{
    public partial class AppPage : ContentPage
    {
        public AppPage()
        {
            InitializeComponent();
        }

        private async void btn_login_clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new MenuPage()));
        }
    }
}
