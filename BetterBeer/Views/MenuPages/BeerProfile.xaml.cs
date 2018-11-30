using BetterBeer.Objects;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BetterBeer.MenuPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BeerProfile : ContentPage, ISwipeCallback
	{
        SwipeListener listener;
        List<int> rating = new List<int>();
        List<Criteria> crits;

        int beerID;
        int ratingGeschmack = 0;
        int ratingFarbe = 0;
        int ratingSüff = 0;
        int ratingDesign = 0;
        int ratingKater = 0;
        


        public BeerProfile (Beer scannedBeer)
        {
            InitializeComponent();
            listener = new SwipeListener(stlout_Swipe, this);

            Beer beer = scannedBeer;
            beerID = Convert.ToInt32(beer.beerId);
            lbl_BeerName.Text = beer.beerName;
            img_BeerImage.Source = beer.pic;
            lbl_BeerInfo.Text = beer.info;

            crits = Database.ShowCriteria();

            attr1.Text = crits[0].Kriterium;
            attr2.Text = crits[1].Kriterium;
            attr3.Text = crits[2].Kriterium;
            //attr4.Text = crits[3].Kriterium;
            //attr5.Text = crits[4].Kriterium;
        }

        public void OnRightSwipe(View view)
        {
            Navigation.PushAsync(new OptionsPage());
        }

        public void OnTopSwipe(View view)
        {

        }

        public void OnLeftSwipe(View view)
        {
            Navigation.PushAsync(new MenuPage());
        }

        public void OnNothingSwipe(View view)
        {
           
        }

        private void btn_Submit_Clicked(object sender, EventArgs e)
        {
            ratingGeschmack = Convert.ToInt32(lblAttr1.Text);
            ratingFarbe = Convert.ToInt32(lblAttr2.Text);
            ratingDesign = Convert.ToInt32(lblAttr3.Text);
            ratingSüff = Convert.ToInt32(lblAttr4.Text);
            ratingKater = Convert.ToInt32(lblAttr5.Text);

            rating.Add(ratingGeschmack);
            rating.Add(ratingFarbe);
            rating.Add(ratingDesign);
            rating.Add(ratingSüff);
            rating.Add(ratingKater);

            bool check = Database.CreateRating(beerID, SpecificUser.UserID, rating);

            if (!check)
            {
                DisplayAlert("Fehler! Bier Konnte nicht angelegt werden", "Überprüfe bitte die Eingaben", "Okay");
            }
        }


        private void pickAttr5_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            lblAttr5.Text = pickAttr5.Value.ToString();
        }

        private void pickAttr4_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            lblAttr4.Text = pickAttr4.Value.ToString();
        }

        private void pickAttr2_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            lblAttr2.Text = pickAttr2.Value.ToString();
        }

        private void pickAttr1_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            lblAttr1.Text = pickAttr1.Value.ToString();
        }

        private void pickAttr3_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            lblAttr3.Text = pickAttr3.Value.ToString();
        }
    }


}