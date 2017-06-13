using SQLite;
using System.Collections.Generic;
using System.Linq;


namespace TideDBClasses
{
    public class WsDbAccess
    {
        SQLiteConnection db;
        string path;

        public WsDbAccess(string dbPath)
        {
            path = dbPath;
        }



        private void Connect()
        {
            db = new SQLiteConnection(path);
        }

        public List<WeatherStation> GetStations()
        {
            Connect();
            string qry = "select Name from weatherStations order by Name Asc";
            var t = db.Query<WeatherStation>(qry, new string[1]);
            db.Close();
            return t;
        }

        public WeatherStation GetLocale(int id)
        {
            Connect();
            string qry = "select * from weatherStations where Id=?";
            var t = db.Get<WeatherStation>(id);
            db.Close();
            return t;
        }

        public WeatherStation GetId(string name)
        {
            Connect();
            string qry = "select * from weatherStations where Name=?";
            var t = db.Query<WeatherStation>(qry, name);
            db.Close();
            return t.FirstOrDefault<WeatherStation>();
        }

    }
}
