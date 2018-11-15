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
        double rating1 = 0;

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
            lbl_BeerName.Text = beer.beerName;
            img_BeerImage.Source = beer.pic;
            //lbl_BeerInfo.Text = beer.info;

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
            var scanPage = new ZXingScannerPage();

            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopModalAsync();
                    Beer beer = Database.getBeerById(result.Text);
                    if (beer != null)
                    {
                        Navigation.PushAsync(new BeerProfile(beer));
                    }
                    else if (beer == null)
                    {
                        Navigation.PushAsync(new AddBeer(result.Text));
                    }
                });
            };
            await Navigation.PushAsync(scanPage);
        }

        public void OnLeftSwipe(View view)
        {
            Navigation.PushAsync(new MenuPage());
        }

        public void OnNothingSwipe(View view)
        {
           
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (img1Attr1.Source.ToString() == "LeeresBier.jpg")
            {
                img1Attr1.Source = "VollesBier.png";
            }
            else
            {
                img1Attr1.Source = "LeeresBier.jpg";
            }
            rating1 = 1;
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            if (img1Attr2.Source.ToString() == "LeeresBier.jpg")
            {
                img1Attr1.Source = "VollesBier.png";
                img2Attr1.Source = "VollesBier.png";
                
            }
            else
            {
                img1Attr1.Source = "LeeresBier.jpg";
                img2Attr1.Source = "LeeresBier.jpg";

            }
            rating1 = 2;
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            if (img1Attr3.Source.ToString() == "LeeresBier.jpg")
            {
                img1Attr1.Source = "VollesBier.png";
                img2Attr1.Source = "VollesBier.png";
                img3Attr1.Source = "VollesBier.png";

            }
            else
            {
                img1Attr1.Source = "LeeresBier.jpg";
                img2Attr1.Source = "LeeresBier.jpg";
                img3Attr1.Source = "LeeresBier.jpg";
            }
            rating1 = 3;
        }
        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            if (img1Attr4.Source.ToString() == "LeeresBier.jpg")
            {
                img1Attr1.Source = "VollesBier.png";
                img2Attr1.Source = "VollesBier.png";
                img3Attr1.Source = "VollesBier.png";
                img4Attr1.Source = "VollesBier.png";

            }
            else
            {
                img1Attr1.Source = "LeeresBier.jpg";
                img2Attr1.Source = "LeeresBier.jpg";
                img3Attr1.Source = "LeeresBier.jpg";
                img4Attr1.Source = "LeeresBier.jpg";
            }
            rating1 = 4;
        }
        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            if (img1Attr5.Source.ToString() == "LeeresBier.jpg")
            {
                img1Attr1.Source = "VollesBier.png";
                img2Attr1.Source = "VollesBier.png";
                img3Attr1.Source = "VollesBier.png";
                img4Attr1.Source = "VollesBier.png";
                img5Attr1.Source = "VollesBier.png";
            }
            else
            {
                img1Attr1.Source = "LeeresBier.jpg";
                img2Attr1.Source = "LeeresBier.jpg";
                img3Attr1.Source = "LeeresBier.jpg";
                img4Attr1.Source = "LeeresBier.jpg";
                img5Attr1.Source = "LeeresBier.jpg";
            }
            rating1 = 5;
        }

        private void btn_Submit_Clicked(object sender, EventArgs e)
        {
            //Database.apiCall("createGrade", rating);
        }
    }
}