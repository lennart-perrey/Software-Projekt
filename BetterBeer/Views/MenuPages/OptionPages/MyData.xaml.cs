﻿using System;
using System.Collections.Generic;
using System.IO;
using Plugin.Media;

using Xamarin.Forms;

namespace BetterBeer.MenuPages
{
    public partial class MyData : ContentPage
    {
        string password1 = "";
        string password2 = "";

        public MyData()
        {
            InitializeComponent();

            //get Passsword, Picture, Username and EMail from Database
        }

        private async void myImage_Tapped(object sender, EventArgs e)
        {

            await CrossMedia.Current.Initialize();
            bool answer = await DisplayAlert("Profilbild", "Möchtest du ein Bild aufnehmen oder auswählen?", "Aufnehmen", "Auswählen");

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

        private void btn_deleteAcc_Clicked(Object sender, EventArgs e)
        {
            throw new Exception();
        }

        private async void btn_changePassword_Clicked(Object sender, EventArgs e)
        {
            password1 = myPassword.Text;
            password2 = myPassword2.Text;

            showPasswordChange();

        }

        private async void btn_PasswordWasChanged_Clicked(Object sender, EventArgs e)
        {
            showMyData();

            //passwort in Datenbank ändern

        }

        private async void btn_CancelPasswordChange_Clicked(Object sender, EventArgs e)
        {
            myPassword.Text = password1;
            myPassword2.Text = password2;

            showMyData();
        }

        void Handle_TextChangedPaassword(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (myPassword.Text == null || myPassword2.Text == null)
            {
                btn_PasswordWasChanged.IsEnabled = false;
                btn_PasswordWasChanged.BackgroundColor = Color.Gray;
            }
            if (myPassword.Text == "" || myPassword2.Text == "")
            {
                btn_PasswordWasChanged.IsEnabled = false;
                btn_PasswordWasChanged.BackgroundColor = Color.Gray;
            }
            else if (myPassword.Text != null && myPassword2.Text != null && myPassword.Text == myPassword2.Text)
            {
                btn_PasswordWasChanged.IsEnabled = true;
                btn_PasswordWasChanged.BackgroundColor = Color.FromHex("#FFCD33");
            }
        }

        void Handle_TextChangedPaassword2(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (myPassword.Text == null || myPassword2.Text == null)
            {
                btn_PasswordWasChanged.IsEnabled = false;
                btn_PasswordWasChanged.BackgroundColor = Color.Gray;
            }
            if (myPassword.Text == "" || myPassword2.Text == "")
            {
                btn_PasswordWasChanged.IsEnabled = false;
                btn_PasswordWasChanged.BackgroundColor = Color.Gray;
            }
            else if (myPassword.Text != null && myPassword2.Text != null && myPassword.Text == myPassword2.Text)
            {
                btn_PasswordWasChanged.IsEnabled = true;
                btn_PasswordWasChanged.BackgroundColor = Color.FromHex("#FFCD33");
            }
        }

        private void showMyData()
        {
            userName.IsVisible = true;
            myEmail.IsVisible = true;
            btn_ChangePassword.IsVisible = true;

            btn_CancelPasswordChange.IsVisible = false;
            btn_PasswordWasChanged.IsVisible = false;
            myPassword.IsVisible = false;
            myPassword2.IsVisible = false;
        }

        private void showPasswordChange()
        {
            userName.IsVisible = false;
            myEmail.IsVisible = false;
            btn_ChangePassword.IsVisible = false;

            btn_CancelPasswordChange.IsVisible = true;
            btn_PasswordWasChanged.IsVisible = true;
            myPassword.IsVisible = true;
            myPassword2.IsVisible = true;

        }

    }
}
