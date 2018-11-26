using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BetterBeer.MenuPages
{
    public partial class SearchPage : ContentPage, ISwipeCallback
    {
        SwipeListener listener;

        public SearchPage()
        {
            InitializeComponent();

            listener = new SwipeListener(stlout_Swipe, this);
            NavigationPage.SetHasNavigationBar(this, false);

            if (Device.RuntimePlatform == Device.iOS)
            {
                SetStatusStyle.SetStyle();
            }

            setBeers();
        }

        private void setBeers()
        {
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


        public void OnLeftSwipe(View view)
        {
            throw new NotImplementedException();
        }

        public void OnNothingSwipe(View view)
        {
            throw new NotImplementedException();
        }

        public void OnRightSwipe(View view)
        {
            throw new NotImplementedException();
        }

        public void OnTopSwipe(View view)
        {
            throw new NotImplementedException();
        }

        private void Options_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage(new OptionsPage()));
        }

        private void Home_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DashBoard(), false);
        }

        private void Friends_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FriendsPage(), false);
        }
        private void Scan_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CustomScanPage(), false);
        }

        private void searchBar_TextChanged(object sender, EventArgs e)
        {
            string bier = searchBar.Text;
            if (bier == "")
            {
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

        private void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {

            List<Beer> beers = Database.getBeerByName(lv_searchBeer.SelectedItem.ToString());
            Beer foundBeer = null;
            foreach (Beer beer in beers)
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
