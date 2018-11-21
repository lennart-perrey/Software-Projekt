using System;
using System.Collections.Generic;
using Plugin.Media;

using Xamarin.Forms;

namespace BetterBeer.MenuPages
{
    public partial class MyData : ContentPage
    {
        public MyData()
        {
            InitializeComponent();
        }

        private async void myImage_Tapped(object sender, EventArgs e)
        {

            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });


            if (file == null)
                return;

            myImage.Source = ImageSource.FromStream(() => file.GetStream());
        }

        private async void btn_deleteAcc_Clicked (Object sender, EventArgs e)
        {
            throw new Exception();
        }
    }
}
