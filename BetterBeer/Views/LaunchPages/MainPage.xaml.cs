using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BetterBeer.Objects;
using Xamarin.Forms;

namespace BetterBeer
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            btn_login.IsEnabled = false;
            act_Indicator.IsVisible = false;
        }

        private async void btn_login_clicked(object sender, EventArgs e)
        {
            await Task.Run(async () =>
            {
                act_Indicator.IsVisible = true;
                await Task.Delay(500);

            });

            try
            {
                    string email = entry_email.Text;
                    string SaltedPassword = Database.GetSaltedPW(email).Trim(' ');
                    SaltedPassword = Database.GetSaltedPW(email).Replace(' ', '+');
                    string password = HashAndSalt.HashString(String.Format("{0}{1}", entry_password.Text, SaltedPassword));
                    int userID = Database.CheckUser(email, password);

                    if (userID > 0)
                    {
                        Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;
                        Application.Current.Properties["userID"] = userID;

                        //Set UserID
                        SpecificUser.UserID = userID;

                        //Set Initial Database-Requests
                        RatedBeer.highscores = Database.Highscore();
                        RatedBeer.criterias = Database.ShowCriteria();
                        Objects.DashBoard.friendsRating = Database.showFriendLast(SpecificUser.UserID);
                        BetterBeer.Objects.DashBoard.count = Database.countRatings(SpecificUser.UserID);
                        BetterBeer.Objects.DashBoard.friendRatingCount = Database.countFriendRatings(SpecificUser.UserID);
                        BetterBeer.Objects.DashBoard.friend = BetterBeer.Objects.DashBoard.getFriends();
                        BetterBeer.Objects.DashBoard.friendsRatingList = BetterBeer.Objects.DashBoard.getFriendsRating();
                        Friend.friends = Database.GetFriends();
                        act_Indicator.IsVisible = false;

                        //Push DashBoard
                        await Navigation.PushAsync(new DashBoard());

                    }
                    else
                    {
                        act_Indicator.IsVisible = false;
                        await DisplayAlert("Fehlgeschlagen", "Anmelden fehlgeschlagen", "Mist");
                        entry_email.Text = "";
                        entry_password.Text = "";
                    }
            }
            catch(Exception)
            {
                act_Indicator.IsVisible = false;
                await DisplayAlert("Fehler", "Ups, hier ist etwas schiefgegangen.", "Ok");
                entry_password.Text = "";
            }

        }

        private void entryMail_TextChanged(object sender, EventArgs e)
        {
            if (entry_email.Text == null || entry_password.Text == null)
            {
                btn_login.IsEnabled = false;
                btn_login.BackgroundColor = Color.Gray;
            }
            if (entry_email.Text == "" || entry_password.Text == "")
            {
                btn_login.IsEnabled = false;
                btn_login.BackgroundColor = Color.Gray;
            }
            else if (entry_email.Text != null && entry_password.Text != null)
            {
                btn_login.IsEnabled = true;
                btn_login.BackgroundColor = Color.FromHex("#FFCD33");
            }
        }

        private void entryPassword_TextChanged(object sender, EventArgs e)
        {

            if (entry_email.Text == null || entry_password.Text == null)
            {
                btn_login.IsEnabled = false;
                btn_login.BackgroundColor = Color.Gray;
            }
            if (entry_email.Text == "" || entry_password.Text == "")
            {
                btn_login.IsEnabled = false;
                btn_login.BackgroundColor = Color.Gray;
            }
            else if (entry_email.Text != null && entry_password.Text != null)
            {
                btn_login.IsEnabled = true;
                btn_login.BackgroundColor = Color.FromHex("#FFCD33");
            }
        }

        private async void lbl_createAcc_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new createAcc()));
        }

        private async void lbl_forgotPassword_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new ForgotPassword()));
        }

        private void clickMe(object sender, EventArgs e)
        {
            this.IsBusy = true;
        }
    }
}
