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

                Label labelBeerName= new Label{Text = beer.beerName, HorizontalTextAlignment = TextAlignment.Center, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), HorizontalOptions = LayoutOptions.CenterAndExpand};
                Label labelMarke = new Label { Text = beer.brand, HorizontalTextAlignment = TextAlignment.Center, FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)), HorizontalOptions = LayoutOptions.CenterAndExpand, };
                Label labelBewertung = new Label { Text = beer.avgRating.ToString(), FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),TextColor=Color.DarkKhaki, HorizontalOptions = LayoutOptions.CenterAndExpand, };
                Label labelPic = new Label { Text = "HIER FOTO",  FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), HorizontalTextAlignment = TextAlignment.End, };


                gridBeer.Children.Add(labelBeerName, 0, 0);
                gridBeer.Children.Add(labelMarke, 0, 1);
                gridBeer.Children.Add(labelBewertung, 1,0);
                gridBeer.Children.Add(labelPic,2,1);

                layout.Children.Add(gridBeer);
            }
          
        }

        /*Toolbar*/
        public void OnLeftSwipe(View view)
        {

            Navigation.PushAsync(new MenuPage());
        }

        public void OnNothingSwipe(View view)
        {

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
        private void Scan_Tapped(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();
            Navigation.PushAsync(scan);

            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    await DisplayAlert("Achtung", result.Text, "Ok");
                });
            };
        }


       



    }
}