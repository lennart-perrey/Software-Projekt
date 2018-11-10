using FreshMvvm;
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
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void btn_login_clicked(object sender, EventArgs e)
        {
            string email = entry_email.Text;
            string SaltedPassword = Database.GetSaltedPW(email);
            string password = HashAndSalt.HashString(String.Format("{0}{1}", entry_password.Text, SaltedPassword));
            if (Database.CheckUser(email, password))
            {
                await Navigation.PushAsync(new MenuPage());
            }
            else
            {
                await DisplayAlert("Fehlgeschlagen", "Anmelden fehlgeschlagen", "Mist");
                entry_email.Text = "";
                entry_password.Text = "";
            }


        }

        private async void lbl_createAcc_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new createAcc()));
        }

        private async void lbl_forgotPassword_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new ForgotPassword()));
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var page = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
            var basicNavContainer = new FreshNavigationContainer(page);

            Navigation.PushAsync(basicNavContainer);
        }
    }
}
