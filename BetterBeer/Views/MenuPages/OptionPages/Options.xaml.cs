﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BetterBeer.MenuPages
{
    public partial class Options : ContentPage
    {
        public Options()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
            {
                SetStatusStyle.SetStyleBlack();
            }
        }
    }
}