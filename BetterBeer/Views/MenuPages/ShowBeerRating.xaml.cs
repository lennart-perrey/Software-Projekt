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

            //Set CurrentBeer
            beer = currentBeer;
            this.Title = beer.beerName;
            img_BeerImage.Source = beer.pic;

            //Set Counter
            lbl_Counter.Text = Database.countRatings(beer.beerId);

            //Get Criteria
            List<Criteria> criterias = Database.ShowCriteria();

            //Get Rating for Beer
            List<Rating> ratings = Database.getAvgGradeByBeerId(beer.beerId);

            int ratedCrits = 0;
            if(ratings != null){
                ratedCrits= ratings.Count;
            }


            //Leider hard gecodet, weil die Labels feststehen. Bessere lösung, wäre wenn dynamisch.
            if (ratedCrits < 5){
                frame_attr5.IsVisible = false;
                lbl_attr5.IsVisible = false;
                lbl_crit5.IsVisible = false;
            }
            if (ratedCrits < 4)
            {
                frame_attr4.IsVisible = false;
                lbl_attr4.IsVisible = false;
                lbl_crit4.IsVisible = false;
            }
            if (ratedCrits < 3)
            {
                frame_attr3.IsVisible = false;
                lbl_attr3.IsVisible = false;
                lbl_crit3.IsVisible = false;
            }
            if (ratedCrits < 2 )
            {
                frame_attr2.IsVisible = false;
                lbl_attr2.IsVisible = false;
                lbl_crit2.IsVisible = false;
            }
            if (ratedCrits < 1)
            {
                frame_attr1.IsVisible = false;
                lbl_attr1.IsVisible = false;
                lbl_crit1.IsVisible = false;
            }


            if(ratedCrits > 0){
                lbl_attr1.Text = ratings[0].Kriterium;
                lbl_crit1.Text = Convert.ToString(ratings[0].Bewertung);
            }
            if (ratedCrits > 1)
            {
                lbl_attr2.Text = ratings[1].Kriterium;
                lbl_crit2.Text = Convert.ToString(ratings[1].Bewertung);
            }
            if (ratedCrits > 2)
            {
                lbl_attr3.Text = ratings[2].Kriterium;
                lbl_crit3.Text = Convert.ToString(ratings[2].Bewertung);
            }
            if (ratedCrits > 3)
            {
                lbl_attr4.Text = ratings[3].Kriterium;
                lbl_crit4.Text = Convert.ToString(ratings[3].Bewertung);
            }
            if (ratedCrits > 4)
            {
                lbl_attr5.Text = ratings[4].Kriterium;
                lbl_crit5.Text = Convert.ToString(ratings[4].Bewertung);
            }
        }

        private async void btn_RateBeer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BeerProfile(beer));
        }
    }
}