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
using System.IO;
using TideDBClasses;

namespace TidePrediction
{
    [Activity(Label = "TideList")]
    public class TideList : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            string dbPath;
            List<TideDB> tides;
            List<String> tidelist = new List<String>();

            // Check for database and create object
            dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "TidesDB.s3db");
            if (!File.Exists(dbPath))
            {
                using (Stream inStream = Assets.Open("TidesDB.s3db"))
                using (Stream outStream = File.Create(dbPath))
                    inStream.CopyTo(outStream);
            }
            TideDbAccess tideDb = new TideDbAccess(dbPath);

            
            tides = tideDb.RetrieveTide(Intent.GetStringExtra("Location"),Intent.GetStringExtra("Date"));
            foreach (TideDB tide in tides)
            {
                tidelist.Add(tide.Day + ", " + tide.Date + " at " + tide.Time + System.Environment.NewLine + tide.HL + " tide of " + tide.Cen);
            }

            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, tidelist);

        }


    }
}