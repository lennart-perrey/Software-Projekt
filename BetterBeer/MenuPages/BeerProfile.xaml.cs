using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace BetterBeer.MenuPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BeerProfile : ContentPage, ISwipeCallback
	{
        SwipeListener listener;
        List<int> rating = new List<int>();
        List<string> crits;
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
            

            crits = Database.ShowCriteria();
            //attr1.Text = crits[0];
            //attr2.Text = crits[1];
            //attr3.Text = crits[2];
            //attr4.Text = crits[3];
            //attr5.Text = crits[4];

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

            bool check = Database.CreateRating(beerID, 0, rating);

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