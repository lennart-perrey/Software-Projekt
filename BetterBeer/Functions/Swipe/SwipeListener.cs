using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BetterBeer
{
    public class SwipeListener : PanGestureRecognizer
    {
        private ISwipeCallback callback;
        private double translatedX = 0;
        private double translatedY = 0;

        public SwipeListener(View view, ISwipeCallback callback)
        {
            this.callback = callback;
            var gesture = new PanGestureRecognizer();
            gesture.PanUpdated += OnPanUpdated;
            view.GestureRecognizers.Add(gesture);

        }

        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            View content = (View)sender;

            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    try
                    {
                        translatedX = e.TotalX;
                        translatedY = e.TotalY;
                    }
                    catch (Exception err)
                    {

                        System.Diagnostics.Debug.WriteLine("" + err.Message);
                    };
                    break;

                case GestureStatus.Completed:


                    if (translatedX < 0 && Math.Abs(translatedX) > Math.Abs(translatedY))
                    {
                        callback.OnLeftSwipe(content);

                    }
                    else if((translatedX > 0 && Math.Abs(translatedX) > Math.Abs(translatedY)))
                    {
                        callback.OnRightSwipe(content);
                    }
                    else if((translatedX < 0 && Math.Abs(translatedY) > Math.Abs(translatedX)))
                    {
                        callback.OnTopSwipe(content);
                    }
                    else
                    {
                        callback.OnNothingSwipe(content);
                    }
                    break;
            }
        }
    }
}
