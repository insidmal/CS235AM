using SQLite;

namespace TideDBClasses
{
    [Table("Tides")]
    public class TideDB
    {
        public string Date { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public string Feet { get; set; }
        public string Locale { get; set; }
        public string Cen { get; set; }
        public string HL { get; set; }
        public string Index { get; set; }
    }
}
