using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BetterBeer.MenuPages
{
    public partial class AddBeer : ContentPage
    {
        public AddBeer()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void btn_add_clicked(object sender, EventArgs e)
        {
            string beerId = entry_BeerID.Text;
            string beerName = entry_BeerName.Text;
            string beerBrand = entry_BrandName.Text;

            if (beerName == null || beerBrand == null)
            {
                DisplayAlert("Achtung", "Biername oder Biermarke fehlen", "Ok");
            }
            else
            {
                //Database.createBeer(beerId, beerName, beerBrand);

                Navigation.PushAsync(new MenuPage());
            }                            
        }

        private void btn_cancel_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }
    }
}
