using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace EventDB
{
    public class EventDbAccess
    {
        SQLiteConnection db;
        string path;


        public EventDbAccess(string dbPath)
        {
            path = dbPath;
        }

        private void Connect()
        {
            db = new SQLiteConnection(path);
        }
        
        public void Create(Event e)
        {
            Connect();
            db.Insert(e);
            db.Close();
        }

        public void Delete(Event e)
        {
            Connect();
            db.Delete(e);
            db.Close();
        }

        public void Update(Event e)
        {
            Connect();
            db.Update(e);
            db.Close();
        }

        public Event RetrieveId(int id)
        {
            Connect();
            string qry = "select * from event where  ID = ?";
            string[] para = new string[1] { id.ToString() };
            List<Event> e = db.Query<Event>(qry, para);
            return e[0];
        }


        public List<Event> RetrieveAll()
        {
            Connect();
            string qry = "select * from event where state = 0 order by date, (case when time like '% am' then 1 else 2 end), time";
            List<Event> e = db.Query<Event>(qry, new string[1]);
            return e;
        }

        public List<Event> RetrieveDone()
        {
            Connect();
            string qry = "select * from event where state = 1 order by date, (case when time like '% am' then 1 else 2 end), time";
            List<Event> e = db.Query<Event>(qry, new string[1]);
            return e;
        }

        public Event RetrieveFirst()
        {
            Connect();
            string qry = "select * from event where state = 0 order by date, (case when time like '% am' then 1 else 2 end), time limit 1";
            List<Event> e = db.Query<Event>(qry, new string[1]);
            return e[0];
        }

    }
}
