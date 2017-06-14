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

namespace pokeAScreen
{
    [Activity(Label = "SecondActivity", ParentActivity = typeof(MainActivity))]
    public class SecondActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //create object, set view, set action bar to visible
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Second);
            ActionBar.SetDisplayShowHomeEnabled(true);
            
            //create label and assign text based on extras in intent class
            var label = FindViewById<TextView>(Resource.Id.secondText);
                if (Intent.GetBooleanExtra("poke", false)) label.Text = "Poked by Screen 1!";
                else if (Intent.GetStringExtra("text") != null) label.Text = Intent.GetStringExtra("text");
                else label.Text = "Nothing sent from screen one :(";
        }
    }
}