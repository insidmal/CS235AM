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

            tideDb.Create();

            int ind = 0;
            foreach (Tides tide in tides)
            {
                tide.ID = ind++;
                tideDb.AddEntry(tide);
            }


            Console.WriteLine(tides.Count());

            for (int ind2 = ind; ind2 > -1; ind2--)
            {
                Console.WriteLine(tideDb.Retrieve(ind2).Feet);
            }
            Console.ReadLine();
            Console.WriteLine();
        }
    }
}
