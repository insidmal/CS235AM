using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EventDB;

namespace EventList
{
    [Activity(Label = "Event Details")]
    public class ViewDetails : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Details);

            if(!Intent.HasExtra("Id"))
            {
                var go = new Android.Content.Intent(this, typeof(MainActivity));
                StartActivity(go);
            }
            else
            {

                var itemName = FindViewById<TextView>(Resource.Id.itemName);
                var itemDate = FindViewById<TextView>(Resource.Id.itemDate);
                var itemWith = FindViewById<TextView>(Resource.Id.itemWith);
                var itemWhere = FindViewById<TextView>(Resource.Id.itemWhere);
                var itemExtra = FindViewById<TextView>(Resource.Id.itemExtra);
                var doneButton = FindViewById<Button>(Resource.Id.DoneButton);
                var deleteButton = FindViewById<Button>(Resource.Id.DeleteButton);
                var editButton = FindViewById<Button>(Resource.Id.EditButton);


                int Id = Intent.GetIntExtra("Id",0);

                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "EventDB.db");
                if (!File.Exists(dbPath))
                {
                    using (Stream inStream = Assets.Open("EventDB.db"))
                    using (Stream outStream = File.Create(dbPath))
                        inStream.CopyTo(outStream);
                }

                EventDbAccess eventDb = new EventDbAccess(dbPath);

                Event e = eventDb.RetrieveId(Id);

                itemName.Text = e.name;
                itemDate.Text = "Due by " + e.time + " on " + e.date;
                itemWith.Text = "With " + e.with;
                itemWhere.Text = "At " + e.where;
                itemExtra.Text = e.extra;

                if (e.state == 1) doneButton.Visibility = ViewStates.Gone;

                deleteButton.Click += delegate
                {
                    eventDb.Delete(e);
                    base.OnBackPressed();
                    Android.Widget.Toast.MakeText(this, "Event Deleted", Android.Widget.ToastLength.Short).Show();
                };

                editButton.Click += delegate
                {
                    var go = new Android.Content.Intent(this, typeof(EditEvent));
                    go.PutExtra("Id", e.ID);
                    StartActivity(go);
                };


                doneButton.Click += delegate
                {
                    e.state = 1;
                    eventDb.Update(e);
                    base.OnBackPressed();


                    Android.Widget.Toast.MakeText(this, "Completed!", Android.Widget.ToastLength.Short).Show();
                };
            }
            
        }

        protected override void OnResume()
        {
            base.OnResume();
            int Id = Intent.GetIntExtra("Id", 0);

            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "EventDB.db");
            if (!File.Exists(dbPath))
            {
                using (Stream inStream = Assets.Open("EventDB.db"))
                using (Stream outStream = File.Create(dbPath))
                    inStream.CopyTo(outStream);
            }

            EventDbAccess eventDb = new EventDbAccess(dbPath);

            Event e = eventDb.RetrieveId(Id);
            var itemName = FindViewById<TextView>(Resource.Id.itemName);
            var itemDate = FindViewById<TextView>(Resource.Id.itemDate);
            var itemWith = FindViewById<TextView>(Resource.Id.itemWith);
            var itemWhere = FindViewById<TextView>(Resource.Id.itemWhere);
            var itemExtra = FindViewById<TextView>(Resource.Id.itemExtra);
            var doneButton = FindViewById<Button>(Resource.Id.DoneButton);

            itemName.Text = e.name;
            itemDate.Text = "Due by " + e.time + " on " + e.date;
            itemWith.Text = "With " + e.with;
            itemWhere.Text = "At " + e.where;
            itemExtra.Text = e.extra;

            if (e.state == 1) doneButton.Visibility = ViewStates.Gone;
        }
    }
}