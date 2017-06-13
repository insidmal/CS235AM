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
using System.Globalization;

namespace EventList
{
    class EventAdapter : BaseAdapter<Event>, ISectionIndexer
    {

        List<Event> events;
        Java.Lang.Object[] sectionsObjects;
        List<string> secIndex = new List<string>();
        Activity context;

        public EventAdapter(Activity con, List<Event> eventlist) : base()
        {
            context = con;
            events = eventlist;
            BuildSectionIndex();
        }

        public List<Event> EventList { get; }


        public override int Count
        {
            get
            {
                return events.Count();
            }
        }

        public override Event this[int position]
        {
            get
            {
                return events[position];
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //this is the ui for each item
            {
                View view = convertView;
                if (view == null)
                    view = context.LayoutInflater.Inflate(
                        Android.Resource.Layout.TwoLineListItem,
                        null);
                view.FindViewById<TextView>(Android.Resource.Id.Text1).Text
                = events[position].name;
                view.FindViewById<TextView>(Android.Resource.Id.Text2).Text
                = "Due by " + events[position].time + " on " + events[position].date;
                return view;
            }
        }


        private void BuildSectionIndex()
        {
            int i = 0;
            sectionsObjects = new Java.Lang.Object[events.Count];
            foreach (Event e in events)
            {
                secIndex.Add(e.date);
                sectionsObjects[i++] = new Java.Lang.String(e.date);
            }

        }


        public int GetPositionForSection(int sectionIndex)
        {
            return sectionIndex;
        }

        public int GetSectionForPosition(int position)
        {
            return position;
        }

        public Java.Lang.Object[] GetSections()
        {
            return sectionsObjects;
        }
    }
}