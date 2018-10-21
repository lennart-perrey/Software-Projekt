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

        private void btn_create_clicked(object sender, EventArgs e)
        {
            string vName = entry_vorName.Text;
            string name = entry_Name.Text;
            string uName = entry_UserName.Text;
            string email = entry_eMail.Text;
            string password = entry_password.Text;
            string password2 = entry_password2.Text;

            App.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}
