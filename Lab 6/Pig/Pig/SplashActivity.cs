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
    [Activity(Label = "Pig", Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            string p1Name;
            string p2Name;

            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Main);

            var startButton = FindViewById<Button>(Resource.Id.StartButton);
            var p1Data = FindViewById<EditText>(Resource.Id.p1Value);
            var p2Data = FindViewById<EditText>(Resource.Id.p2Value);

            startButton.Click += delegate
            {


                if (p1Data.Text == null || p1Data.Length() < 1) p1Name = "Player 1";
                else p1Name = p1Data.Text;

                if (p2Data.Text == null || p2Data.Length()<1) p2Name = "Player 2";
                else p2Name = p2Data.Text;

 
                    var game = new Intent(this, typeof(MainActivity));

                    game.PutExtra("p1Name", p1Name);
                    game.PutExtra("p2Name", p2Name);

                    StartActivity(game);

            };





        }
    }
}