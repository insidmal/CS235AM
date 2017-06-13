using Android.App;
using Android.Widget;
using Android.OS;
using EventDB;
using System.Collections.Generic;
using System.IO;

namespace EventList
{
    [Activity(Label = "Upcoming Event", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Event e;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);


            string dbPath;
            List<string> eventlist = new List<string>();
            dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "EventDB.db");
            if (!File.Exists(dbPath))
            {
                using (Stream inStream = Assets.Open("EventDB.db"))
                using (Stream outStream = File.Create(dbPath))
                    inStream.CopyTo(outStream);
            }

            EventDbAccess eDb = new EventDbAccess(dbPath);

            e = eDb.RetrieveFirst();


            var label = FindViewById<TextView>(Resource.Id.eventInstruct);
            var name = FindViewById<TextView>(Resource.Id.itemName);
            var due = FindViewById<TextView>(Resource.Id.itemDue);
            var detailsButton = FindViewById<Button>(Resource.Id.ViewDetailsButton);

            var doneButton = FindViewById<Button>(Resource.Id.DoneButton);
            var editButton = FindViewById<Button>(Resource.Id.EditButton);
            var viewButton = FindViewById<Button>(Resource.Id.ViewButton);
            var viewDoneButton = FindViewById<Button>(Resource.Id.ViewDoneButton);
            var addButton = FindViewById<Button>(Resource.Id.AddButton);


            name.Text = e.name;
            due.Text = "Due by " + e.time + " on " + e.date;


            doneButton.Click += delegate
            {
                e.state = 1;
                eDb.Update(e);
                e = eDb.RetrieveFirst();

                label.Text = "Next Event:";
                name.Text = e.name;
                due.Text = "Due by " + e.time + " on " + e.date;
                Android.Widget.Toast.MakeText(this, "Completed!", Android.Widget.ToastLength.Short).Show();
            };

            viewButton.Click += delegate
            {
                var go = new Android.Content.Intent(this, typeof(ViewEvents));
                StartActivity(go);
            };

            addButton.Click += delegate
            {
                var go = new Android.Content.Intent(this, typeof(AddEvent));
                StartActivity(go);
            };

            viewDoneButton.Click += delegate
            {
                var go = new Android.Content.Intent(this, typeof(CompletedEvents));
                StartActivity(go);
            };

            detailsButton.Click += delegate
            {
                var go = new Android.Content.Intent(this, typeof(ViewDetails));
                go.PutExtra("Id", e.ID);
                StartActivity(go);
            };
            editButton.Click += delegate
            {
                var go = new Android.Content.Intent(this, typeof(EditEvent));
                go.PutExtra("Id", e.ID);
                StartActivity(go);
            };

        }

        protected override void OnResume()
        {
            base.OnResume();


            string dbPath;
            List<string> eventlist = new List<string>();
            dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "EventDB.db");
            if (!File.Exists(dbPath))
            {
                using (Stream inStream = Assets.Open("EventDB.db"))
                using (Stream outStream = File.Create(dbPath))
                    inStream.CopyTo(outStream);
            }

            EventDbAccess eDb = new EventDbAccess(dbPath);

            e = eDb.RetrieveFirst();
            var name = FindViewById<TextView>(Resource.Id.itemName);
            var due = FindViewById<TextView>(Resource.Id.itemDue);


            name.Text = e.name;
            due.Text = "Due by " + e.time + " on " + e.date;

        }
    }
}

