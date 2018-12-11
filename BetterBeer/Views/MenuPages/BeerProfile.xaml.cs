using BetterBeer.Objects;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Forms.Grid;

namespace BetterBeer.MenuPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BeerProfile : ContentPage, ISwipeCallback
	{
        SwipeListener listener;
        List<Criteria> crits;

        int beerID;
        


        public BeerProfile (Beer scannedBeer)
        {
            InitializeComponent();
            listener = new SwipeListener(stlout_Swipe, this);

            Beer beer = scannedBeer;
            beerID = Convert.ToInt32(beer.beerId);
            lbl_BeerName.Text = beer.beerName;
            img_BeerImage.Source = beer.pic;
            lbl_BeerInfo.Text = beer.info;

            if(lbl_BeerInfo.Text == null)
            {
                lbl_line.IsVisible = false;
            }

            crits = Database.ShowCriteria();

            attr1.Text = crits[0].Kriterium;
            attr2.Text = crits[1].Kriterium;
            attr3.Text = crits[2].Kriterium;
            attr4.Text = crits[3].Kriterium;
            attr5.Text = crits[4].Kriterium;

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
            Navigation.PushAsync(new DashBoard());
        }

        public void OnNothingSwipe(View view)
        {
           
        }

        private void btn_Submit_Clicked(object sender, EventArgs e)
        {
            List<int> rating = new List<int>();
            rating.Add(Convert.ToInt32(pickAttr1.Value));
            rating.Add(Convert.ToInt32(pickAttr2.Value));
            rating.Add(Convert.ToInt32(pickAttr3.Value));
            rating.Add(Convert.ToInt32(pickAttr4.Value));
            rating.Add(Convert.ToInt32(pickAttr5.Value));


            bool check = Database.CreateRating(beerID, SpecificUser.UserID, rating,crits);

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
        private void btn_GoHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DashBoard());
        }
        private void pickAttr5_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            switch (pickAttr5.Value)
            {
                case 1:
                    imgAttr5.Source = "oneBeer.png";
                    break;
                case 2:
                    imgAttr5.Source = "twoBeer.png";
                    break;
                case 3:
                    imgAttr5.Source = "threeBeer.png";
                    break;
                case 4:
                    imgAttr5.Source = "fourBeer.png";
                    break;
                case 5:
                    imgAttr5.Source = "fiveBeer.png";
                    break;
                case 0:
                    imgAttr5.Source = "LeeresBier.jpg";
                    break;

            }
            Check();
        }

        private void pickAttr4_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            switch (pickAttr4.Value)
            {
                case 1:
                    imgAttr4.Source = "oneBeer.png";
                    break;
                case 2:
                    imgAttr4.Source = "twoBeer.png";
                    break;
                case 3:
                    imgAttr4.Source = "threeBeer.png";
                    break;
                case 4:
                    imgAttr4.Source = "fourBeer.png";
                    break;
                case 5:
                    imgAttr4.Source = "fiveBeer.png";
                    break;
                case 0:
                    imgAttr4.Source = "LeeresBier.jpg";
                    break;
            }
            Check();
        }

        private void pickAttr2_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            switch (pickAttr2.Value)
            {
                case 1:
                    imgAttr2.Source = "oneBeer.png";
                    break;
                case 2:
                    imgAttr2.Source = "twoBeer.png";
                    break;
                case 3:
                    imgAttr2.Source = "threeBeer.png";
                    break;
                case 4:
                    imgAttr2.Source = "fourBeer.png";
                    break;
                case 5:
                    imgAttr2.Source = "fiveBeer.png";
                    break;
                case 0:
                    imgAttr2.Source = "LeeresBier.jpg";
                    break;
            }
            Check();
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

        private void pickAttr3_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            switch (pickAttr3.Value)
            {
                case 1:
                    imgAttr3.Source = "oneBeer.png";
                    break;
                case 2:
                    imgAttr3.Source = "twoBeer.png";
                    break;
                case 3:
                    imgAttr3.Source = "threeBeer.png";
                    break;
                case 4:
                    imgAttr3.Source = "fourBeer.png";
                    break;
                case 5:
                    imgAttr3.Source = "fiveBeer.png";
                    break;
                case 0:
                    imgAttr3.Source = "LeeresBier.jpg";
                    break;
            }
            Check();

        }

        private void Check()
        {
            if (pickAttr1.Value == 0 || pickAttr2.Value == 0 || pickAttr3.Value == 0 || pickAttr4.Value == 0 || pickAttr5.Value == 0)
            {
                btn_Submit.IsEnabled = false;
                btn_Submit.BackgroundColor = Color.Gray;
            }
            else
            {
                btn_Submit.IsEnabled = true;
                btn_Submit.BackgroundColor = Color.FromHex("#FFCD33");
            }
        }
    }
}
