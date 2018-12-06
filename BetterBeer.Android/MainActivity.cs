using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using BetterBeer;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;

namespace BetterBeer.Droid
{
    [Activity(Label = "BetterBeer", Icon = "@drawable/BetterBeerIcon", Theme ="@style/MyTheme.Splash", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.Window.RequestFeature(WindowFeatures.ActionBar);
            // Name of the MainActivity theme you had there before.
            // Or you can use global::Android.Resource.Style.ThemeHoloLight
            base.SetTheme(Resource.Style.MainTheme);

            base.OnCreate(savedInstanceState);

            global::ZXing.Net.Mobile.Forms.Android.Platform.Init();
            ImageCircleRenderer.Init();
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        }
    }
}