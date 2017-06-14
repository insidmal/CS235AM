using Android.App;
using Android.Widget;
using Android.OS;
using TideDBClasses;
using TidePredictionClasses;
using System.Collections.Generic;
using System.IO;
using Java.Lang;
using System;
using System.Globalization;

namespace TidePrediction
{
    [Activity(Label = "TidePrediction", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            string dbPath;
            List<string> spinList = new List<string>();
            string spinSelected = "";
            
            // Set our view from the "main" layout resource
             SetContentView (Resource.Layout.Main);

            //move db from assets to file system
            dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "TidesDB.s3db");
            if(!File.Exists (dbPath))
            {
                using (Stream inStream = Assets.Open("TidesDB.s3db"))
                using (Stream outStream = File.Create(dbPath))
                    inStream.CopyTo(outStream);
            }

            
            //create object from db layer for population
            TideDbAccess tideDb = new TideDbAccess(dbPath);

            //place locales from db into spinner value list and update spinner with values
            foreach(TideDB tide in tideDb.RetrieveLocales())
            {
                spinList.Add(tide.Locale);
                if (spinSelected == "") spinSelected = tide.Locale;
            }
            Spinner spinner = FindViewById<Spinner>(Resource.Id.SpinSelect);
            var spinCont = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, spinList);
            spinCont.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = spinCont;
            //store selected item selected
            spinner.ItemSelected += (sender, e) => {
                spinSelected = spinner.GetItemAtPosition(e.Position).ToString();

            };

            //datepicker
            var datePicker = FindViewById<DatePicker>(Resource.Id.datePick);
            datePicker.MinDate = Long.ParseLong(ConvertToUnixTimestamp(tideDb.RetrieveDates(-1)[0].Date).ToString());
            datePicker.MaxDate = Long.ParseLong(ConvertToUnixTimestamp(tideDb.RetrieveDates(1)[0].Date).ToString());



            //do it to it!!
            var button = FindViewById<Button>(Resource.Id.submit);
            button.Click += delegate {                
                var go = new Android.Content.Intent(this, typeof(TideList));
                go.PutExtra("Location", spinSelected);
                go.PutExtra("Date",datePicker.DateTime.ToString("yyyy/MM/dd"));
                StartActivity(go);
            };
             

        }

        public static double ConvertToUnixTimestamp(string sdate)
        {
            DateTime date = DateTime.ParseExact(sdate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            // DateTime date = Convert.ToDateTime(sdate);
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return System.Math.Floor(diff.TotalMilliseconds);
        }
    }


}

