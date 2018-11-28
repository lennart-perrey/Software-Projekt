using System;
using System.Collections.Generic;
using System.Threading;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using BetterBeer.Objects;

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
            this.BindingContext = this;
            this.IsBusy = false;
            act_Indicator.IsVisible = false;
            //setHighscore();
        }

        /// Erstellt die Ausgabe der Topliste
        private void setHighscore()
        {
            List<Beer> highscores = Database.Highscore();
            foreach (Beer beer in highscores)
            {
                highscoreLayout.Children.Add(getBeerGrid(beer));
            }
        }

        //Erstellt ein Grid /Label mit Name, Bewertung etc.
        private Grid getBeerGrid(Beer beer)
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

            return gridBeer;
            highscoreLayout.IsVisible = false;

        }



        /*Tar*/
        public async void OnLeftSwipe(View view)
        {
            await Navigation.PushAsync(new DashBoard(), false);
        }

        public async void OnNothingSwipe(View view)
        {

        }

        public async void OnRightSwipe(View view)
        {
            await Navigation.PushAsync(new OptionsPage(), false);
        }

        public async void OnTopSwipe(View view)
        {

        }

        private async void Options_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OptionsPage(), false);
        }

        private async void Home_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DashBoard(), false);
        }

        private async void Friends_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FriendsPage(), false);
        }
        private async void Scan_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CustomScanPage(), false);
        }


        /* Suchleistung Änderungen
         * */
        private async void searchBar_TextChanged(object sender, EventArgs e)
        {
            var cts = new CancellationTokenSource();
            act_Indicator.IsVisible = true;

            try
            {
                cts.CancelAfter(10000);
                this.IsBusy = true;
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
                    act_Indicator.IsVisible = false;
                    this.IsBusy = false;

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
            catch (OperationCanceledException)
            {
                act_Indicator.IsVisible = false;
                await DisplayAlert("Fehler", "Ihr Internetverbindung ist zu langsam, bitte versuchen Sie es später erneut.", "Ok");
                this.IsBusy = false;

            }
            catch (Exception ex)
            {
                act_Indicator.IsVisible = false;
                await DisplayAlert("Fehler", ex.Message, "Ok");
                this.IsBusy = false;
            }

            cts = null;

        }

        private async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            lv_searchBeer.IsVisible = false;
            act_Indicator.IsVisible = true;
            this.IsBusy = true;
            try
            {
                List<Beer> beers = Database.getBeerByName(lv_searchBeer.SelectedItem.ToString());
                this.IsBusy = false;
                Beer foundBeer = null;
                foreach (Beer beer in beers)
                {
                    foundBeer = beer;
                }

                if (foundBeer != null)
                {
                    await Navigation.PushAsync(new BeerProfile(foundBeer));
                }
                else
                {
                    lv_searchBeer.IsVisible = true;
                    act_Indicator.IsVisible = false;
                    this.IsBusy = false;
                    await DisplayAlert("Fehler", "Ups, da ist etwas schief gegangen, bitte probieren Sie es erneut.", "Ok");
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Fehler", "Ups, da ist etwas schief gegangen, bitte probieren Sie es erneut.", "Ok");
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Grid gridSort = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(20, 0, 20, 0),

                RowDefinitions =
                        {
                            new RowDefinition { Height = 40},
                            //new RowDefinition { Height = GridLength.Star}
                        },
                ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = GridLength.Auto },
                            new ColumnDefinition { Width = GridLength.Auto },
                            new ColumnDefinition { Width = GridLength.Auto }

                        }
            };


            List<Criteria> kriterien = Database.ShowCriteria();

            foreach (Criteria krit in kriterien)
            {
                if (krit.Deleted_On == null)
                {
                    Button button = new Button { Text = krit.Kriterium };
                    button.Clicked += delegate { sortByCrit(krit.KriterienID); };
                    highscoreLayout.Children.Add(button);
                }
            }
        }

        private void sortByCrit(int criteriumID)
        {
            List<Beer> highscores = Database.HighscoreForCrit(criteriumID);
            foreach (Beer beer in highscores)
            {
                highscoreLayout.Children.Add(getBeerGrid(beer));
            }
        }
    }
}