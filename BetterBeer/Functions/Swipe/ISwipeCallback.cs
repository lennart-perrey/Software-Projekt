using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Text;

namespace BetterBeer
{
    public interface ISwipeCallback
    {
        void OnLeftSwipe(View view);
        void OnRightSwipe(View view);
        void OnTopSwipe(View view);
        
        void OnNothingSwipe(View view);
    }
}
