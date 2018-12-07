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
	public partial class FriendProfile : ContentPage
	{
		public FriendProfile (Friend friend)
		{
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
            {
                SetStatusStyle.SetStyleBlack();
            }

            friendImage.Source = friend.Image;
            lbl_Name.Text = friend.Name;

		}
	}
}