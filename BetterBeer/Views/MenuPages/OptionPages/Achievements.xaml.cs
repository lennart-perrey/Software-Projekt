using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BetterBeer.MenuPages
{
    public partial class Achievements : ContentPage
    {
        public Achievements()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
            {
                SetStatusStyle.SetStyleBlack();
            }
        }
        protected override void OnDisappearing()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                SetStatusStyle.SetStyle();
            }
            base.OnDisappearing();
        }
    }
}
