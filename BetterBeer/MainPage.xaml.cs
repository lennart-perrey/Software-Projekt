using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BetterBeer
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btn_login_clicked(object sender, EventArgs e)
        {
            string email = entry_email.Text;
            string password = entry_password.Text;

            App.Current.MainPage = new NavigationPage(new AppPage());
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            NavigationPage.SetHasNavigationBar(new createAcc(), false);
            await Navigation.PushAsync(new NavigationPage(new createAcc()));
        }
    }
}
