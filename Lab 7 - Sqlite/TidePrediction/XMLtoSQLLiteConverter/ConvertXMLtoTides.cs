using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TidePredictionClasses;
using System.Xml;
using System.IO;


namespace XMLtoSQLLiteConverter
{
    public class ConvertXMLtoTides
    {
        public static List<Tides> ConvertTides(StreamReader file, string locale="not found") {
            Tides tide = new Tides();
            List<Tides> tides = new List<Tides>();
            string date;
            using (XmlReader reader = XmlReader.Create(file))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "item":
                                tide = new Tides();
                                tide.Locale = locale;
                                break;
                            case "date":
                                if (reader.Read() && tide != null) {
                                date = reader.Value.Trim();
                                tide.Date = date;
                                tide.Index = Int32.Parse(date.Substring(5, 2));
                                }
                                break;
                            case "day":
                                if (reader.Read() && tide != null)
                                    tide.Day = reader.Value.Trim();
                                break;
                            case "time":
                                if (reader.Read() && tide != null)
                                    tide.Time = reader.Value.Trim();
                                break;
                            case "pred_in_ft":
                                if (reader.Read() && tide != null)
                                    tide.Feet = reader.Value.Trim();
                                break;
                            case "pred_in_cm":
                                reader.Read();
                                tide.Cen = reader.Value.Trim();
                                break;
                            case "highlow":
                                if(reader.Read() && tide != null)
                                tide.HL = reader.Value.Trim();
                                break;
                        }

                    }
                    else
                    {
                        if (reader.Name == "item") { tides.Add(tide); tide = null; }
                    }
                }
            }
            tides.Sort((x, y) => x.Index.CompareTo(y.Index));
            return tides;
        }
    }
}