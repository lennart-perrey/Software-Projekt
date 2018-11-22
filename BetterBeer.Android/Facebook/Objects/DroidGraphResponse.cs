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
using BetterBeer.Droid.Objects;

[assembly:Xamarin.Forms.Dependency(typeof(DroidGraphResponse))]
namespace BetterBeer.Droid.Objects
{
    class DroidGraphResponse : IGraphResponse
    {
        public string RawResponse { get; set; }
        public DroidGraphResponse(GraphResponse graphResponse)
        {
            RawResponse = graphResponse.RawResponse;
        }
    }
}