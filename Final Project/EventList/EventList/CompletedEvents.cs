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
using EventDB;
using System.IO;

namespace EventList
{
    [Activity(Label = "Completed Events")]
    public class CompletedEvents : ListActivity
    {
        List<Event> events = new List<Event>();

        protected override void OnCreate(Bundle savedInstanceState)
        {


            base.OnCreate(savedInstanceState);
            string dbPath;

            dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "EventDB.db");
            if (!File.Exists(dbPath))
            {
                using (Stream inStream = Assets.Open("EventDB.db"))
                using (Stream outStream = File.Create(dbPath))
                    inStream.CopyTo(outStream);
            }

            EventDbAccess eventDb = new EventDbAccess(dbPath);
            events = eventDb.RetrieveDone();


            ListAdapter = new EventAdapter(this, events);
            ListView.FastScrollEnabled = true;

        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var go = new Android.Content.Intent(this, typeof(ViewDetails));
            go.PutExtra("Id", events[position].ID);
            StartActivity(go);
        }

        protected override void OnResume()
        {
            base.OnResume();

            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "EventDB.db");
            if (!File.Exists(dbPath))
            {
                using (Stream inStream = Assets.Open("EventDB.db"))
                using (Stream outStream = File.Create(dbPath))
                    inStream.CopyTo(outStream);
            }

            EventDbAccess eventDb = new EventDbAccess(dbPath);
            events = eventDb.RetrieveDone();
            ListAdapter = new EventAdapter(this, events);
            ListView.FastScrollEnabled = true;

        }
    }
}