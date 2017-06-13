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
    [Activity(Label = "Edit Event")]
    public class EditEvent : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Edit);


            if (!Intent.HasExtra("Id"))
            {
                var go = new Android.Content.Intent(this, typeof(MainActivity));
                StartActivity(go);
            }
            else
            {

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

                var nameText = FindViewById<EditText>(Resource.Id.NameText);
                var whereText = FindViewById<EditText>(Resource.Id.WhereText);
                var withText = FindViewById<EditText>(Resource.Id.WithText);
                var extraText = FindViewById<EditText>(Resource.Id.ExtraText);
                var date = FindViewById<DatePicker>(Resource.Id.DatePicker);
                var time = (TimePicker)FindViewById<TimePicker>(Resource.Id.TimePicker);
                var addButton = FindViewById<Button>(Resource.Id.AddButton);

                string[] datepart;
                string displaydate;

                string displaytime;
                string[] timepart;

                string meridian = "am";
                int timehr;


                if (Intent.HasExtra("name")) nameText.Text = Intent.GetStringExtra("name");
                else nameText.Text = e.name;

                if (Intent.HasExtra("where")) whereText.Text = Intent.GetStringExtra("where");
                else whereText.Text = e.where;

                if (Intent.HasExtra("with")) withText.Text = Intent.GetStringExtra("with");
                else withText.Text = e.with;

                if (Intent.HasExtra("extra")) extraText.Text = Intent.GetStringExtra("extra");
                else extraText.Text = e.extra;

                if (Intent.HasExtra("date")) displaydate = Intent.GetStringExtra("date");
                else displaydate = e.date;

                datepart = displaydate.Split('/');

                date.UpdateDate(int.Parse(datepart[0]), (int.Parse(datepart[1]) - 1), int.Parse(datepart[2]));

                if (Intent.HasExtra("time")) displaytime = Intent.GetStringExtra("time");
                else displaytime = e.time;

                timepart = displaytime.Split(new char[2] { ':', ' ' });

                if (timepart[2] == "pm") timepart[0] += 12;

                time.Hour = int.Parse(timepart[0]);
                time.Minute = int.Parse(timepart[1]);

                addButton.Click += delegate
                {
                    e.name = nameText.Text;
                    e.date = date.Year + "/" + (date.Month+1).ToString("00") + "/" + date.DayOfMonth.ToString("00");
                    e.where = whereText.Text;
                    e.with = withText.Text;
                    e.extra = extraText.Text;
                    if (time.Hour > 12) { timehr = time.Hour - 12; meridian = "pm"; }
                    else timehr = time.Hour;
                    e.time = timehr.ToString() + ":" + time.Minute + " " + meridian;
                    eventDb.Update(e);
                    base.OnBackPressed();
                    Android.Widget.Toast.MakeText(this, "Updated!", Android.Widget.ToastLength.Short).Show();


                };


            }


           



    }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            var nameText = FindViewById<EditText>(Resource.Id.NameText);
            var whereText = FindViewById<EditText>(Resource.Id.WhereText);
            var withText = FindViewById<EditText>(Resource.Id.WithText);
            var extraText = FindViewById<EditText>(Resource.Id.ExtraText);
            var date = FindViewById<DatePicker>(Resource.Id.DatePicker);
            var time = (TimePicker)FindViewById<TimePicker>(Resource.Id.TimePicker);
            string meridian = "am";
            int timehr;

            outState.PutString("name", nameText.Text);
            outState.PutString("where", whereText.Text);
            outState.PutString("with", withText.Text);
            outState.PutString("extra", extraText.Text);
            outState.PutString("date", date.Year + "/" + (date.Month + 1).ToString("00") + "/" + date.DayOfMonth.ToString("00"));
            if(time.Hour>12) { timehr = time.Hour - 12; meridian = "pm"; }
            outState.PutString("time", time.Hour + ":" + time.Minute + " " + meridian);

            base.OnSaveInstanceState(outState);


        }
    }
}