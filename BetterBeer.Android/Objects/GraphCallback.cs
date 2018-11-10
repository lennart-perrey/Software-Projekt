using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Facebook;

namespace BetterBeer.Droid.Objects
{
    public class GraphCallback : Java.Lang.Object, GraphRequest.ICallback
    {
        public event EventHandler<GraphResponseEventArgs> RequestCompleted = delegate { };

        public void OnCompleted(GraphResponse response)
        {
            this.RequestCompleted(this, new GraphResponseEventArgs(response));
        }
    }

    public class GraphResponseEventArgs : EventArgs
    {
        GraphResponse _response;
        public GraphResponseEventArgs(GraphResponse response)
        {
            _response = response;
        }

        public GraphResponse Response { get { return _response; } }
    }
}