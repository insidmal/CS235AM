using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System.Collections.Generic;
using Android.Runtime;
using TidePredictionClasses;

namespace TidePrediction
{
    [Activity(Label = "TidePrediction", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ListActivity
    {
        List<Tides> tides = new List<Tides>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);


            tides = ConvertXMLtoTides.ConvertTides(Assets.Open(@"tidepredict.xml"));

            #region manual tides

            /*
            Tides t1 = new Tides();
            t1.Date = "a";
            t1.Day = "a";
            t1.Cen = 1;
            t1.Feet = 1;
            t1.HL = "High";
            t1.Time = "1";
            t1.Index = 11;
            Tides t2 = new Tides();
            t2.Date = "b";
            t2.Day = "b";
            t2.Cen = 2;
            t2.Feet = 2;
            t2.HL = "Low";
            t2.Time = "2";
            t2.Index = 22;

            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t1);
            tides.Add(t2);
            tides.Add(t2);
            tides.Add(t2); tides.Add(t2); tides.Add(t2);
            tides.Add(t2); tides.Add(t2); tides.Add(t2);
            tides.Add(t1);
            tides.Add(t2);
            tides.Add(t1);
            tides.Add(t2);
            tides.Add(t1);
            tides.Add(t2);
            */
            #endregion

            ListAdapter = new TidesAdapter(this, tides);

            ListView.FastScrollEnabled = true;



        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            string word = tides[position].Feet.ToString() ;
            Android.Widget.Toast.MakeText(this, word, Android.Widget.ToastLength.Short).Show();
        }
    }
}

