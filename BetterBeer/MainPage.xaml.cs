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
            btn_login.IsEnabled = false;
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

        private void entryMail_TextChanged(object sender, EventArgs e)
        {
            //if (entry_email.Placeholder.ToString() == "EMail / Username" || entry_password.Placeholder.ToString() == "Passwort")
            //{
            //    btn_login.IsEnabled = false;
            //    btn_login.BackgroundColor = Color.Gray;
            //}
            if (entry_email.Text == "" || entry_password.Text == "")
            {
                btn_login.IsEnabled = false;
                btn_login.BackgroundColor = Color.Gray;
            }
            else if (entry_email.Text != "" && entry_password.Text != "")
            {
                btn_login.IsEnabled = true;
                btn_login.BackgroundColor = Color.FromHex("#FFCD33");
            }
        }

        private void entryPassword_TextChanged(object sender, EventArgs e)
        {
            //if (entry_email.Placeholder.ToString() == "EMail / Username" || entry_password.Placeholder.ToString() == "Passwort")
            //{
            //    btn_login.IsEnabled = false;
            //    btn_login.BackgroundColor = Color.Gray;
            //}
            if (entry_email.Text == "" || entry_password.Text == "")
            {
                btn_login.IsEnabled = false;
                btn_login.BackgroundColor = Color.Gray;
            }
            else if (entry_email.Text != "" && entry_password.Text != "")
            {
                btn_login.IsEnabled = true;
                btn_login.BackgroundColor = Color.FromHex("#FFCD33");
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
    }
}
