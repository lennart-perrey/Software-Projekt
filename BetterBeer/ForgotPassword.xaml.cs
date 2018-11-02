using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BetterBeer
{
    public partial class ForgotPassword : ContentPage
    {
        public ForgotPassword()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void btn_sendRequest_clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Achtung", "Hier passiert noch nichts", "OK");
            App.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}