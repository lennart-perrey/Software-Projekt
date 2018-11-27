using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BetterBeer.MenuPages
{
    public partial class AddBeer : ContentPage
    {
        List<Brand> brandsList = Database.showBrand();
        IDictionary<string, int> brands = new Dictionary<string, int>();
        string scanResult = "";

        public AddBeer(string id)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);



            foreach(Brand brand in brandsList)
            {
                brands[brand.brand] = brand.brandId;
                picker_BrandName.Items.Add(brand.brand);
            }

            scanResult = id;
        }

        private void btn_add_clicked(object sender, EventArgs e)
        {
            string ean = scanResult;
            string beerName = entry_BeerName.Text;
            string beerBrand = picker_BrandName.SelectedItem.ToString();
            int brandId = brands[beerBrand];


            if (beerName == null || beerBrand == null)
            {
                DisplayAlert("Achtung", "Biername oder Biermarke fehlen", "Ok");
            }
            else
            {
                if(Database.createBeer(ean, beerName, brandId))
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
