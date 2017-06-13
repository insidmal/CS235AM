using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using TideDBClasses;
using TidePredictionClasses;

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
            Tides newtide = new Tides();

            // Check for database and create object
            dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "TidesDB.s3db");
            if (!File.Exists(dbPath))
            {
                using (Stream inStream = Assets.Open("TidesDB.s3db"))
                using (Stream outStream = File.Create(dbPath))
                    inStream.CopyTo(outStream);
            }

            TideDbAccess tideDb = new TideDbAccess(dbPath);
            WsDbAccess wsDb = new WsDbAccess(dbPath);
            TideRest tideRest = new TideRest();

            string inputLocation = Intent.GetStringExtra("Location");
            string inputDate = Intent.GetStringExtra("Date");

            tides = tideDb.RetrieveTide(inputLocation,inputDate);

            if (tides.Count <= 0)
            {
                //loading from web service if we didn't get any entries
                string location = wsDb.GetId(inputLocation).Id.ToString();
                var tidedata = tideRest.GetTide(location, inputDate.Replace("/",string.Empty));
                List<Tides> tlist = tideRest.XmlToTide(tidedata, inputLocation);


                foreach (Tides t in tlist)
                {
                    //saving to db
                    tideDb.AddEntry(t);

                }

                //now we'll reload the database so we have one with valid values and resume as normal
                tideDb = new TideDbAccess(dbPath);
                tides = tideDb.RetrieveTide(inputLocation, inputDate);


            }


                foreach (TideDB tide in tides)
                {
                    tidelist.Add(tide.Day + ", " + tide.Date + " at " + tide.Time + System.Environment.NewLine + tide.HL + " tide of " + tide.Cen);
                }

            
            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, tidelist);

        }


    }
}