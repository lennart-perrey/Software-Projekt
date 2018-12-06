﻿using System;
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

            if(Device.RuntimePlatform == Device.iOS)
            {
                SetStatusStyle.SetStyleBlack();
            }

            btn_login.IsEnabled = false;
        }

        protected override void OnAppearing()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                SetStatusStyle.SetStyleBlack();
            }
            base.OnDisappearing();
        }

        private async void btn_login_clicked(object sender, EventArgs e)
        {
            try
            {
                string email = entry_email.Text;
                string SaltedPassword = Database.GetSaltedPW(email);
                string password = HashAndSalt.HashString(String.Format("{0}{1}", entry_password.Text, SaltedPassword));
                int userID = Database.CheckUser(email, password);

                if (userID > 0)
                {
                    Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;
                    Application.Current.Properties["userID"] = userID;
                    SpecificUser.UserID = userID;
                    await Navigation.PushAsync(new DashBoard());
                }
                else
                {
                    await DisplayAlert("Fehlgeschlagen", "Anmelden fehlgeschlagen", "Mist");
                    entry_email.Text = "";
                    entry_password.Text = "";
                }
            }
            catch(Exception)
            {
                await DisplayAlert("Fehler", "Ups, hier ist etwas schiefgegangen.", "Ok");
            }

        }

        private void entryMail_TextChanged(object sender, EventArgs e)
        {
            if (entry_email.Text == null || entry_password.Text == null)
            {
                btn_login.IsEnabled = false;
                btn_login.BackgroundColor = Color.Gray;
            }
            if (entry_email.Text == "" || entry_password.Text == "")
            {
                btn_login.IsEnabled = false;
                btn_login.BackgroundColor = Color.Gray;
            }
            else if (entry_email.Text != null && entry_password.Text != null)
            {
                btn_login.IsEnabled = true;
                btn_login.BackgroundColor = Color.FromHex("#FFCD33");
            }
        }

        private void entryPassword_TextChanged(object sender, EventArgs e)
        {

            if (entry_email.Text == null || entry_password.Text == null)
            {
                btn_login.IsEnabled = false;
                btn_login.BackgroundColor = Color.Gray;
            }
            if (entry_email.Text == "" || entry_password.Text == "")
            {
                btn_login.IsEnabled = false;
                btn_login.BackgroundColor = Color.Gray;
            }
            else if (entry_email.Text != null && entry_password.Text != null)
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
