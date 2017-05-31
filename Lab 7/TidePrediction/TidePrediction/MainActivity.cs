using Android.App;
using Android.Widget;
using Android.OS;
using TideDBClasses;
using TidePredictionClasses;
using System.Collections.Generic;

namespace TidePrediction
{
    [Activity(Label = "TidePrediction", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        const string DBPATH = "file://android_asset/TidesDB.s3db";
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            List<string> spinList = new List<string>();
            
            // Set our view from the "main" layout resource
             SetContentView (Resource.Layout.Main);

            TideDbAccess tideDb = new TideDbAccess(DBPATH);

            foreach(TideDB tide in tideDb.RetrieveLocales())
            {
                spinList.Add(tide.Locale);
            }


            var spinner = FindViewById<Spinner>(Resource.Id.SpinSelect);
            var spinCont = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, spinList);
            spinCont.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = spinCont;
        }
    }
}

