using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BetterBeer.MenuPages
{
    public partial class AddBeer : ContentPage
    {
        string scanResult = "";

        public AddBeer(string id)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            List<Brand> brands = Database.showBrand();

            foreach(Brand brand in brands)
            {
                picker_BrandName.Items.Add(brand.brandId + "-" + brand.brand);
            }

            scanResult = id;
        }

        private void btn_add_clicked(object sender, EventArgs e)
        {
            string ean = scanResult;
            string beerName = entry_BeerName.Text;
            string beerBrand = picker_BrandName.SelectedItem.ToString();
            string s =picker_BrandName.SelectedItem.ToString();
            string[] s2= s.Split('-');
            int brandID = Convert.ToInt32(s2[0]);
            if (beerName == null || beerBrand == null)
            {
                DisplayAlert("Achtung", "Biername oder Biermarke fehlen", "Ok");
            }
            else
            {
                if(Database.createBeer(ean, beerName, brandID))
                {
                    DisplayAlert("Info", "Bierantrag erfolgreich gesendet", "Ok");
                }
                else
                {
                    DisplayAlert("Fehler", "Bierantrag konnte nicht gesendet werden, bitte probieren Sie es später erneut", "Ok");
                }
                Navigation.PushAsync(new MenuPage());
            }                            
        }

        private void btn_cancel_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }
    }
}
