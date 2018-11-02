using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BetterBeer
{
    public partial class createAcc : ContentPage
    {
        public createAcc()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void btn_create_clicked(object sender, EventArgs e)
        {

            string uName = entry_UserName.Text;
            string email = entry_eMail.Text;
            string password = entry_password.Text;
            string password2 = entry_password2.Text;


            if (uName == null || password == null)
            {
                await DisplayAlert("Achtung", "Benutzername oder Passwort fehlen", "Ok");
            }
            else if (password.Equals(password2) == false)
            {
                await DisplayAlert("Achtung", "Passwörter stimmer nicht überein", "Ok");
            }
            else
            {
                if (Database.newUser(uName, email, password))
                {
                    await DisplayAlert("Super", "Dein Account wurde erfolgreich angelegt", "Ok");
                    App.Current.MainPage = new NavigationPage(new MainPage());
                }
                else
                {
                    await DisplayAlert("Fehlgeschlagen", "Dein Account wurde nicht angelegt :(", "Mist");
                }

            }
        }
    }
}