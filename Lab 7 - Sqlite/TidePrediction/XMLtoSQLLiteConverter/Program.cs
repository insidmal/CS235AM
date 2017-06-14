using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TidePredictionClasses;
using TideDBClasses;

namespace XMLtoSQLLiteConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tides> tides = new List<Tides>();

            string dir = @"../../../Files/";
            string dbPath = System.AppDomain.CurrentDomain.BaseDirectory + "/Files/TidesDB.s3db";

            TideDbAccess tideDb = new TideDbAccess(dbPath);

            string[] names = new string[3] {"depoebay.xml","florence.xml","yaquinabay.xml" };
            string[] locales = new string[3] { "Depoe Bay", "Florence", "Yaquina Bay" };
            int i = 0;

            foreach (string name in names)
            {

                StreamReader textIn =
            new StreamReader(
                new FileStream(dir + name, FileMode.OpenOrCreate, FileAccess.Read));


                tides.AddRange(ConvertXMLtoTides.ConvertTides(textIn, locales[i++]));

            }

            //tideDb.Create();

            //int ind = 0;
            //foreach (Tides tide in tides)
            //{
            //    tide.ID = ind++;
            //    tideDb.AddEntry(tide);
            //}


            Console.WriteLine(tides.Count());

            for (i = 0; i < 5; i++)
            {
                Console.WriteLine(i);
                Console.WriteLine(tideDb.Retrieve(i).Feet);
            }
            Console.WriteLine("Tide Locales");

            foreach (TideDB tideL in tideDb.RetrieveLocales())
            {
                Console.WriteLine(tideL.Locale);
            }

            Console.WriteLine("Tide Dates");

            //cut this off at 10 entries to save space
            i = 0;
            foreach (TideDB tideD in tideDb.RetrieveDates(0))
            {
                Console.WriteLine(tideD.Date);
                if (i++ > 10) break;
            }

            //select a locale and date
            Console.WriteLine("Tide for Deopoe Bay, 2017/01/23");
            foreach (TideDB tide in tideDb.RetrieveTide("Depoe Bay", "2017/01/23"))
            {
                Console.WriteLine(tide.HL + " of " + tide.Cen + " at " + tide.Time);
            }
            Console.WriteLine("Minimum Date: " + tideDb.RetrieveDates(-1)[0].Date.Replace("/", ""));
            Console.WriteLine("Maximum Date: " + tideDb.RetrieveDates(1)[0].Date.Replace("/", ""));
            Console.ReadLine();
            Console.WriteLine();
        }


        
    }
}
