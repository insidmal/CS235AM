
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

        public Tides Retrieve(int id)
        {
            Connect();
            Tides t = db.Get<Tides>(id);
            db.Close();
            return t;
        }

        public List<Tides> RetrieveLocale(string locale)
        {
            Connect();
            string qry = "select * from Tides where locale='?'";
            string[] para = new string[1] { locale };
            List<Tides> t = db.Query<Tides>(qry, para);
            db.Close();
            return t;
        }

        public List<Tides> RetrieveAll()
        {
            Connect();
            string qry = "select * from Tides order by locale asc";
            List<Tides> t = new List<Tides>();
            t = db.Query<Tides>(qry, null);
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
                int count = Convert.ToInt32(db.Query<Tides>(qry,null));
            db.Close();
            return count;
            }


        }

    }

