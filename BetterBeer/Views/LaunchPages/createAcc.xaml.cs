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
            try
            {
                string uName = entry_UserName.Text;
                string email = entry_eMail.Text;
                string value = HashAndSalt.CreateSalt();
                string SaltedPassword= value.Replace(' ', '=');
                SaltedPassword = value.Replace('+', '=');
                string password = HashAndSalt.HashString(String.Format("{0}{1}", entry_password.Text, SaltedPassword));
                string password2 = HashAndSalt.HashString(String.Format("{0}{1}", entry_password2.Text, SaltedPassword));


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
                    if (Database.NewUser(uName, email, password, SaltedPassword))
                    {
                        await DisplayAlert("Super", "Dein Account wurde erfolgreich angelegt", "Ok");
                        App.Current.MainPage = new NavigationPage(new MainPage());
                    }
                    else
                    {
                        await DisplayAlert("Fehlgeschlagen", "Dein Account konnte nicht angelegt werden. \nBenutzername oder Email schon vorhanden", "Mist");
                    }

                }
            }
            catch(Exception)
            {
                await DisplayAlert("Fehler", "Ups, hier ist etwas schiefgegangen", "Ok");
            }


        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (!entry_eMail.Text.Contains("@"))
            {
                entry_eMail.TextColor = Color.Red;
            }
            else
            {
                entry_eMail.TextColor = Color.Black;
            }
        }
    }
}