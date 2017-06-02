
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TidePredictionClasses;

namespace TideDBClasses
{
    public class TideDbAccess
    {
        SQLiteConnection db;
        string path;

        public TideDbAccess(string dbPath)
        {
            path = dbPath;
        }

        private void Connect()
        {
            db = new SQLiteConnection(path);
        }
        public void Create()
        {
            
            Connect();
            db.DropTable<TideDB>();
            if (db.CreateTable<TideDB>() == 0)
            {
                // A table already exixts, delete any data it contains
                db.DeleteAll<TideDB>();
            }
            db.Close();

        }

        public void Update(Tides tide)
        {
            Connect();
            db.Update(tide);
            db.Commit();
            db.Close();
        }

        public void Delete(Tides tide)
        {
            Connect();
            db.Delete(tide);
            db.Commit();
            db.Close();
        }

        public TideDB Retrieve(int id)
        {
            Connect();
            TideDB t = db.Get<TideDB>(id);
            
            db.Close();
            return t;
        }

        public List<TideDB> RetrieveLocale(string locale)
        {
            Connect();
            string qry = "select * from Tides where locale='?'";
            string[] para = new string[1] { locale };
            List<TideDB> t = db.Query<TideDB>(qry, para);
            db.Close();
            return t;
        }

        public List<TideDB> RetrieveTide(string locale, string date)
        {
            Connect();
            string qry = "select * from Tides where locale=? and date=?  order by (case when Time like '% am' then 1 else 2 end), Time";
            var t = db.Query<TideDB>(qry, new string[2] { locale, date });
            db.Close();
            return t;
        }


        public List<TideDB> RetrieveLocales()
        {
            Connect();
            string qry = "select distinct(Locale) from Tides order by Locale Asc";
            var t = db.Query<TideDB>(qry, new string[1]);
            db.Close();
            return t;
        }
        
        /// <summary>
        /// Displays which Dates are Available.
        /// </summary>
        /// <param name="range">Expected integer -1 to 1. -1 Returns Min date, 0 Returns all Dates, and 1 returns Max date.</param>
        /// <returns></returns>
        public List<TideDB> RetrieveDates(int range)
        {
            if (range < -1 || range > 1) throw new ArgumentOutOfRangeException("Expected: -1, 0, or 1");
            string qry;
            Connect();
            if (range == 0) qry = "select distinct(Date) from Tides order by Date Asc";
            else if (range == -1) qry = "select distinct(Date) from Tides order by Date asc limit 1";
            else qry = "select distinct(Date) from Tides order by Date desc limit 1";
            var t = db.Query<TideDB>(qry, new string[1]);
            db.Close();
            return t;
        }

        public List<TideDB> RetrieveAll()
        {
            Connect();
            string qry = "select * from Tides order by locale asc";
            List<TideDB> t = new List<TideDB>();
            t = db.Query<TideDB>(qry, null);
            db.Close();
            return t;

        }

        public void AddEntry(Tides tide) {
            Connect();
            db.BeginTransaction();
            db.Insert(tide);
            db.Commit();
            db.Close();
        }


        public int CountEntries()
        {
            //Connect();
            //var c = db.GetTableInfo("Tides");
            //db.Close();
            //return c.Count();

            string qry = "SELECT COUNT(ID) FROM tblActivities WHERE [Activity] = 'Sleeping'";
            Connect();
                int count = Convert.ToInt32(db.Query<TideDB>(qry,null));
            db.Close();
            return count;
            }


        }

    }

