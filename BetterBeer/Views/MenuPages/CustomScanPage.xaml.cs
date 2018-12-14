using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace BetterBeer.MenuPages
{
    public partial class CustomScanPage : ContentPage
    {
        ZXingScannerView zxing;
        ZXingDefaultOverlay overlay;

        public CustomScanPage()
        {
            InitializeComponent();

            if(Device.RuntimePlatform == Device.Android)
            {
                NavigationPage.SetHasBackButton(this, false);
            }



            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "zxingScannerView",
            };
            zxing.OnScanResult += (result) =>
            {
                zxing.IsAnalyzing = false;

                Device.BeginInvokeOnMainThread(() =>
                {
                    // Stop analysis until we navigate away so we don't keep reading barcodes
                    Navigation.PopModalAsync();
                    Beer beer = Database.getBeerByEAN(result.Text);
                    if (beer != null)
                    {
                        Navigation.PushModalAsync(new NavigationPage(new BeerProfile(beer)));
                    }
                    else if (beer == null)
                    {
                        Navigation.PushModalAsync(new NavigationPage(new AddBeer(result.Text)));
                    }

                });
            };


            overlay = new ZXingDefaultOverlay
            {
                
                TopText = "Halte die Kamera auf einen Barcode",
                BottomText = "Es wird automatisch gescannt",
                ShowFlashButton = true,
                AutomationId = "zxingDefaultOverlay",
            };
            overlay.FlashButtonClicked += (sender, e) => {
                zxing.IsTorchOn = !zxing.IsTorchOn;
            };
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            grid.Children.Add(zxing);
            grid.Children.Add(overlay);

            // The root page of your application
            Content = grid;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            zxing.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;

            base.OnDisappearing();
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PushModalAsync(new NavigationPage(new DashBoard()));

            return true;
        }

    }
}