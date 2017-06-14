using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TidePredictionClasses
{
    public class Tides
    {
        private string feet;
        private string cen;
        private string hl;

        public int ID { get; set; }

        public string Date { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public string Feet {
            get { return feet; }
            set { feet = value + " feet"; }
        }
        public string Locale { get; set; }
        public string Cen
        {
            get { return cen; }
            set { cen = value + " centimeters"; }
        }
        public string HL
        {
            get { return hl;  }
            set {
                if (value == "H") hl = "High";
                else if (value == "L") hl = "Low";
                else hl = "";
            }
        }
        public int Index { get; set; }
    }

}
