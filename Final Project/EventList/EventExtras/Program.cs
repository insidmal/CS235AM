using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventDB;

namespace EventExtras
{
    public class Program
    {



        static void Main(string[] args)
        {
            string dbPath = System.AppDomain.CurrentDomain.BaseDirectory + "/Files/EventDB.db";

            //using (var conn = new SQLite.SQLiteConnection(dbPath))
            //{
            //    conn.CreateTable<Event>();
            //}

            EventDbAccess eventDb = new EventDbAccess(dbPath);

            string meridian;
            int month;
            int hour;
            int day;
            int minute;
            string date;
            string time;
            Random rnd = new Random();
            Event e = new Event();
            for (int i = 0; i < 100; i++)
            {
                month = rnd.Next(6, 13);
                day = rnd.Next(1, 31);
                hour = rnd.Next(1, 13);
                minute = rnd.Next(1, 60);
                if (rnd.Next(1, 3) == 1) meridian = "am";
                else meridian = "pm";

                date = "2017/" + month.ToString("00") + "/" + day.ToString("00");
                time = hour.ToString("00") + ":" + minute.ToString("00") + " " + meridian;


                e.name = "Random Event " + i.ToString();
                e.date = date;
                e.time = time;
                e.with = "Bob, Joe, Sue";
                e.where = "Lane Community College";
                e.extra = "This is a randomly generated event for demonstration purposes.";
                e.state = 0;

                eventDb.Create(e);
            }

            List<Event> d = eventDb.RetrieveId(1);

            Console.WriteLine(d[0].with);

            e = d[0];
            e.with = "Joe, Fred, Bob";
            eventDb.Update(e);


            List<Event> d2 = eventDb.RetrieveId(1);

            Console.WriteLine(d2[0].with);

            Console.WriteLine();
            Console.ReadLine();
        }
        }
    }
