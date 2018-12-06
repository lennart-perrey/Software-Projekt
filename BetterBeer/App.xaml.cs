using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BetterBeer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            bool isLoggedIn = Current.Properties.ContainsKey("IsLoggedIn") ? Convert.ToBoolean(Current.Properties["IsLoggedIn"]) : false;
           // int userID = SpecificUser.UserID;
            if (!isLoggedIn)
            {
                //Load if Not Logged In
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                //Load if Logged In
                MainPage = new NavigationPage(new DashBoard());
            }

        }

        protected override void OnStart()
        {
            // Handle when your app starts
            SpecificUser.UserID = Current.Properties.ContainsKey("userID") ? Convert.ToInt32(Current.Properties["userID"]) : 0;
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
