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
        


        public BeerProfile (Beer scannedBeer)
        {
            InitializeComponent();
            listener = new SwipeListener(stlout_Swipe, this);

            if (Device.RuntimePlatform == Device.iOS)
            {
                SetStatusStyle.SetStyleBlack();
            }

            Beer beer = scannedBeer;
            beerID = Convert.ToInt32(beer.beerId);
            lbl_BeerName.Text = beer.beerName;
            img_BeerImage.Source = beer.pic;
            lbl_BeerInfo.Text = beer.info;

            crits = Database.ShowCriteria();

            foreach (Criteria criteria in crits)
            {
                grd_rating.Children.Add(grd_Criteria);
                
            }

            attr1.Text = crits[0].Kriterium;

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
            foreach (Picker pick in grd_rating.Children)
            {
                rating.Add(pick.SelectedIndex -1 );
            }


            bool check = Database.CreateRating(beerID, SpecificUser.UserID, rating);

            if (!check)
            {
                DisplayAlert("Fehler! Bier Konnte nicht angelegt werden", "Überprüfe bitte deine Eingaben", "Okay");
            }
            else
            {
                DisplayAlert("Super!", "Deine Bewertung wurde angelegt", "Okay");
                Navigation.PushAsync(new DashBoard());
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
                case 0:
                    imgAttr1.Source = "LeeresBier.jpg";
                    break;
            }
            Check();
        }


        private void Check()
        {
            //if (grd_rating.Children.)
            //{
            //    btn_Submit.IsEnabled = false;
            //    btn_Submit.BackgroundColor = Color.Gray;
            //}
            //else
            //{
            //    btn_Submit.IsEnabled = true;
            //    btn_Submit.BackgroundColor = Color.FromHex("#FFCD33");
            //}
        }
    }


}