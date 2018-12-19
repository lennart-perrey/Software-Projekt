using System;
using System.Collections.Generic;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BetterBeer.Objects;
using System.Data;
using BetterBeer.Views.MenuPages;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using System.Windows.Input;

namespace BetterBeer.MenuPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StarPage : ContentPage, ISwipeCallback
    {
        SwipeListener listener;

        //List<Criteria> kriterien = Database.ShowCriteria();
        IDictionary<string, int> criticsDict = new Dictionary<string, int>();
        public Beer SelectedBeer { get; set; }
        List<String> kriterienString;

        public StarPage()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
            {
                var safeInsets = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();
                safeInsets.Left = 0;
                safeInsets.Top = 50;
                this.Padding = safeInsets;
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                btn_Filter.FontSize = 14;
                lbl_Biername.FontSize = 16;
                lbl_Rating.FontSize = 16;
                
                searchBar.WidthRequest = 250;

            }

            listener = new SwipeListener(stlout_Swipe, this);
           // listener1 = new SwipeListener(swipe, this);
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);

            picker_Criteria.IsVisible = false;

            kriterienString = new List<string>();

            foreach (Criteria crit in RatedBeer.criterias)
            {
                if (crit.Deleted_On == null)
                {
                    criticsDict[crit.Kriterium] = crit.KriterienID;
                    kriterienString.Add(crit.Kriterium);
                }
            }

            setHighscore();
        }

        /// Erstellt die Ausgabe der Topliste
        private void setHighscore()
        { 
            lv_highscoreBeer.ItemsSource = RatedBeer.highscores;
        }

        /* Suchleistung Änderungen
         * */
        private async void searchBar_TextChanged(object sender, EventArgs e)
        {
            picker_Criteria.IsVisible = false;

            try
            {
                string bier = searchBar.Text;
                if (bier == "")
                {
                    setHighscore();
                }
                else
                {
                    List<Beer> beers = Database.getBeerByName(bier);

                    if (beers == null)
                    {
                        List<string> leer = new List<string>();
                        lv_highscoreBeer.ItemsSource = leer;
                    }
                    else
                    { 
                        lv_highscoreBeer.ItemsSource = beers;
                    }
                }
            }
            catch (OperationCanceledException)
            {
                await DisplayAlert("Fehler", "Ihr Internetverbindung ist zu langsam, bitte versuchen Sie es später erneut.", "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Fehler", ex.Message, "Ok");
            }
        }

        private async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            try
            {
                SelectedBeer = (Beer)lv_highscoreBeer.SelectedItem;
                List<Beer> beers = Database.getBeerByName(SelectedBeer.beerName);
                Beer foundBeer = null;
                foreach (Beer beer in beers)
                {
                    foundBeer = beer;
                }

                if (foundBeer != null)
                {
                    await Navigation.PushAsync(new ShowBeerRating(foundBeer));
                }
                else
                {
                    await DisplayAlert("Fehler", "Ups, da ist etwas schief gegangen, bitte probieren Sie es erneut.", "Ok");
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Fehler", "Ups, da ist etwas schief gegangen, bitte probieren Sie es erneut.", "Ok");
            }
        }

        private void btn_Filter_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (picker_Criteria.IsVisible == true)
                {
                    setHighscore();
                    btn_Filter.Text = "Filter";
                    picker_Criteria.IsVisible = false;
                }
                else
                {
                    picker_Criteria.IsVisible = true;
                    btn_Filter.Text = "Cancel";

                    picker_Criteria.ItemsSource = kriterienString;
                    picker_Criteria.SelectedIndex = 0;
                }
            }
            catch(Exception)
            {
                DisplayAlert("Fehler", "Ups, da ist etwas schief gelaufen!", "Ok");
            }
        }

        void picker_SelectedItemChanged(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            try
            {
                int critID = criticsDict[picker_Criteria.SelectedItem.ToString()];
                if(critID > 0)
                {
                    List<Beer> beersByCrit = Database.HighscoreForCrit(critID);
                    lv_highscoreBeer.ItemsSource = beersByCrit;
                }
            }
            catch(Exception)
            {
                DisplayAlert("Fehler", "Ups, da ist etwas schief gelaufen!", "Ok");
            }
        }



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
            await Navigation.PushAsync(new CustomScanPage());
        }

        private async void onRefresh (object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            
            try
            {
                lv_highscoreBeer.IsRefreshing = true;
                lv_highscoreBeer.ItemsSource = Database.Highscore();
                lv_highscoreBeer.IsRefreshing = false;
            }
            catch (Exception)
            {
                await DisplayAlert("Fehler", "Ups, da ist etwas schief gegangen, bitte probieren Sie es erneut.", "Ok");
                lv_highscoreBeer.IsRefreshing = false;
            }
        }
    }
}