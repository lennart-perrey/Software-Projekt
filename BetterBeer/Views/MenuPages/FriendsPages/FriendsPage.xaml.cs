using BetterBeer.MenuPages;
using BetterBeer.Objects;
using BetterBeer.Views.MenuPages.FriendsPages;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BetterBeer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendsPage : ContentPage, ISwipeCallback
    {
        SwipeListener listener;
        List<Friend> friends;
        public Friend SelectedFriend { get; set; }

        public FriendsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);



            listener = new SwipeListener(stlout_Swipe, this);

            if (Device.RuntimePlatform == Device.iOS)
            {
                SetStatusStyle.SetStyle();
                searchBar.BackgroundColor = Color.Black;
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                searchBar.BackgroundColor = Color.White;
                searchBar.WidthRequest = 250;
            }

            friends = Database.GetFriends();
            lv_FriendsList.ItemsSource = friends;

        }

        private async void searchBar_TextChanged(object sender, EventArgs e)
        {
                   
        }

        private async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
     
            Friend friend  =(Friend) lv_FriendsList.SelectedItem;
            await Navigation.PushAsync(new FriendProfile(friend));
        }

        public async void OnLeftSwipe(View view)
        {
            await Navigation.PushAsync(new CustomScanPage(), false);   
        }

        public async void OnNothingSwipe(View view)
        {

        }

        public async void OnRightSwipe(View view)
        {
            await Navigation.PushAsync(new DashBoard(),false);
        }

        public async void OnTopSwipe(View view)
        {

        }

        private async void Options_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OptionsPage(),false);
        }

        private async void Home_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DashBoard(),false);
        }

        private async void Ranking_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StarPage(),false);
        }

        private async void Scan_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CustomScanPage(), false);
        }

        private async void addFriends(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new AddFriendPage(), false);
        }

    }
}