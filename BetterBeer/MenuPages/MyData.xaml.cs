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
            var answer = await DisplayAlert("Profilbild", "Möchtest du ein Bild aufnehmen oder auswählen?", "Aufnehmen", "Auswählen");

            if (answer == false)
            {
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
            else if (answer == true)
            {
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Pictures",
                    Name = "ProfilPic.jpg"
                });

                if (file == null)
                    return;


                myImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
            else
            {
                await DisplayAlert("Upps", "Das hat leider nicht geklappt!", "OK");
                return;
            }
        }

        private async void btn_deleteAcc_Clicked (Object sender, EventArgs e)
        {
            throw new Exception();
        }
    }
}
