using System;
using UIKit;
using Xamarin.Forms;

namespace BetterBeer
{
    public class SetStatusStyle
    {
        public static void SetStyle()
        {
            UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;
        }
        public static void SetStyleBlack()
        {
            UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.Default;
        }
    }
}

