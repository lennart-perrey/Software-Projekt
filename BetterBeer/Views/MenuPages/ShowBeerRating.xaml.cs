using BetterBeer.MenuPages;
using BetterBeer.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BetterBeer.Views.MenuPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowBeerRating : ContentPage
    {
        Beer beer;
        public ShowBeerRating(Beer currentBeer)
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
            {
                SetStatusStyle.SetStyleBlack();
            }

            //Set CurrentBeer
            beer = currentBeer;
            lbl_BeerName.Text = beer.beerName;
            img_BeerImage.Source = beer.pic;

            //Set Counter
            //lbl_Counter.Text = Database.CountRatings(beer.beerId);

            //Get Criteria
            List<Criteria> criterias = Database.ShowCriteria();

            //Set CriteriaLabels
            lbl_attr1.Text = criterias[0].Kriterium;
            lbl_attr2.Text = criterias[1].Kriterium;
            lbl_attr3.Text = criterias[2].Kriterium;
            lbl_attr4.Text = criterias[3].Kriterium;
            lbl_attr5.Text = criterias[4].Kriterium;

            //Get Rating for Beer
            //List<Beer> beersCrit1 = Database.HighscoreForCrit()

        }

        private async void btn_RateBeer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BeerProfile(beer));
        }
    
        protected override void OnDisappearing()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                SetStatusStyle.SetStyle();
            }
            base.OnDisappearing();
        }
    }
}