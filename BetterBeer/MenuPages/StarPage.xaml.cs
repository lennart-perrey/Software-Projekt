using System;
using System.Collections.Generic;
using System.Threading;
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

            //setHighscore();
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
                    Margin = new Thickness(20, 0, 20, 0),

                    RowDefinitions =
                        {
                            new RowDefinition { Height = 75},
                            //new RowDefinition { Height = GridLength.Star}
                        },
                    ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = GridLength.Auto },
                            new ColumnDefinition { Width = GridLength.Auto },
                            new ColumnDefinition { Width = GridLength.Auto }
                        }
                };

                Label labelBeerName = new Label { Text = beer.beerName + " | ", VerticalTextAlignment = TextAlignment.Center, TextColor = Color.Black, HorizontalTextAlignment = TextAlignment.Center, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), HorizontalOptions = LayoutOptions.CenterAndExpand };
                Label labelBewertung = new Label { Text = beer.avgRating.ToString() + " | ", VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), TextColor = Color.Black, HorizontalOptions = LayoutOptions.CenterAndExpand, };
                Image pic = new Image { Source = beer.pic, Aspect = Aspect.AspectFit, HorizontalOptions = LayoutOptions.EndAndExpand };
                //Label labelLine = new Label { BackgroundColor = Color.Gray, HeightRequest = 1, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Fill };

                gridBeer.Children.Add(labelBeerName, 0, 0);
                gridBeer.Children.Add(labelBewertung, 1, 0);
                gridBeer.Children.Add(pic, 2, 0);

                highscoreLayout.Children.Add(gridBeer);

                highscoreLayout.IsVisible = false;

                //Show All Beers at Push from Page

                //List<Beer> beers = Database.ShowBeer();

                //List<string> matchingBeers = new List<string>();
                //foreach(Beer beer1 in beers)
                //{
                  //  matchingBeers.Add(beer1.beerName);
                //}
                //
              //lv_searchBeer.ItemsSource = matchingBeers;
            }
        }



    /*Tar*/
    public void OnLeftSwipe(View view)
        {

            Navigation.PushAsync(new DashBoard(),false);
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
            Navigation.PushAsync(new NavigationPage(new OptionsPage()));
        }

        private void Home_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DashBoard(),false);
        }

        private void Friends_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FriendsPage(),false);
        }
        private void Scan_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CustomScanPage(),false);
        }


        /* Suchleistung Änderungen
         * */
        private async void searchBar_TextChanged(object sender, EventArgs e)
        {
            var cts = new CancellationTokenSource();

            try
            {
                cts.CancelAfter(10000);
                lv_searchBeer.IsVisible = true;
                highscoreLayout.IsVisible = false;
                string bier = searchBar.Text;
                if (bier == "")
                {
                    lv_searchBeer.IsVisible = false;
                    highscoreLayout.IsVisible = true;
                    List<string> leer = new List<string>();
                    lv_searchBeer.ItemsSource = leer;

                }
                else
                {
                    List<Beer> beers = Database.getBeerByName(bier);

                    if (beers == null)
                    {
                        List<string> leer = new List<string>();
                        lv_searchBeer.ItemsSource = leer;
                    }
                    else
                    {
                        List<string> matchingBeers = new List<string>();
                        foreach (Beer beer2 in beers)
                        {
                            matchingBeers.Add(beer2.beerName);
                        }

                        lv_searchBeer.ItemsSource = matchingBeers;
                    }
                }
            }
            catch(OperationCanceledException)
            {
                await DisplayAlert("Fehler", "Ihr Internetverbindung ist zu langsam, bitte versuchen Sie es später erneut.", "Ok");
            }
            catch(Exception ex)
            {
                await DisplayAlert("Fehler", ex.Message, "Ok");
            }

            cts = null;
        }

        private void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {

            List<Beer> beers = Database.getBeerByName(lv_searchBeer.SelectedItem.ToString());
            Beer foundBeer = null;
            foreach(Beer beer in beers)
            {
                foundBeer = beer;
            }

            if (foundBeer != null)
            {
                Navigation.PushAsync(new BeerProfile(foundBeer));
            }
            else
            {
                DisplayAlert("Fehler", "Ups, da ist etwas schief gegangen, bitte probieren Sie es erneut.", "Ok");
            }
        }
    }
}