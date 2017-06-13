using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Xml;

namespace TidePredictionClasses
{
    public class TideRest
    {
        public string Id { get; set; }

        public string GetTide(string id, string date)
        {
            string start = "https://tidesandcurrents.noaa.gov/api/datagetter?product=predictions";
                string stapara = "&station=";
            string dtpara = "&begin_date=";
            string paras = "&range=8760&time_zone=lst_ldt&format=xml&units=metric&datum=stnd&interval=hilo";
            string response;

            string uri = start + stapara + id + dtpara + date + paras;

            try
            {
                using (var webClient = new WebClient())
                {
                    response = webClient.DownloadString(uri);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return response;
        }



        public List<Tides> XmlToTide(string xml, string locale)
        {
            Tides tide = new Tides();
            List<Tides> list = new List<Tides>();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml);
            string time;
            string[] timeParts;
            string date;
            DateTime dt = new DateTime();


            XmlNodeList xmlelem = xmldoc.GetElementsByTagName("pr");
            foreach (XmlNode t in xmlelem)
            {
                tide = new Tides();

                date = t.Attributes["t"].Value;


                //format the time correctly
                time = date.Substring(date.Length - 5);
                timeParts = time.Split(':');
                if (Int32.Parse(timeParts[0]) > 12) tide.Time = Int32.Parse(timeParts[0]) - 12 + ":" + timeParts[1] + " pm";
                else tide.Time = time + " am";
                double cen;
                double feet;
                //get the day from the date
                dt = DateTime.ParseExact(date.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                tide.Day = dt.ToString("dddd");

                tide.Date = date.Substring(0, 10).Replace('-','/');

                cen = double.Parse(t.Attributes["v"].Value) * 100;
                tide.Cen = cen.ToString();

                feet = cen * 0.0328084;
                tide.Feet = feet.ToString();

                tide.Locale = locale;
                tide.HL = t.Attributes["type"].Value;

                tide.Index = 1;

                list.Add(tide);
            }
            return list;
        }

    }
}