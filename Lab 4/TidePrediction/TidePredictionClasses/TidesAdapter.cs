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
using Java.Lang;
using System.Globalization;

namespace TidePredictionClasses
{
    public class TidesAdapter : BaseAdapter<Tides>, ISectionIndexer
    {
        List<Tides> tides;
       Java.Lang.Object[] sectionsObjects;
        List<string> secIndex = new List<string>();
        Activity context;

        public TidesAdapter(Activity con, List<Tides> tidelist) : base()
        {
            context = con;
            tides = tidelist;
            BuildSectionIndex();
        }

        public List<Tides> TideList { get; }


        public override int Count
        {
            get
            {
                return tides.Count();
            }
        }

        public override Tides this[int position]
        {
            get
            {
                return tides[position];
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
                = tides[position].Date + " " + tides[position].Time;
                view.FindViewById<TextView>(Android.Resource.Id.Text2).Text
                = tides[position].HL;
                return view;
            }
        }


        private void BuildSectionIndex()
        {
            int i = 0;
            sectionsObjects = new Java.Lang.Object[tides.Count];
            foreach (Tides tide in tides)
            {
                secIndex.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(tide.Index));
                sectionsObjects[i++] = new Java.Lang.String(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(tide.Index));
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