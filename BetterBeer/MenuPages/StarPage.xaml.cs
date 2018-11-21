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
            lv_searchBeer.IsVisible = false;
            listener = new SwipeListener(stlout_Swipe, this);
            NavigationPage.SetHasNavigationBar(this, false);
            if (Device.RuntimePlatform == Device.iOS)
            {
                SetStatusStyle.SetStyle();
            }

            setHighscore();
         }

        /// Erstellt die Ausgabe der Topliste
        private void setHighscore()
        {
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
                Label labelBewertung = new Label { Text = beer.avgRating.ToString(), HorizontalTextAlignment = TextAlignment.End, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), TextColor = Color.DarkKhaki, HorizontalOptions = LayoutOptions.CenterAndExpand, };
                Image pic = new Image { Source = beer.pic, Aspect = Aspect.Fill, WidthRequest = 33, HeightRequest = 60, };

                gridBeer.Children.Add(labelBeerName, 0, 0);
                gridBeer.Children.Add(labelMarke, 0, 1);
                gridBeer.Children.Add(labelBewertung, 1, 0);
                gridBeer.Children.Add(pic, 2, 3, 0, 2);

                highscoreLayout.Children.Add(gridBeer);
            }
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
            await Navigation.PushAsync(new CustomScanPage(),false);
        }


        /* Suchleistung Änderungen
         * */
        private void searchBar_TextChanged(object sender, EventArgs e)
        {
            lv_searchBeer.IsVisible = true;
            highscoreLayout.IsVisible = false;
            string bier = searchBar.Text;
            if (bier == "")
            {   
                lv_searchBeer.IsVisible = false;
                highscoreLayout.IsVisible = true;
                List<string> leer = new List<string>();
                lv_searchBeer.ItemsSource = leer;
                //highscoreLayout.Children.Clear();
                //setHighscore();
            }
            else
            {
                List<Beer> beers = Database.getBeerByName(bier);

                if(beers == null)
                {
                    List<string> leer = new List<string>();
                    lv_searchBeer.ItemsSource = leer;
                }
                else
                {
                    List<string> matchingBeers = new List<string>();
                    foreach (Beer beer in beers)
                    {
                        matchingBeers.Add(beer.beerName);
                    }
                    //Label searchInfo = new Label { Text = "Meinst du:\n" + matchingBeers };
                    //highscoreLayout.Children.Clear();
                    //highscoreLayout.Children.Add(searchInfo);
                    lv_searchBeer.ItemsSource = matchingBeers;
                }
            }
        }

        private void searchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            highscoreLayout.IsVisible = true;
            string bier = searchBar.Text;
            List<Beer> beers= Database.getBeerByName(bier);

            if (beers.Count == 1)
            {
                 Navigation.PushAsync(new BeerProfile(beers[0]));
            }
            else if (beers.Count == 0)
            {
                highscoreLayout.Children.Clear();
                DisplayAlert("Sorry", "Bier leider nicht gefunden", "Mist!");
                setHighscore();
            }
            else if (beers.Count  > 1)
            {
                string matchingBeers = "";
                foreach(Beer beer in beers)
                {
                    matchingBeers += beer.beerName + ", ";
                }
                DisplayAlert("Mehrere Biere gefunden", "Folgende Biere wurden gefunden:\n"+matchingBeers, "YO!");
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