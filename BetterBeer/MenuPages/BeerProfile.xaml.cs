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
            
            for (int i = 1; i < 6; i++)
            {
               pickGeschmack.Items.Add(i.ToString());
                pickDesign.Items.Add(i.ToString());
                pickSüff.Items.Add(i.ToString());
                pickFarbe.Items.Add(i.ToString());
                pickKater.Items.Add(i.ToString());
            }
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
            ratingGeschmack = Convert.ToInt32(pickGeschmack.SelectedItem);
            ratingFarbe = Convert.ToInt32(pickFarbe.SelectedItem);
            ratingDesign = Convert.ToInt32(pickDesign.SelectedItem);
            ratingSüff = Convert.ToInt32(pickSüff.SelectedItem);
            ratingKater = Convert.ToInt32(pickKater.SelectedItem);

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

      
    }


}