using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace BetterBeer.MenuPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StarPage : ContentPage, ISwipeCallback
    {
        SwipeListener listener;
        public StarPage()
        {
            InitializeComponent();
            listener = new SwipeListener(stlout_Swipe, this);
            NavigationPage.SetHasNavigationBar(this, false);
            if (Device.RuntimePlatform == Device.iOS)
            {
                SetStatusStyle.SetStyle();
            }



            //string highscore= Database.Highscore();
            List<Beer> highscores = Database.Highscore();
            foreach (Beer beer in highscores)
            {
                Grid gridBeer = new Grid
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    RowDefinitions =
                    {
                        new RowDefinition { Height = GridLength.Auto },
                        new RowDefinition { Height = GridLength.Auto },
                        new RowDefinition { Height = GridLength.Auto  }
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Auto }
                    }
                };

                Label labelBeerName = new Label { Text = beer.beerName, HorizontalTextAlignment = TextAlignment.Center, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), HorizontalOptions = LayoutOptions.CenterAndExpand };
                Label labelMarke = new Label { Text = beer.brand, HorizontalTextAlignment = TextAlignment.Center, FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)), HorizontalOptions = LayoutOptions.CenterAndExpand, };
                Label labelBewertung = new Label { Text = beer.avgRating.ToString(), FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), TextColor = Color.DarkKhaki, HorizontalOptions = LayoutOptions.CenterAndExpand, };
                Label labelPic = new Label { Text = "HIER FOTO", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), HorizontalTextAlignment = TextAlignment.End, };


                gridBeer.Children.Add(labelBeerName, 0, 0);
                gridBeer.Children.Add(labelMarke, 0, 1);
                gridBeer.Children.Add(labelBewertung, 1, 0);
                gridBeer.Children.Add(labelPic, 2, 1);

                layout.Children.Add(gridBeer);
            }
        //highscoreLabel.Text = highscore;
    }

    /*Toolbar*/
    public void OnLeftSwipe(View view)
        {

            Navigation.PushAsync(new MenuPage(),false);
        }

        public void OnNothingSwipe(View view)
        {

        }

        public void OnRightSwipe(View view)
        {
            Navigation.PushAsync(new OptionsPage(),false);
        }

        public void OnTopSwipe(View view)
        {

        }

        private void Options_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OptionsPage(),false);
        }

        private void Home_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuPage(),false);
        }

        private void Friends_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FriendsPage(),false);
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
                    Beer beer = Database.getBeerByEAN(result.Text);
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

        private void searchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            string bier = searchBar.Text;

            if (bier.ToUpper() == "FLENSBURGER")
            {
                //string response = Database.apiCall("showBeer", bier)
                 Navigation.PushAsync(new BeerProfile(Database.getBeerByEAN("41030806")));
            }
            else if (bier == "" || bier == "Suche")
            {
                DisplayAlert("Achtung!", "Bitte gib ein Bier ein!", "OK!");
            }
            else
            {
                DisplayAlert("Sorry", "Bier leider nicht gefunden", "Mist!");
            }
        }
    }
}