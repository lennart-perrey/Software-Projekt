﻿using BetterBeer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BetterBeer.Views.MenuPages.FriendsPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddFriendPage : ContentPage
	{
        public Friend SelectedFriend { get; set; }
        List<Friend> friends;

        public AddFriendPage()
        {
            InitializeComponent();
      
            friends = Database.GetAllUsers();
            lv_FriendsList.ItemsSource = friends;
        }

        private async void searchBar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string friend = searchBar.Text;

                friends = Database.ShowUser(friend);

                if (friend == null)
                {
                    List<string> leer = new List<string>();
                    lv_FriendsList.ItemsSource = leer;
                }
                else
                {
                    lv_FriendsList.ItemsSource = friends;
                }

            }
            catch (OperationCanceledException)
            {
                await DisplayAlert("Fehler", "Ihr Internetverbindung ist zu langsam, bitte versuchen Sie es später erneut.", "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Fehler", ex.Message, "Ok");
            }
        }

        private async void Handle_ItemTapped(object sender, EventArgs e)
        {
            try
            {
                SelectedFriend = (Friend)lv_FriendsList.SelectedItem;
                foreach (Friend friend in friends)
                {
                    if (friend.Name == SelectedFriend.Name)
                    {
                        if (Database.CheckFriendship(friend.UserID))
                        {
                            await DisplayAlert("Freundschaft schon vorhanden!", "Du bist bereits mit diesem Buddy Befreundet", "Okay!");
                            lv_FriendsList.SelectedItem = null;
                        }
                        else
                        {
                            bool answer = await DisplayAlert("Freundschaft ", "Möchtest du " + friend.Name + "zu deinem Beer Buddy ernennen?", "Prost!", "Nein");
                            if (answer)
                            {
                                Database.CreateFriendship(SelectedFriend.UserID);
                                await Navigation.PushAsync(new FriendsPage());
                                BetterBeer.Objects.DashBoard.friendRatingCount = Database.countFriendRatings(SpecificUser.UserID);
                            }
                            else
                            {
                                lv_FriendsList.ItemsSource = null;
                            }
                        }
                        break;
                    }
                }

            }
            catch (Exception)
            {
                await DisplayAlert("Fehler", "Ups, da ist etwas schief gegangen, bitte probieren Sie es erneut.", "Ok");
            }
        }

    }
}