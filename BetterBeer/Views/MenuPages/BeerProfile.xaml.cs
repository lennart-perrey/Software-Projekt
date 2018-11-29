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
            NavigationPage.SetHasNavigationBar(this, false);
            if (Device.RuntimePlatform == Device.iOS)
            {
                SetStatusStyle.SetStyle();
            }
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

        private void Options_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OptionsPage());
        }

        private void Home_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }

        private void Friends_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FriendsPage());
        }
        private async void Scan_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CustomScanPage(), false);
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
            ratingGeschmack = Convert.ToInt32(pickAttr1.Value);
            ratingFarbe = Convert.ToInt32(pickAttr2.Value);
            ratingDesign = Convert.ToInt32(pickAttr3.Value);
            ratingSüff = Convert.ToInt32(pickAttr4.Value);
            ratingKater = Convert.ToInt32(pickAttr1.Value);

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
            switch (pickAttr5.Value)
            {
                case 1:
                    imgAttr1.Source = "oneBeer.png";
                    break;
                case 2:
                    imgAttr1.Source = "twoBeer.png";
                    break;
                case 3:
                    imgAttr1.Source = "threeBeer.png";
                    break;
                case 4:
                    imgAttr1.Source = "fourBeer.png";
                    break;
                case 5:
                    imgAttr1.Source = "fiveBeer.png";
                    break;
                default:
                    break;
            }
        }

        private void pickAttr4_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            switch (pickAttr4.Value)
            {
                case 1:
                    imgAttr1.Source = "oneBeer.png";
                    break;
                case 2:
                    imgAttr1.Source = "twoBeer.png";
                    break;
                case 3:
                    imgAttr1.Source = "threeBeer.png";
                    break;
                case 4:
                    imgAttr1.Source = "fourBeer.png";
                    break;
                case 5:
                    imgAttr1.Source = "fiveBeer.png";
                    break;
                default:
                    break;
            }
        }

        private void pickAttr2_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            switch (pickAttr2.Value)
            {
                case 1:
                    imgAttr1.Source = "oneBeer.png";
                    break;
                case 2:
                    imgAttr1.Source = "twoBeer.png";
                    break;
                case 3:
                    imgAttr1.Source = "threeBeer.png";
                    break;
                case 4:
                    imgAttr1.Source = "fourBeer.png";
                    break;
                case 5:
                    imgAttr1.Source = "fiveBeer.png";
                    break;
                default:
                    break;
            }
        }

        private void pickAttr1_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            switch (pickAttr1.Value)
            {
                case 1:
                    imgAttr1.Source = "oneBeer.png";
                    break;
                case 2:
                    imgAttr1.Source = "twoBeer.png";
                    break;
                case 3:
                    imgAttr1.Source = "threeBeer.png";
                    break;
                case 4:
                    imgAttr1.Source = "fourBeer.png";
                    break;
                case 5:
                    imgAttr1.Source = "fiveBeer.png";
                    break;
                default:
                    break;
            }
        }

        private void pickAttr3_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            switch (pickAttr3.Value)
            {
                case 1:
                    imgAttr1.Source = "oneBeer.png";
                    break;
                case 2:
                    imgAttr1.Source = "twoBeer.png";
                    break;
                case 3:
                    imgAttr1.Source = "threeBeer.png";
                    break;
                case 4:
                    imgAttr1.Source = "fourBeer.png";
                    break;
                case 5:
                    imgAttr1.Source = "fiveBeer.png";
                    break;
                default:
                    break;
            }
        }
    }


}