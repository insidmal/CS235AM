using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using TideDBClasses;
using TidePredictionClasses;
using Plugin.Geolocator;
using System.Threading.Tasks;
using Android.Locations;
using Java.Text;
using Java.Util;

namespace TidePrediction
{
    [Activity(Label = "Local Tide List",MainLauncher = true)]
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

            Location startLoc = new Location("Current");
            Location endLoc = new Location("Station");
            List<WeatherStation> stationlist = new List<WeatherStation>();
            float distance = 10000000000;
            string inputLocation = "none";

            stationlist = wsDb.GetStationData();
            String inputDate = new SimpleDateFormat("yyyy/MM/dd").Format(new Date());

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            //string inputLocation = Intent.GetStringExtra("Location");
            //string inputDate = Intent.GetStringExtra("Date");

            // GetPositionAsynch will either get position information or timeout
            locator.GetPositionAsync(timeoutMilliseconds: 10000)
            // After getting position info or timing out, execution will continue here
            // t represents a Task object (GetPositionAsync returns a Task object)
            .ContinueWith(t =>
            {
                // t.Result is a Position object
                    startLoc.Latitude = t.Result.Latitude;
                    startLoc.Longitude =  t.Result.Longitude;

                // Specify the thread to continue on- it's the UI thread

                foreach (WeatherStation station in stationlist)
                {
                    endLoc = new Location("Station");
                    endLoc.Longitude = station.Lon;
                    endLoc.Latitude = station.Lat;

                    if (startLoc.DistanceTo(endLoc) < distance)
                    {
                        distance = startLoc.DistanceTo(endLoc);
                        inputLocation = station.Name;
                    }


                }






                tides = tideDb.RetrieveTide(inputLocation, inputDate);

                if (tides.Count <= 0)
                {
                    //loading from web service if we didn't get any entries
                    string location = wsDb.GetId(inputLocation).Id.ToString();
                    var tidedata = tideRest.GetTide(location, inputDate.Replace("/", string.Empty));
                    List<Tides> tlist = tideRest.XmlToTide(tidedata, inputLocation);


                    foreach (Tides ti in tlist)
                    {
                        //saving to db
                        tideDb.AddEntry(ti);

                    }

                    //now we'll reload the database so we have one with valid values and resume as normal
                    tideDb = new TideDbAccess(dbPath);
                    tides = tideDb.RetrieveTide(inputLocation, inputDate);


                }

                tidelist.Add("Tides For " + inputLocation);
                foreach (TideDB tide in tides)
                {
                    tidelist.Add(tide.Day + ", " + tide.Date + " at " + tide.Time + System.Environment.NewLine + tide.HL + " tide of " + tide.Cen);
                }




                ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, tidelist);


            }, TaskScheduler.FromCurrentSynchronizationContext());




           
          



        }


    }
}