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
using Android.Content.PM;

namespace Pig
{
    [Activity(Label = "Pig", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class StartActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);
            var p1Label = FindViewById<TextView>(Resource.Id.playerLabel1);

            if(p1Label!=null)
            {

                var game = new Intent(this, typeof(MainActivity));
                StartActivity(game);

            }
            else
            {

                var game = new Intent(this, typeof(SplashActivity));
                StartActivity(game);
            }
            // Create your application here
        }
    }
}