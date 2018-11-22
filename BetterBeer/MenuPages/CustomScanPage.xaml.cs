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

            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "zxingScannerView",
            };
<<<<<<< HEAD
=======

>>>>>>> c4369bbdaa9dfb079e7dc668b9c7768754f1c993
            zxing.IsScanning = true;
            zxing.OnScanResult += (result) =>
            {
                zxing.IsAnalyzing = false;
                zxing.IsScanning = false;

                Device.BeginInvokeOnMainThread(() =>
                {
                    // Stop analysis until we navigate away so we don't keep reading barcodes
                    Navigation.PopModalAsync();
                    Beer beer = Database.getBeerByEAN(result.Text);
                    if (beer != null)
                    {
                        Navigation.PushModalAsync(new BeerProfile(beer));
                    }
                    else if (beer == null)
                    {
                        Navigation.PushModalAsync(new AddBeer(result.Text));
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


    }
}