using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace EventDB
{
    [Table("Event")]
    public class Event
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string where { get; set; }
        public string with { get; set; }
        public string extra { get; set; }
        public int state { get; set; }


        public Event Update(Event e)
        {
            name = e.name;
            date = e.date;
            time = e.time;

            where = e.where;
            with = e.with;
            extra = e.extra;
            state = e.state;

            return e;
        }

    }

}
