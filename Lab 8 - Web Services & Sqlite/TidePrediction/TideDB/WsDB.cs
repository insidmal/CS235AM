using SQLite;



namespace TideDBClasses
{
    [Table("weatherStations")]
    public class WeatherStation
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string  Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Predictions { get; set; }


    }
}
