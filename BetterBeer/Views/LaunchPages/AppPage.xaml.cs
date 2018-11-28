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
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private  void btn_login_clicked(object sender, EventArgs e)
        {
             Navigation.PushAsync(new NavigationPage(new DashBoard()));
        }
    }
}
